using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Access
{
    public class DataAccess
    {
        public MySqlConnection CreateSQLConnection() // CREATING A CLASS TO HOUSE MY CONNECTION METHODS
        {
            // CREATING VARIABLES TO CREATE THE CONNECTION STRING
            // THIS IS ON MY SERVER SO IT CAN RUN WITHOUT AN ISSUE
            MySqlConnection connection;
            string server;
            string database;
            string uid;
            string password;

            // MY SERVER DETAILS, YOU CAN STEAL THESE KINDA
            server = "197.242.145.246"; // SERVER IP ADDRESS
            database = "hotel"; // DB NAME
            uid = "lunghekani"; // USERNAME
            password = "sa"; // PASSWORD AND YES I AM AWESOME


            string connectionString;
            // BUILDING THE CONNECTION STRING
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            // CREATING A MYSQL CONNECTION TO CONDUCT OPERATIONS
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

            }
            catch (Exception)
            {
                MessageBox.Show("Contact Lunghi The Server Could be Down or you have no internet");// NO REALLY CALL HIM :)
            }



            return connection; // RETURNING THE CONNECTION
        }
    }

}
