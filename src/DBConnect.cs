using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Evemu_DB_Editor.src
{

    static class DBConnect
    {
        private static MySqlConnection connection;
        private static bool isConnectionOpen = false;

        public static bool isConnected() { return isConnectionOpen; }

        //open connection to database
        public static void OpenConnection()
        {
            string[] dbcon = XMLReader.xmlLoad();
            string connectionString = "SERVER=" + dbcon[0] + ";" + "DATABASE=" +
            dbcon[4] + ";" + "UID=" + dbcon[1] + ";" + "PASSWORD=" + dbcon[2] + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                isConnectionOpen = true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }

                isConnectionOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void OpenConnection(string host, string user, string pass, string dbName)
        {
            string connectionString = "SERVER=" + host + ";" + "DATABASE=" +
            dbName + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                isConnectionOpen = true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }

                isConnectionOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Close connection
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // Simple Query: Handles INSERT, UPDATE, DELETE, ALTER and others i may have forgotten...
        public static int SQuery(string query)
        {
            try
            {
                if (isConnectionOpen)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    return cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Please connect to DB first");
                    return -1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
                return -1;
            }
        }

        // Advanced Query: Handles Multi-SELECT and return the values as a DataTable
        public static DataTable AQuery(string query)
        {
            DataTable datatable = new DataTable();
            if (isConnectionOpen)
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(datatable);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception: " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("Please connect to DB first");
            }

            return datatable;
        }

        public static Int64 LastRowID() {
            if (!isConnectionOpen) {
                return 0;
            }
            string strSQLSelect = "SELECT @@IDENTITY AS 'LastID'";
            MySqlCommand dbcSelect = new MySqlCommand(strSQLSelect, connection);
            MySqlDataReader dbrSelect = dbcSelect.ExecuteReader(); 

            dbrSelect.Read(); 
            Int64 val = Int64.Parse(dbrSelect.GetValue(0).ToString());
            dbrSelect.Dispose();
            return val;
        }

        //Backup
        public static void Backup()
        {

        }

        //Restore
        public static void Restore()
        {

        }

    }
}
