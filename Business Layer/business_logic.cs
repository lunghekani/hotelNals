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
        public const int lowSeason = 550;
        public const int MidSeason = 750;
        public const int HighSeason = 995;
        public DataTable BookingsDataTable()
        {
            DataTable dtBookings = new DataTable();

            var conn = objConn.CreateSQLConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn; // SETTING THE CONNECTION FOR THE COMMAND
            cmd.CommandText = "Select FirstName, LastName, Address, RoomNo, CheckIn, CheckOut, Rate, DepositPaid from bookings;"; // SETTING THE QUERY FOR THE COMMAND

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

        public string CreateBooking(string name, string lastname, string address, DateTime checkin, DateTime checkout, int rate, string roomNo, int deposit)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = objConn.CreateSQLConnection();

            cmd.CommandText = "CreateBooking";
            cmd.CommandType = CommandType.StoredProcedure;

           
            cmd.Parameters.AddWithValue("@firstnname", name);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@checkin", checkin);
            cmd.Parameters.AddWithValue("@checkout", checkout);
            cmd.Parameters.AddWithValue("@rate", rate);
            cmd.Parameters.AddWithValue("@roomno", roomNo);
            cmd.Parameters.AddWithValue("@deposit", deposit);
           

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

        public int GetSeason(DateTime date)
        {
            DateTime lowStart = DateTime.Parse("01/12/2021");
            DateTime lowEnd = DateTime.Parse("07/12/2021");

            DateTime midStart = DateTime.Parse("08/12/2021");
            DateTime midEnd = DateTime.Parse("15/12/2021");

            DateTime highStart = DateTime.Parse("16/12/2021");
            DateTime highEnd = DateTime.Parse("31/12/2021");
            if (date > lowStart && date < lowEnd)
            {
                return lowSeason;
            }
            else if (date > midStart && date < midEnd)
            {
                return MidSeason;

            } else if (date > highStart && date < highEnd)
            {
                return HighSeason;

            }
            else
            {
                return 0;
            }



        }
    }
}
