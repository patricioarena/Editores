using System.Data;

namespace Dominio.Entities
{
    public class DbParameter
    {
        public string Name { get; set; }
        public ParameterDirection Direction { get; set; }
        public object Value { get; set; }

        public DbParameter(string paramName, ParameterDirection paramDirection, object paramValue)
        {
            Name = paramName;
            Direction = paramDirection;
            Value = paramValue;
        }

    }

}
