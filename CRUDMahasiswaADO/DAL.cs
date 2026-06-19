using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CRUDMahasiswaADO
{
    public class DAL
    {
        protected string connectionString = "Server=localhost;Database=DBAkademikADO;UID=root;Password=;";

        // 1. Ambil Semua Data Mahasiswa (Termasuk Foto)
        public DataTable GetMhs()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetMahasiswa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error pada GetMhs: " + ex.Message);
            }
            return dt;
        }

        // 2. Tambah Data Mahasiswa (Dengan Parameter Foto)
        public void InsertMhs(string nim, string nama, string alamat, string jk, DateTime tgl, string kode, byte[] foto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_InsertMahasiswa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nim", nim);
                        cmd.Parameters.AddWithValue("@p_nama", nama);
                        cmd.Parameters.AddWithValue("@p_alamat", alamat);
                        cmd.Parameters.AddWithValue("@p_jk", jk);
                        cmd.Parameters.AddWithValue("@p_tgl", tgl);
                        cmd.Parameters.AddWithValue("@p_kode", kode);
                        cmd.Parameters.AddWithValue("@p_foto", foto);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error pada InsertMhs: " + ex.Message);
            }
        }

        // 3. Ubah Data Mahasiswa (Dengan Parameter Foto)
        public void UpdateMhs(string nim, string nama, string alamat, string jk, DateTime tgl, string kode, byte[] foto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_UpdateMahasiswa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nim", nim);
                        cmd.Parameters.AddWithValue("@p_nama", nama);
                        cmd.Parameters.AddWithValue("@p_alamat", alamat);
                        cmd.Parameters.AddWithValue("@p_jk", jk);
                        cmd.Parameters.AddWithValue("@p_tgl", tgl);
                        cmd.Parameters.AddWithValue("@p_kode", kode);
                        cmd.Parameters.AddWithValue("@p_foto", foto);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error pada UpdateMhs: " + ex.Message);
            }
        }

        // 4. Hapus Data Mahasiswa
        public void DeleteMhs(string nim)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_DeleteMahasiswa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nim", nim);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error pada DeleteMhs: " + ex.Message);
            }
        }

        // 5. Method Get Data Chart untuk Grafik Dashboard
        public DataTable getAllDataChart()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_DashBoard", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error pada getAllDataChart: " + ex.Message);
            }
            return dt;
        }
    }
}