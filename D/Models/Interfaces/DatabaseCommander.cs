using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;

namespace D.Models.Interfaces
{
    public class DatabaseCommander : IDatabaseCommander
    {
        public object ExecuteSql(string sql)
        {
            object result = null;

            using (var connection = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=D;User Id=sa;password=prosvasis;"))
            {
                result = connection.Query<object>(sql).ToList();
            }

            return result;
        }
    }
}