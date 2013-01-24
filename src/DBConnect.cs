using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
            isConnectionOpen = false;
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
            if (!isConnectionOpen) throw new NotConnectedException();
            try 
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                return cmd.ExecuteNonQuery();
            } 
            catch(Exception e) 
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            return -1;
        }

        // Advanced Query: Handles Multi-SELECT and return the values as a DataTable
        public static DataTable AQuery(string query)
        {            
            if (!isConnectionOpen) throw new NotConnectedException();
            DataTable datatable = new DataTable();
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(datatable);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            return datatable;
        }

        // Advanced Query: Handles SELECT and returns one first value from it
        public static object AQueryScalar(string query)
        {
            if (!isConnectionOpen) throw new NotConnectedException();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object res = cmd.ExecuteScalar();
                if( res is DBNull) return null;
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            return null;
            
        }

        public static Int64 LastRowID() 
        {
            object o = AQueryScalar("SELECT LAST_INSERT_ID()");
            if(o==null) throw new Exception("Cannot get last row id!");
            return (Int64)o;
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

    class NotConnectedException : DbException {
        public NotConnectedException() :base("Not connected to database") {}
         
        public NotConnectedException(string message) : base(message) {}

        public NotConnectedException(string message, Exception innerException) : base(message, innerException) { }
        public NotConnectedException(string message, int errorCode) :base(message, errorCode) {}

    }
}
