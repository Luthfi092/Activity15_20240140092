using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader; // Pastikan package ini sudah terinstall

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        private DAL dbLogic = new DAL();
        private DataTable dtExcelCollection; // Menyimpan data dari Excel sementara waktu

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataMhs();
        }

        private void LoadDataMhs()
        {
            try
            {
                DataTable dt = dbLogic.GetMhs();
                dataGridView1.DataSource = dt;
                label7.Text = dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;
        }

        // Helper untuk mengubah Gambar menjadi Array Byte agar bisa masuk database mediumblob
        private byte[] GetFotoBytes()
        {
            if (pictureBox1.Image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void btnUploadGambar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] foto = GetFotoBytes();
                dbLogic.InsertMhs(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, dateTimePicker1.Value, textBox4.Text, foto);
                MessageBox.Show("Data berhasil diinsert!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataMhs();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] foto = GetFotoBytes();
                dbLogic.UpdateMhs(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, dateTimePicker1.Value, textBox4.Text, foto);
                MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataMhs();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbLogic.DeleteMhs(textBox1.Text);
                MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataMhs();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["NIM"].Value?.ToString();
                textBox2.Text = row.Cells["Nama"].Value?.ToString();
                comboBox1.Text = row.Cells["Jenis_Kelamin"].Value?.ToString();
                textBox3.Text = row.Cells["Alamat"].Value?.ToString();
                textBox4.Text = row.Cells["Kode_Prodi"].Value?.ToString();

                if (row.Cells["Tanggal_Lahir"].Value != null && row.Cells["Tanggal_Lahir"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["Tanggal_Lahir"].Value);

                if (row.Cells["foto"].Value != null && row.Cells["foto"].Value != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row.Cells["foto"].Value;
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }

        // --- FITUR IMPORT DARI EXCEL (Sesuai Halaman Modul) ---
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            dtExcelCollection = result.Tables[0];
                            dataGridView1.DataSource = dtExcelCollection; // Tampilkan isi excel di grid view sementara
                        }
                    }
                }
            }
        }

        private void btnImportDatabase_Click(object sender, EventArgs e)
        {
            if (dtExcelCollection == null || dtExcelCollection.Rows.Count == 0)
            {
                MessageBox.Show("Silahkan load file Excel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                foreach (DataRow row in dtExcelCollection.Rows)
                {
                    // Lakukan insert berulang baris demi baris dari excel ke database MySQL
                    dbLogic.InsertMhs(
                        row["NIM"].ToString(),
                        row["Nama"].ToString(),
                        row["Alamat"].ToString(),
                        row["Jenis_Kelamin"].ToString(),
                        Convert.ToDateTime(row["Tanggal_Lahir"]),
                        row["Kode_Prodi"].ToString(),
                        null // Foto di-set null untuk import massal Excel
                    );
                }
                MessageBox.Show("Seluruh data file Excel berhasil dimigrasi ke database!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataMhs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal migrasi data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e) => LoadDataMhs();
        private void btnLoad_Click(object sender, EventArgs e) => LoadDataMhs();
        private void btnRefresh_Click(object sender, EventArgs e) => LoadDataMhs();
        private void btnResetData_Click(object sender, EventArgs e) => ClearFields();
        private void btnCari_Click(object sender, EventArgs e) { }
        private void btnTest_Click(object sender, EventArgs e) => MessageBox.Show("Koneksi OK!");
        private void btnRekapData_Click(object sender, EventArgs e) { }
    }
}