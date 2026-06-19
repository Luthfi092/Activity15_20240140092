using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUDMahasiswaADO
{
    public partial class Form2 : Form
    {
        private static string connectionString = "Server=localhost;Database=DBAkademikADO;UID=root;Password=;";
        private MySqlConnection conn;
        private MySqlDataAdapter da;
        private DataTable dtMahasiswa;

        private string prodi { get; set; }
        private DateTime tglmasuk { get; set; }

        // Constructor menerima argumen dari Form1
        public Form2(string Prodi, DateTime TglMasuk)
        {
            InitializeComponent();
            conn = new MySqlConnection(connectionString);

            this.prodi = Prodi;
            this.tglmasuk = TglMasuk;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            IsiComboProdi();

            // Mengisi input sesuai data kiriman Form1
            cmbProdi.Text = this.prodi;
            dtpTanggalMasuk.Value = this.tglmasuk;

            if (!string.IsNullOrEmpty(cmbProdi.Text))
            {
                LoadData();
            }
        }

        private void IsiComboProdi()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                // Mengambil kode prodi dari tabel mahasiswa
                string query = "SELECT DISTINCT Kode_Prodi FROM mahasiswa";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbProdi.Items.Clear();
                        while (reader.Read())
                        {
                            cmbProdi.Items.Add(reader["Kode_Prodi"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengisi Combobox Prodi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_Report", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inProdi", cmbProdi.Text);
                    cmd.Parameters.AddWithValue("@inTglMsuk", dtpTanggalMasuk.Value.Year);

                    da = new MySqlDataAdapter(cmd);
                    dtMahasiswa = new DataTable();
                    da.Fill(dtMahasiswa);

                    dataGridView1.DataSource = dtMahasiswa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekap: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbProdi.Text))
            {
                MessageBox.Show("Silahkan pilih Program Studi terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadData();
        }

        // FUNGSI UTAMA UNTUK CETAK LAPORAN
        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (dtMahasiswa == null || dtMahasiswa.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak. Klik tombol Load terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Memanggil langsung class RekapDataMahasiswa sesuai dengan file di Solution Explorer kamu
                RekapDataMahasiswa myReport = new RekapDataMahasiswa();
                myReport.SetDataSource(dtMahasiswa);

                Form3 form3 = new Form3();
                form3.crystalReportViewer1.ReportSource = myReport;
                form3.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencetak laporan Crystal Report: " + ex.Message, "Error Cetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}