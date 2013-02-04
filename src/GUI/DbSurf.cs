using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Evemu_DB_Editor.src;

namespace Evemu_DB_Editor
{
    public partial class DbSurf : Form
    {
        xHelp xhelp;// = new xHelp();

        public DbSurf()
        {
            InitializeComponent();
            PopView();
            xhelp = new xHelp(main.DatabaseName);
        }

        private void PopView()
        {
            lsvTables.Items.Clear();
            ListViewItem[] items = new ListViewItem[DBConnect.Tables().Rows.Count];
            int count = 0;
            foreach (DataRow record in DBConnect.Tables().Rows)
            {
                string SQLQueryCount = "SELECT count(*) FROM " + record[0].ToString();
                DataTable SQLData = DBConnect.AQuery(SQLQueryCount);

                ListViewItem item2 = new ListViewItem(new string[2] { record[0].ToString(), SQLData.Rows[0].ItemArray[0].ToString() });
                items[count] = item2;
                count++;
            }
            lsvTables.Items.AddRange(items);
        }

        private void lsbTabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            //'0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP
            try
            {
                if (lsvTables.SelectedItems.Count == 0)
                {
                    DataTable chgTable = ((DataTable)dgvTabella.DataSource).GetChanges();
                    if (chgTable != null)
                    {
                        //ask
                        if (MessageBox.Show("Table was modified, wont apply change?", "Apply Modified", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DBConnect.RefreshTableInDB(chgTable);
                            PopView();
                        }
                    }
                    BeforeTableSelect();//save
                }
                else
                {
                    try
                    {
                        dgvTabella.DataSource = DBConnect.GetItemsTable(lsvTables.SelectedItems[0].Text);
                        DataGridViewSetColumns();
                        this.Text = ((DataTable)dgvTabella.DataSource).Namespace;
                    }
                    catch 
                    { 
                        //MessageBox.Show("Error incoerenza."); 
                    }//exception incoerenza
                    AfterTableSelect();//load
                }
            }
            catch (DBConcurrencyException erconc) { MessageBox.Show("Error: " + erconc.ToString()); }
            catch (Exception err) { MessageBox.Show("Error: " + err.ToString()); }
        }

