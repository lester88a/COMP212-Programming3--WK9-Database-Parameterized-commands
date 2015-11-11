using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_commands
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * PARAMETERIZED COMMANDS
             * 
             * spaceholders in sql commands
             * to be filled during the run-time resuable sql run-time
             * 
             * //do not do: SqlCommand cmd = new SqlCommand("Select * from customer where city = 'cityName'" + "");
             * 
             * Instead of dynamically building a string, please use parameters.
             * 
             * Steps:
             * 1. construct the sql command string
             * 2.declare sql parameter object, assign ralues as apporpriate
             * 3. assign the SqlParameter object to the sqlCommand object's parameters
             * 
             * Snippet
             * 1. declare command object with parameter
             * 
             */

            //Snippet
            // 1. declare command object with parameter
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand("Select * from customers where city = @city", conn);

            //2. declaring a sqlParameter object
            SqlParameter param = new SqlParameter();
            //param.ParameterName = "@city";
            //string inputCity = null;
            //param.Value = inputCity;

            //3. add new parameter to command obnect
            cmd.Parameters.Add(param);

            //Puting everything together (example)
            //adding parameter to queries
            /*
             * using System;
             * using System.Data;
             * using System.Data.SqlClient;
             */
            SqlConnection connection = null;
            SqlDataReader reader = null;
            string inputCity = "London";

            try
            {
                connection = new SqlConnection("data source=THINKPAD-PC;initial catalog=northwind;Integrated Security=true");
                connection.Open();
                //1. declare command object with parameter
                SqlCommand command = new SqlCommand("Select * from customers where city = @city",connection);
                
                //2.define parameter
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@city";
                parameter.Value = inputCity;

                //3. add new parameter to command
                command.Parameters.Add(parameter);

                //4.get the data stream
                reader = command.ExecuteReader();

                //5.write each record
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}", reader["companyName"], reader["ContactName"]);//also can use reader[1], reader[2]
                }
            }
            finally
            {
                //close the reader
                if (reader != null)
                {
                    reader.Close();
                }
                if (connection!=null)
                {
                    connection.Close();
                }
            }

        }
    }
}
