using System;
using System.Collections.Generic;
using System.Data;
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
                Console.WriteLine("companyName, \t\t\t ContactName");
                //5.write each record
                while (reader.Read())
                {
                    
                    Console.WriteLine("{0}, \t\t{1}", reader["companyName"], reader["ContactName"]);//also can use reader[1], reader[2]
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
            /**********************************************************************************************************/

            //Using stored procedures
            //Steps:
            //1. create a command object identifying stored procedure
            //                          store procedure name: xyz
            SqlCommand sqlcmd = new SqlCommand("xyz", connection);

            //2. set the command object to let the compailer know that is has to excute a stored procedure (in place of an sql command)
            sqlcmd.CommandType = CommandType.StoredProcedure;

            //sending parameters to stored procedures:
            //1. create a command object identifying stored procedure
            SqlCommand sqlcmd2 = new SqlCommand("xyz", connection);
            //2. set the command object so it knows it has to execute a procedure
            sqlcmd2.CommandType = CommandType.StoredProcedure;
            //3. add the parameter to command which will be passed to the store procedure
            string custID = "FURIB";
            sqlcmd2.Parameters.Add(new SqlParameter("@customerID", custID));

            //excute


        }
    }
}
