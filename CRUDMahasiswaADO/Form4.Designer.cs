namespace CRUDMahasiswaADO
{
    partial class FormDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTanggalMasuk = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbTipe = new System.Windows.Forms.ComboBox();
            this.chartProdi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDataMahasiswa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartProdi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // dtpTanggalMasuk
            // 
            this.dtpTanggalMasuk.Location = new System.Drawing.Point(62, 56);
            this.dtpTanggalMasuk.Name = "dtpTanggalMasuk";
            this.dtpTanggalMasuk.Size = new System.Drawing.Size(221, 22);
            this.dtpTanggalMasuk.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(388, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cmbTipe
            // 
            this.cmbTipe.FormattingEnabled = true;
            this.cmbTipe.Location = new System.Drawing.Point(606, 54);
            this.cmbTipe.Name = "cmbTipe";
            this.cmbTipe.Size = new System.Drawing.Size(121, 24);
            this.cmbTipe.TabIndex = 5;
            // 
            // chartProdi
            // 
            chartArea2.Name = "ChartArea1";
            this.chartProdi.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartProdi.Legends.Add(legend2);
            this.chartProdi.Location = new System.Drawing.Point(15, 85);
            this.chartProdi.Name = "chartProdi";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartProdi.Series.Add(series2);
            this.chartProdi.Size = new System.Drawing.Size(704, 440);
            this.chartProdi.TabIndex = 6;
            this.chartProdi.Text = "chartProdi";
            // 
            // btnDataMahasiswa
            // 
            this.btnDataMahasiswa.Location = new System.Drawing.Point(572, 551);
            this.btnDataMahasiswa.Name = "btnDataMahasiswa";
            this.btnDataMahasiswa.Size = new System.Drawing.Size(147, 25);
            this.btnDataMahasiswa.TabIndex = 7;
            this.btnDataMahasiswa.Text = "Data Mahasiswa ";
            this.btnDataMahasiswa.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 588);
            this.Controls.Add(this.btnDataMahasiswa);
            this.Controls.Add(this.chartProdi);
            this.Controls.Add(this.cmbTipe);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpTanggalMasuk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Dashboard";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.chartProdi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTanggalMasuk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbTipe;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProdi;
        private System.Windows.Forms.Button btnDataMahasiswa;
    }
}