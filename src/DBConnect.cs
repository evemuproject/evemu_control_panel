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
        private static MySqlDataAdapter adapter = new MySqlDataAdapter();

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

        #region Dump
        //Backup
        public static bool Backup(string username, string password, string host, string tavolaNome)
        {
            try
            {
                string tmestr = "";

                tmestr = tavolaNome + "-" + DateTime.Now.ToShortDateString() + ".sql";
                tmestr = tmestr.Replace("/", "-");
                tmestr = Environment.CurrentDirectory + "\\Dump\\" + tmestr;
                if (File.Exists(tmestr))
                {
                    if (MessageBox.Show("Dump file " + tmestr + " exist...continue?", "File Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }
                StreamWriter file = new StreamWriter(tmestr);
                try
                {
                    ProcessStartInfo proc = new ProcessStartInfo();
                    string cmd = string.Format(@"-u{0} -p{1} -h{2} evemu {3}", username, password, host, tavolaNome);
                    proc.FileName = main.ini.IniReadValue("Path", "mySQLPath") + "\\mysqldump";
                    proc.RedirectStandardInput = false;
                    proc.RedirectStandardOutput = true;
                    proc.Arguments = cmd;
                    proc.UseShellExecute = false;
                    Process p = Process.Start(proc);
                    string res;
                    res = p.StandardOutput.ReadToEnd();
                    file.WriteLine(res);
                    p.WaitForExit();
                    file.Close();
                    p.Close();
                    return true;
                }
                catch (Exception errf)
                {
                    file.Close();
                    File.Delete(tmestr);
                    MessageBox.Show(errf.Message);
                    return false;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error!");
                return false;
            }
        }

        //Restore
        public static bool Restore(string username, string password, string host, string dumpFile)
        {
            StreamReader file = new StreamReader(dumpFile);
            try
            {
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = main.ini.IniReadValue("Path", "mySQLPath") + "\\mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;

                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", username, password, host, "evemu");
                psi.UseShellExecute = false;
                Process p = Process.Start(psi);
                p.StandardInput.Write(input);
                p.StandardInput.Close();
                p.WaitForExit();
                p.Close();
                return true;
            }
            catch (Exception errio)
            {
                file.Close();
                MessageBox.Show(errio.Message);
                return false;
            }
        }

        //Tables
        public static DataTable Tables()
        {
            DataTable datatable = new DataTable();
            if (isConnectionOpen)
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter("show tables", connection);
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

        //Get all items from database into datatable
        public static DataTable GetItemsTable(string nTable)
        {
            try
            {
                string query = "select * from " + nTable;

                adapter = new MySqlDataAdapter(query, connection);
                DataSet DS = new DataSet();

                adapter.Fill(DS);
                DS.Tables[0].Namespace = nTable;
                DataTable table = DS.Tables[0];

                //take a look no work propely ever
                #region adapter commands
                try
                {
                    if (isConnectionOpen)
                    {
                        // Set the UPDATE command and parameter.
                        string formaCc = "";
                        for (int i = 1; i < table.Columns.Count; i++)
                        {
                            formaCc += table.Columns[i].Caption + "=?" + table.Columns[i].Caption + ", ";
                        }
                        formaCc = formaCc.Substring(0, formaCc.Length - 2);

                        adapter.UpdateCommand = new MySqlCommand("UPDATE " + nTable + " SET " + formaCc + " WHERE " + table.Columns[0].Caption + "=?" + table.Columns[0].Caption + ";", connection);
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            adapter.UpdateCommand.Parameters.Add("?" + table.Columns[i].Caption, StructuredField.FieldType(nTable, table.Columns[i].Caption), StructuredField.FieldSize(nTable, table.Columns[i].Caption), table.Columns[i].Caption);
                        }
                        adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;


                        // Set the INSERT command and parameter.
                        string formaCcIns = "";
                        string formaCvIns = "";
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            formaCcIns += "?" + table.Columns[i].Caption + ", ";
                            formaCvIns += table.Columns[i].Caption + ", ";
                        }
                        formaCcIns = formaCcIns.Substring(0, formaCcIns.Length - 2);
                        formaCvIns = formaCvIns.Substring(0, formaCvIns.Length - 2);
                        adapter.InsertCommand = new MySqlCommand("INSERT INTO " + nTable + " (" + formaCvIns + ") VALUES (" + formaCcIns + ");", connection);
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            adapter.InsertCommand.Parameters.Add("?" + table.Columns[i].Caption, StructuredField.FieldType(nTable, table.Columns[i].Caption), StructuredField.FieldSize(nTable, table.Columns[i].Caption), table.Columns[i].Caption);
                        }
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                        // Set the DELETE command and parameter.
                        adapter.DeleteCommand = new MySqlCommand("DELETE FROM " + nTable + " WHERE " + table.Columns[0].Caption + "=?" + table.Columns[0].Caption + ";", connection);
                        adapter.DeleteCommand.Parameters.Add("?" + table.Columns[0].Caption, MySqlDbType.Int16, 4, table.Columns[0].Caption);
                        adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                    }
                    else
                    {
                        MessageBox.Show("Open connection before!");
                    }
                }
                catch (Exception err) { MessageBox.Show("Error: " + err.ToString()); }
                #endregion

                return DS.Tables[0];
            }
            catch (Exception err) { MessageBox.Show("Error: " + err.ToString()); return null; }
        }

        //add change in db
        public static void RefreshTableInDB(DataTable table)
        {
            adapter.Update(table);
        }
        #endregion

        //some info from mysql db rows
        public static class StructuredField
        {
            //not completed
            public static MySqlDbType FieldType(string tableName, string fieldName)
            {
                DataTable tabInf = AQuery("SHOW FIELDS FROM " + tableName + " WHERE Field='" + fieldName + "';");
                string cell = tabInf.Rows[0].ItemArray[1].ToString();
                string[] splCell = cell.Split('(');
                switch (splCell[0])
                {
                    case "int":
                    case "bigint":
                    case "tinyint":
                    case "smallint":
                        return MySqlDbType.Int64;
                    case "varchar":
                        return MySqlDbType.VarChar;
                    case "text":
                        return MySqlDbType.Text;
                    case "blob":
                        return MySqlDbType.Blob;
                    case "mediumtext":
                        return MySqlDbType.MediumText;
                    case "timestamp":
                        return MySqlDbType.Timestamp;

                }
                //expressions not evaluated are binary...but this is not right way!!!
                return MySqlDbType.Binary;
            }

            public static int FieldSize(string tableName, string fieldName)
            {
                DataTable tabInf = AQuery("SHOW FIELDS FROM " + tableName + " WHERE Field='" + fieldName + "';");
                string cell = tabInf.Rows[0].ItemArray[1].ToString();
                int vspl = 255;
                if (cell.Contains('('))
                {
                    string[] splCell = cell.Split('(');
                    int dspc = splCell[1].LastIndexOf(')');
                    string nSpl = splCell[1].Substring(0, dspc);
                    //some ERROR with decimal format
                    vspl = Convert.ToInt32(nSpl);
                }
                return vspl;
            }

            public static bool AutoIncrement(string tableName, string fieldName)
            {
                DataTable tabInf = AQuery("SHOW FIELDS FROM " + tableName + " WHERE Field='" + fieldName + "';");
                string cell = tabInf.Rows[0].ItemArray[5].ToString();
                if (cell == "")
                    return false;
                else
                    return true;
            }

            public static bool Null(string tableName, string fieldName)
            {
                DataTable tabInf = AQuery("SHOW FIELDS FROM " + tableName + " WHERE Field='" + fieldName + "';");
                string cell = tabInf.Rows[0].ItemArray[2].ToString();
                if (cell == "NO")
                    return false;
                else
                    return true;
            }

            public static bool PrimaryKey(string tableName, string fieldName)
            {
                DataTable tabInf = AQuery("SHOW FIELDS FROM " + tableName + " WHERE Field='" + fieldName + "';");
                string cell = tabInf.Rows[0].ItemArray[3].ToString();
                if (cell == "PRI")
                    return true;
                else
                    return false;
            }
        }

    }
}
