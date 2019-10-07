using System;
using System.Data.SqlClient;
using System.Web;

namespace refactor_this.Models
{
    public class Helpers
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        private static SqlConnection conn = null;
        public static SqlConnection NewConnection()
        {
            var connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
            return new SqlConnection(connstr);
        }

        public static SqlConnection GetConnection()
        {
            if(conn == null)
            {
                return NewConnection();
            }

            return conn;
        }

        public static SqlDataReader ExecuteQuery(string command, bool nonQuery=false)
        {
            try
            {
                SqlConnection conn = GetConnection();
                conn.Open();

                var cmd = new SqlCommand(command, conn);

                if(!nonQuery)
                    return cmd.ExecuteReader();
                else
                    cmd.ExecuteNonQuery();
                return null;
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Console.WriteLine("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                return null;
            }
        }
    }
}