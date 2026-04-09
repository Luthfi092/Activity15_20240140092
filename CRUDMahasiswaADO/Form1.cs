using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        private readonly MySqlConnection conn;
        // Sesuaikan password jika ada
        // Ganti dbakademiado menjadi nama yang benar di MySQL kamu
        private string connectionString = "Server=localhost;Database=dbakademikado;UID=root;Password=212223;";

        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection(connectionString);
        }

        // ================== INSERT ==================
        private void btnTambah_Click(object sender, EventArgs e)
        { 
            try
            {
                conn.Open();
                // Saran: Sebutkan nama kolom secara eksplisit agar lebih aman
                // Contoh jika di DB namanya jeniskelamin (tanpa underscore)
                // Kita hilangkan (nim, nama, dll), langsung VALUES
                string query = "INSERT INTO mahasiswa VALUES (@nim, @nama, @jk, @tgl, @alamat, @kode)";
                MySqlCommand cmd = new MySqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@nim", label1.Text);
                cmd.Parameters.AddWithValue("@nama", label2.Text);
                cmd.Parameters.AddWithValue("@jk", label3.Text);
                cmd.Parameters.AddWithValue("@tgl", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@alamat", label5.Text);
                cmd.Parameters.AddWithValue("@kode", label7.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil ditambahkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================== READ ==================
        private void btnTampil_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM mahasiswa";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================== UPDATE ==================
        private void btnUbah_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                // KOREKSI: Pastikan nama kolom di DB (misal: jenis_kelamin) sama dengan di query
                string query = "UPDATE mahasiswa SET nama=@nama, jenis_kelamin=@jk, tanggal_lahir=@tgl, alamat=@alamat, kode_prodi=@kode WHERE nim=@nim";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nim", label1.Text);
                cmd.Parameters.AddWithValue("@nama", label2.Text);
                cmd.Parameters.AddWithValue("@jk", label3.Text);
                cmd.Parameters.AddWithValue("@tgl", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@alamat", label5.Text);
                cmd.Parameters.AddWithValue("@kode", label7.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil diupdate!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================== KONEKSI ==================

        private void btnKoneksi_Click(object sender, EventArgs e)
        {
            try
            {
                // Mengecek apakah koneksi sedang tertutup
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MessageBox.Show("Koneksi Berhasil Dibuka!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Koneksi sudah dalam keadaan terbuka.");
                }
            }
            catch (Exception ex)
            {
                // Menampilkan pesan jika koneksi gagal (misal: password salah atau database mati)
                MessageBox.Show("Gagal Membuka Koneksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Selalu tutup koneksi setelah selesai mengecek
                conn.Close();
            }
        }

        // ================== DELETE ==================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM mahasiswa WHERE nim=@nim";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // KOREKSI: Pastikan nama controlnya txtNIM.Text (tadi kamu tulis NIM.Text)
                cmd.Parameters.AddWithValue("@nim", label1.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // ================== KLIK DATA GRID ==================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tambahkan proteksi agar tidak error jika klik header
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                label1.Text = row.Cells[0].Value.ToString();
                label2.Text = row.Cells[1].Value.ToString();
                label3.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[3].Value);
                label5.Text = row.Cells[4].Value.ToString();
                label7.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}