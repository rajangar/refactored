using System;
using System.Data.SqlClient;
using System.Web;

namespace refactor_this.Models
{
    /// <summary>
    /// Helpers class to provide connection with Database and execute queries
    /// </summary>
    public class Helpers
    {
        // ConnectionString can be taken as environment variables or in some config file
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
            @"AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        public static SqlConnection NewConnection()
        {
            var connstr = ConnectionString.Replace("{DataDirectory}",
                HttpContext.Current.Server.MapPath("~/App_Data"));
            return new SqlConnection(connstr);
        }

        /// <summary>
        /// Executing a SQL query
        /// </summary>
        /// <param name="command"> SQL command to run </param>
        /// <returns>
        /// SQLDataReader which contains rows or null if any error
        /// </returns>
        public static SqlDataReader ExecuteQuery(string command)
        {
            try
            {
                SqlConnection conn = NewConnection();
                conn.Open();

                var cmd = new SqlCommand(command, conn);

                return cmd.ExecuteReader();
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

        /// <summary>
        /// Executing a Transact-SQL statement
        /// </summary>
        /// <param name="command"> SQL command to run </param>
        /// <returns>
        /// Number of rows affected or -1 if any error
        /// </returns>
        public static int ExecuteNonQuery(string command)
        {
            try
            {
                SqlConnection conn = NewConnection();
                conn.Open();

                var cmd = new SqlCommand(command, conn);

                return cmd.ExecuteNonQuery();
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
                return -1;
            }
        }

        /// <summary>
        /// Making response to send back to frontend for Transact-SQL
        /// </summary>
        /// <param name="status"> status code received from query </param>
        /// <returns>
        /// StatusCode class's object
        /// </returns>
        public static StatusCode ResponseMaker(int status)
        {
            if (status == -1)
                return new StatusCode()
                {
                    status = "Error"
                };
            else if (status == 0)
                return new StatusCode()
                {
                    status = "Not Found"
                };
            else
                return new StatusCode()
                {
                    status = "Done"
                };
        }
    }

    /// <summary>
    /// StatusCode class to send to frontend as JSON
    /// </summary>
    public class StatusCode
    {
        public string status { get; set; }
    }
}