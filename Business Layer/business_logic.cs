using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;
using MySql.Data.MySqlClient;

namespace Business_Layer
{
    public class BusinessLogic
    {
        private DataAccess objConn = new DataAccess();

        public DataTable BookingsDataTable()
        {
            DataTable dtBookings = new DataTable();

            var conn = objConn.CreateSQLConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn; // SETTING THE CONNECTION FOR THE COMMAND
            cmd.CommandText = "Select g.FirstName, g.LastName, g.Address, b.RoomNo, b.CheckIn, b.CheckOut, b.Rate, b.DepositPaid from guest g inner join bookings b on g.Id = b.GuestId;"; // SETTING THE QUERY FOR THE COMMAND

            MySqlDataReader sqlRead = cmd.ExecuteReader(); // USING THIS TO READ THE RESULTS FROM THE QUERY 
            try
            {
                if (sqlRead.HasRows) // CHECKING IF THE QUERY HAS ROWS
                {
                    dtBookings.Load(sqlRead);
                }
            }
            catch (Exception ex)
            {
                dtBookings.Rows.Add(ex.Message); // IN THE EVENT THAT THERE IS AN ERROR I WRITE THAT TO THE DATATABLE
            }
            finally
            {
                sqlRead.Close(); // CLOSE THE READER OTHERWISE IT BREAKS OR WHATEVER
            }
            conn.Close();


            return dtBookings;
        }

        public string CreateBooking(string name, string lastname, string address, DateTime checkin, DateTime checkout)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = objConn.CreateSQLConnection();

            cmd.CommandText = "Guest_Create";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name_in", name);
            cmd.Parameters.AddWithValue("@lastname_in", lastname);
            cmd.Parameters.AddWithValue("@address_in", address);
            cmd.Parameters.AddWithValue("@address_in", address);
            cmd.Parameters.AddWithValue("@address_in", address);
            cmd.Parameters.AddWithValue("@address_in", address);

            MySqlTransaction myTrans;

            myTrans = cmd.Connection.BeginTransaction();
            cmd.Transaction = myTrans;

            try
            {
                cmd.ExecuteNonQuery();
                myTrans.Commit();
                cmd.Connection.Close();
                return "Success";
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                cmd.Connection.Close();
                return ex.Message;

            }
        }

    }
}
