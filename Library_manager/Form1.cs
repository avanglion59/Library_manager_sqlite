using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Common;
//using System.Data.SqlClient;

namespace Library_manager
{
    public partial class Form1 : Form
    {
        private SQLiteDataAdapter da;
        private DataTable mDataTable;
        //private Entry entry { get; set; }        
        //public Form1(Entry _entry)
        public Form1()
        {           
            InitializeComponent();
            //entry = _entry;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Entry entry = new Entry();
            entry.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            /* SQLiteDataReader */

            /*SQLiteDataReader sqlite_datareader;
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Adress FROM Clients";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            foreach (DbDataRecord record in sqlite_datareader)
            {
                textBox1.AppendText(sqlite_datareader["Adress"].ToString() + '\n');
            }
            sqlite_conn.Close();*/

            /* SQLiteDataAdapter */

            SQLiteConnection sqlite_conn = Globals.getSQLiteConnection();

            SQLiteCommand sqliteSelectCommand;
            sqliteSelectCommand = sqlite_conn.CreateCommand();
            sqliteSelectCommand.CommandText = "SELECT id, Adress, Familiya FROM Clients";

            SQLiteCommand sqliteUpdateCommand;
            sqliteUpdateCommand = sqlite_conn.CreateCommand();
            sqliteUpdateCommand.CommandText = "UPDATE Clients SET id = @id, Adress = @Adress, Familiya = @Familiya WHERE id = @oldId";

            sqliteUpdateCommand.Parameters.Add(new SQLiteParameter("@id", DbType.Int16, "id"));
            sqliteUpdateCommand.Parameters.Add(new SQLiteParameter("@Adress", DbType.String, "Adress"));
            sqliteUpdateCommand.Parameters.Add(new SQLiteParameter("@Familiya", DbType.String, "Familiya"));
            sqliteUpdateCommand.Parameters.Add(new SQLiteParameter("@oldId", DbType.Int16, "id"));
            sqliteUpdateCommand.Parameters["@oldId"].SourceVersion = DataRowVersion.Original;


            mDataTable = new DataTable();
            da = new SQLiteDataAdapter();
            da.SelectCommand = sqliteSelectCommand;
            da.UpdateCommand = sqliteUpdateCommand;
            da.Fill(mDataTable);
            dataGridView1.DataSource = mDataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(mDataTable);
        }       
    }
}
