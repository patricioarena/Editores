using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Dominio.Entities;

namespace DataAccess
{
    public interface IDbManager
    {
        T ExecuteSingle<T>(string procedureName) where T : new();
        T ExecuteSingle<T>(string procedureName, List<DbParameter> parameters) where T : new();
        List<T> ExecuteList<T>(string procedureName) where T : new();
        List<T> ExecuteList<T>(string procedureName, List<DbParameter> parameters) where T : new();
        int ExecuteNonQuery(string procedureName, List<DbParameter> parameters);
    }
}
