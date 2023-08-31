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
using System.Windows.Input;

namespace NotKayıtSistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection("Data Source=KLMNTB038;Initial Catalog=DbNotKayit;Persist Security Info=True;User ID=sa; password=Klm_1234");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
            baglanti.Open();
            // SqlCommand durum1= new SqlCommand("select count(*) from TBLDERS where durum=1",baglanti);
            string baglanti1 = "Data Source=KLMNTB038;Initial Catalog=DbNotKayit;Persist Security Info=True;User ID=sa; password=Klm_1234";

            string durum1 = "select count(*) from TBLDERS where durum=1";
            using (SqlConnection baglanti = new SqlConnection(baglanti1))
            {
                baglanti.Open();
                using (SqlCommand command = new SqlCommand(durum1.ToString(), baglanti))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        string value = result.ToString();
                        lblGeçenSayisi.Text = value; // Label'a yazdırma
                    }
                }
            }
            string durum0 = "select count(*) from TBLDERS where durum=0";
            using (SqlConnection baglanti = new SqlConnection(baglanti1))
            {
                baglanti.Open();
                using (SqlCommand command = new SqlCommand(durum0.ToString(), baglanti))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        string value = result.ToString();
                        
                        lblKalanSayisi.Text = value; // Label'a yazdırma
                    }
                }
            }
            /*double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(txtSinav1.Text);*/
            string sinifOrt = "select AVG(ORTALAMA) from TBLDERS";
           
            using (SqlConnection baglanti = new SqlConnection(baglanti1))
            {
                baglanti.Open();
                using (SqlCommand command = new SqlCommand(sinifOrt.ToString(), baglanti))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        string value = result.ToString();
                        double num = double.Parse(value);
                        lblOrtalama.Text = num.ToString("0.00"); // Label'a yazdırma
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values" +
                "(@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", mskNumara.Text);
            komut.Parameters.AddWithValue("@P2", txtAd.Text);
            komut.Parameters.AddWithValue("@P3", txtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
 

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1=Convert.ToDouble(txtSinav1.Text);
            s2=Convert.ToDouble(txtSinav2.Text);
            s3=Convert.ToDouble(txtSinav3.Text);

            ortalama = (s1+ s2+s3)/3;
            lblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "Geçti";
            }
            else
            {
                durum = "Kaldı";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1, OGRS2=@P2, OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1",txtSinav1.Text);
            komut.Parameters.AddWithValue("@P2",txtSinav2.Text);
            komut.Parameters.AddWithValue("@P3", txtSinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(lblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi.");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

      
    }
}
