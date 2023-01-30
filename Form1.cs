using MySql.Data.MySqlClient;
using System.Data;

namespace MySqlCrud
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=localhost;Database=playerdb;Uid=root;Pwd=1234";
        int playerID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using(MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("PlayerAddOrEdit", mysqlCon);
                mySqlCmd.CommandType =CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_Playerid", playerID);
                mySqlCmd.Parameters.AddWithValue("_PlayerName", txtplayername.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Club", txtplayerclub.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Desciption", txtplayerdesc.Text.Trim());
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Submitted Successfully");
                GridFill();
            }

        }

        void GridFill()
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqldata = new MySqlDataAdapter("PlayerViewAll", mysqlCon);
                sqldata.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtblPlayer= new DataTable();
                sqldata.Fill(dtblPlayer);
                dgvPlayer.DataSource = dtblPlayer;
                dgvPlayer.Columns[0].Visible= false;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("PlayerDeleteByid", mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_Playerid", playerID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                Clear();
                GridFill();
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqldata = new MySqlDataAdapter("PlayerSearch", mysqlCon);
                sqldata.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldata.SelectCommand.Parameters.AddWithValue("_SearchValue", txtsearch.Text);
                DataTable dtblPlayer = new DataTable();
                sqldata.Fill(dtblPlayer);
                dgvPlayer.DataSource = dtblPlayer;
                dgvPlayer.Columns[0].Visible= false;
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            GridFill();
        }

        void Clear()
        {
            txtplayername.Text = txtplayerclub.Text = txtplayerdesc.Text = txtsearch.Text = "";
            playerID = 0;
            btnSave.Text = "Save";
            btnDelete.Enabled= false;
        }

        private void dgvPlayer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvPlayer.CurrentRow.Index != -1)
            {
                txtplayername.Text = dgvPlayer.CurrentRow.Cells[1].Value.ToString();
                txtplayerclub.Text = dgvPlayer.CurrentRow.Cells[2].Value.ToString();
                txtplayerdesc.Text = dgvPlayer.CurrentRow.Cells[3].Value.ToString();
                playerID = Convert.ToInt32(dgvPlayer.CurrentRow.Cells[0].Value.ToString());
                btnSave.Text = "Update";
                btnDelete.Enabled = Enabled;
            }

        }

        private void txtplayerclub_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtplayername_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
