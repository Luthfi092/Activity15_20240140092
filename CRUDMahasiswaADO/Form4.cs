using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Tambahkan ini jika menggunakan Chart WinForms

namespace CRUDMahasiswaADO
{
    public partial class FormDashboard : Form
    {
        private DAL dbLogic = new DAL();

        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            TampilGrafik();
        }

        private void TampilGrafik()
        {
            try
            {
                DataTable dt = dbLogic.getAllDataChart();

                chartProdi.Series.Clear();
                Series series = chartProdi.Series.Add("Jumlah Mahasiswa");
                series.ChartType = SeriesChartType.Column; // Jenis grafik batang sesuai modul

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Kode_Prodi"].ToString(), Convert.ToInt32(row["Jumlah"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat grafik: " + ex.Message);
            }
        }
    }
}