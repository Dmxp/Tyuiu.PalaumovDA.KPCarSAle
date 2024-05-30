namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    partial class FormStatisticsAdm_PDA
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartgistogrammAdm_PDA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartgistogrammAdm_PDA)).BeginInit();
            this.SuspendLayout();
            // 
            // chartgistogrammAdm_PDA
            // 
            chartArea1.Name = "ChartArea1";
            this.chartgistogrammAdm_PDA.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartgistogrammAdm_PDA.Legends.Add(legend1);
            this.chartgistogrammAdm_PDA.Location = new System.Drawing.Point(0, -1);
            this.chartgistogrammAdm_PDA.Name = "chartgistogrammAdm_PDA";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartgistogrammAdm_PDA.Series.Add(series1);
            this.chartgistogrammAdm_PDA.Size = new System.Drawing.Size(798, 451);
            this.chartgistogrammAdm_PDA.TabIndex = 0;
            this.chartgistogrammAdm_PDA.Text = "chart1";
            // 
            // FormStatisticsAdm_PDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartgistogrammAdm_PDA);
            this.Name = "FormStatisticsAdm_PDA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистика";
            ((System.ComponentModel.ISupportInitialize)(this.chartgistogrammAdm_PDA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartgistogrammAdm_PDA;
    }
}