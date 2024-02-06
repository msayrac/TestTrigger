using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace TestTrigger
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		SqlConnection connection = new SqlConnection("Data Source=msyc;Initial Catalog=Test;Integrated Security=True");


		void listele()
		{
			connection.Open();
			SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKITAPLAR", connection);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dataGridView1.DataSource = dt;
			connection.Close();
		}

		void sayac()
		{
			connection.Open();
			SqlCommand command = new SqlCommand("Select * From TBLSAYAC", connection);
			SqlDataReader dr = command.ExecuteReader();
			while (dr.Read())
			{
				LblKitapAdet.Text = dr[0].ToString();
			}
			connection.Close();

		}
		private void Form1_Load(object sender, EventArgs e)
		{
			listele();
			sayac();

		}

		private void BtnEkle_Click(object sender, EventArgs e)
		{
			connection.Open();

			SqlCommand command = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) values (@p1,@p2,@p3,@p4,@p5)", connection);
			command.Parameters.AddWithValue("@p1", TxtAD.Text);
			command.Parameters.AddWithValue("@p2", TxtYazar.Text);
			command.Parameters.AddWithValue("@p3", TxtSayfa.Text);
			command.Parameters.AddWithValue("@p4", TxtYayinevi.Text);
			command.Parameters.AddWithValue("@p5", TxtTur.Text);

			command.ExecuteNonQuery();
			connection.Close();
			listele();
			sayac();



		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int secilen = dataGridView1.SelectedCells[0].RowIndex;

			TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
			TxtAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
			TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
			TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
			TxtYayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
			TxtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();



		}

		private void BtnSil_Click(object sender, EventArgs e)
		{
			connection.Open();

			SqlCommand command = new SqlCommand("Delete from TBLKITAPLAR where ID=@p1", connection);

			command.Parameters.AddWithValue("@p1",TxtID.Text);
			command.ExecuteNonQuery();
			connection.Close();
			listele();
			sayac();



		}
	}
}
