using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DoctorSearch.Interfaces;

namespace DoctorSearch.utilities
{
    public class DapperHelper : IDapper
    {
        public IEnumerable<T> Query<T>(string connection, string sql, object parameter = null)
        {
            IEnumerable<T> response;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();

                response = sqlConnection.Query<T>(sql, parameter);

                sqlConnection.Close();
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                }
            }

            return response;
        }
        
    }
}