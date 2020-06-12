using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Application.IFactory;
using Dominio.Entities;

namespace Application.Factory
{
    public class DbManager : IDbManager
    {
        public DbManager(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private string ConnectionString { get; set; }

        //private static string ConnectionString
        //{
        //    get
        //    {
        //        return @"server=my.sql.server;user id=youHere;password=yourPassword;database=yourTestDB";
        //    }
        //}

        private SqlConnection Connection { get; set; }

        private SqlCommand Command { get; set; }

        public List<DbParameter> OutParameters { get; private set; }

        private void Open()
        {
            try
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                Close();
            }
        }

        private void Close()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }

        // executes stored procedure with DB parameteres if they are passed
        private object ExecuteProcedure(string procedureName, ExecuteType executeType, List<DbParameter> parameters)
        {
            object returnObject = null;

            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Command = new SqlCommand(procedureName, Connection);
                    Command.CommandType = CommandType.StoredProcedure;

                    // pass stored procedure parameters to command
                    if (parameters != null)
                    {
                        Command.Parameters.Clear();

                        foreach (DbParameter dbParameter in parameters)
                        {
                            SqlParameter parameter = new SqlParameter();
                            parameter.ParameterName = "@" + dbParameter.Name;
                            parameter.Direction = dbParameter.Direction;
                            parameter.Value = dbParameter.Value;
                            Command.Parameters.Add(parameter);
                        }
                    }

                    switch (executeType)
                    {
                        case ExecuteType.ExecuteReader:
                            returnObject = Command.ExecuteReader();
                            break;
                        case ExecuteType.ExecuteNonQuery:
                            returnObject = Command.ExecuteNonQuery();
                            break;
                        case ExecuteType.ExecuteScalar:
                            returnObject = Command.ExecuteScalar();
                            break;
                        default:
                            break;
                    }
                }
            }

            return returnObject;
        }

        // updates output parameters from stored procedure
        private void UpdateOutParameters()
        {
            if (Command.Parameters.Count > 0)
            {
                OutParameters = new List<DbParameter>();
                OutParameters.Clear();

                for (int i = 0; i < Command.Parameters.Count; i++)
                {
                    if (Command.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        OutParameters.Add(new DbParameter(Command.Parameters[i].ParameterName,
                                                          ParameterDirection.Output,
                                                          Command.Parameters[i].Value));
                    }
                }
            }
        }

        // executes scalar query stored procedure without parameters
        public T ExecuteSingle<T>(string procedureName) where T : new()
        {
            return ExecuteSingle<T>(procedureName, null);
        }

        // executes scalar query stored procedure and maps result to single object
        public T ExecuteSingle<T>(string procedureName, List<DbParameter> parameters) where T : new()
        {
            Open();
            IDataReader reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);
            T tempObject = new T();

            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var resultValue = reader.GetValue(i);
                    resultValue = resultValue == DBNull.Value ? null : resultValue;
                    PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                    propertyInfo.SetValue(tempObject, resultValue, null);

                }
            }

            reader.Close();

            UpdateOutParameters();

            Close();

            return tempObject;
        }

        // executes list query stored procedure without parameters
        public List<T> ExecuteList<T>(string procedureName) where T : new()
        {
            return ExecuteList<T>(procedureName, null);
        }

        // executes list query stored procedure and maps result generic list of objects
        public List<T> ExecuteList<T>(string procedureName, List<DbParameter> parameters) where T : new()
        {
            List<T> objects = new List<T>();

            Open();
            IDataReader reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);

            while (reader.Read())
            {
                T tempObject = new T();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                        if (propertyInfo == null)
                            propertyInfo = typeof(T).GetProperty(char.ToUpper(reader.GetName(i)[0]) + reader.GetName(i).Substring(1));
                        propertyInfo.SetValue(tempObject, reader.GetValue(i), null);
                    }
                }

                objects.Add(tempObject);
            }

            reader.Close();

            UpdateOutParameters();

            Close();

            return objects;
        }

        // executes non query stored procedure with parameters
        public int ExecuteNonQuery(string procedureName, List<DbParameter> parameters)
        {
            int returnValue;

            Open();

            returnValue = (int)ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters);

            UpdateOutParameters();

            Close();

            return returnValue;
        }
    }
}
