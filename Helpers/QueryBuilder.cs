using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gero.API.Helpers
{
    public class QueryBuilder
    {
        private static readonly string FILE_PATH = "wwwroot/Queries/";

        public static RawSqlString Build(string fileName, List<QueryParameter> parameters = null)
        {
            string sqlString = System.IO.File.ReadAllText(FILE_PATH + fileName);

            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    if (sqlString.Contains(parameter.key))
                    {
                        sqlString = sqlString.Replace(parameter.key, parameter.value);
                    }
                }
            }

            return new RawSqlString(sqlString.ToString());
        }
    }

    public class QueryParameter
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