        private void DataGridViewSetColumns()
        {
            foreach (DataGridViewColumn colonna in dgvTabella.Columns)
            {
                DataGridViewCellStyle deCel = new DataGridViewCellStyle();
                colonna.ReadOnly = DBConnect.StructuredField.AutoIncrement(((DataTable)dgvTabella.DataSource).Namespace, colonna.HeaderText);
                if (colonna.ReadOnly == true)
                {
                    deCel.BackColor=Color.Gray;
                }
                if (!DBConnect.StructuredField.Null(((DataTable)dgvTabella.DataSource).Namespace, colonna.HeaderText) && !colonna.ReadOnly)
                {
                    deCel.BackColor = Color.Red;
                }
                if (DBConnect.StructuredField.PrimaryKey(((DataTable)dgvTabella.DataSource).Namespace, colonna.HeaderText) && !colonna.ReadOnly)
                {
                    deCel.BackColor = Color.Gold;
                }

                colonna.DefaultCellStyle = deCel;
            }
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            //MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context.ToString() == "Formatting, Display")
            {
                //some fields got this error...boolean or tinyint dont know
                string[] fields = (xhelp.tableHelp.RecuperaRiga(TracePositionTable(this.Text))[1]).Split(',');//posTable
                foreach (string nomeF in fields)
                {
                    MySql.Data.MySqlClient.MySqlDbType typefield = DBConnect.StructuredField.FieldType(xhelp.tableHelp.RecuperaRiga(TracePositionTable(this.Text))[0], nomeF);//posTable
                    if(typefield==MySql.Data.MySqlClient.MySqlDbType.Timestamp)
                        dgvTabella.Columns[nomeF].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                }
            }
            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        #region XHelp - RichTextBox
        private void BeforeTableSelect()
        {
            if (richDesc != rchTextTable.Rtf)
            {
                //save xhelp.xml
                string fields = "";
                foreach (DataGridViewColumn colon in dgvTabella.Columns)
                {
                    fields += colon.HeaderText + ",";
                }
                fields = fields.Substring(0, fields.Length - 1);
                string[] elemXhelp = new string[3] { ((DataTable)dgvTabella.DataSource).Namespace, fields, Base64Coding(rchTextTable.Rtf) };
                if (xhelp.tableHelp.AggiornaTable(TracePositionTable(this.Text), elemXhelp))//posTable
                {
                    xhelp.tableHelp.SalvaTable(TracePositionTable(this.Text));//posTable
                }
            }
        }

        private int TracePositionTable(string nomeTable)
        {
            int posTable = -1;
            for (int i = 0; i < xhelp.tableHelp.DataRecordDeep; i++)
            {
                if (xhelp.tableHelp.RecuperaRiga(i)[0] == nomeTable)
                {
                    posTable = i;
                    i = xhelp.tableHelp.DataRecordDeep;
                }

            }
            return posTable;
        }

        private string richDesc = "";
        //private int posTable = -1;
        private void AfterTableSelect()
        {
            //load xhelp.xml
            rchTextTable.Enabled = true;
            int posTable = TracePositionTable(lsvTables.SelectedItems[0].Text);
            
            if (posTable > -1)
            {
                rchTextTable.Rtf = RTFCoding(xhelp.tableHelp.RecuperaRiga(posTable)[2]);
            }
            else
            {
                //need xhelp of this field so...generate
                string fields = "";
                string prertf = "{\\rtf1\\ansi\\ansicpg1252\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Franklin Gothic Medium;}{\\f1\\fnil\\fcharset0 Microsoft Sans Serif;}}";
                prertf += "{\\colortbl ;\\red0\\green0\\blue160;}";
                string fieldsRtxt = prertf + "\\viewkind4\\uc1\\pard\\cf1\\lang1040\\f0\\fs24" + ((DataTable)dgvTabella.DataSource).Namespace + "\\cf0\\f1\\fs17\\par" + "\\par" + "\\par";
                foreach (DataGridViewColumn colon in dgvTabella.Columns)
                {
                    fields += colon.HeaderText + ",";
                    fieldsRtxt += "\\ul\\b\\i\\fs20 " + colon.HeaderText + " : " + "\\ulnone\\b0\\i0\\fs17" + "\\par" + "\\par";
                }
                rchTextTable.Rtf = fieldsRtxt + "}";
                //rchTextTable.Text = fieldsRtxt;
                fields = fields.Substring(0, fields.Length - 1);
                string[] elemXhelp = new string[3] { ((DataTable)dgvTabella.DataSource).Namespace, fields, Base64Coding(rchTextTable.Rtf) };
                xhelp.tableHelp.RecordTable(elemXhelp);
                xhelp = new xHelp(main.DatabaseName);
            }
            richDesc = rchTextTable.Rtf;
        }

        private string Base64Coding(string richtext)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            string storeMe = System.Convert.ToBase64String(enc.GetBytes(richtext));

            return storeMe;
            //System.Windows.Forms.MessageBox.Show(storeMe);
        }

        private string RTFCoding(string xmlText)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] result = System.Convert.FromBase64String(xmlText);
            string RTFString = enc.GetString(result);

            return RTFString;
            //System.Windows.Forms.MessageBox.Show(RTFString);
        }

        private void rchTextTable_SelectionChanged(object sender, EventArgs e)
        {
            if (rchTextTable.SelectionFont != null)
            {
                BoldToolStripButton.Checked = rchTextTable.SelectionFont.Bold;
                ItalicToolStripButton.Checked = rchTextTable.SelectionFont.Italic;
                UnderlineToolStripButton.Checked = rchTextTable.SelectionFont.Underline;
            }
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            if (rchTextTable.SelectionFont == null)
            {
                return;
            }

            FontStyle style = rchTextTable.SelectionFont.Style;

            if (rchTextTable.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;

            }
            rchTextTable.SelectionFont = new Font(rchTextTable.SelectionFont, style);
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {
            if (rchTextTable.SelectionFont == null)
            {
                return;
            }

            FontStyle style = rchTextTable.SelectionFont.Style;

            if (rchTextTable.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            rchTextTable.SelectionFont = new Font(rchTextTable.SelectionFont, style);
        }

        private void ItalicToolStripButton_Click(object sender, EventArgs e)
        {
            if (rchTextTable.SelectionFont == null)
            {
                return;
            }
            FontStyle style = rchTextTable.SelectionFont.Style;

            if (rchTextTable.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            rchTextTable.SelectionFont = new Font(rchTextTable.SelectionFont, style);
        }

        private void FontColorToolStripButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rchTextTable.SelectionColor = colorDialog1.Color;
            }
        }

        private void FontToolStripButton_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                rchTextTable.SelectionFont = fontDialog1.Font;
            }
        }

        private void BulletsToolStripButton_Click(object sender, EventArgs e)
        {
            if (rchTextTable.SelectionFont == null)
            {
                return;
            }

            if (rchTextTable.SelectionBullet)
            {
                rchTextTable.SelectionBullet = false;
            }
            else
            {
                rchTextTable.SelectionBullet = true;
            }
        }
        #endregion
    }

}
