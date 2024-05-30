using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormStatisticsAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

        public FormStatisticsAdm_PDA()
        {
            InitializeComponent();

            // Вызов метода для построения гистограммы
            PopulateBrandStatistics();

            // Настройка подписей осей
            chartgistogrammAdm_PDA.ChartAreas[0].AxisX.Title = "Марка автомобиля";
            chartgistogrammAdm_PDA.ChartAreas[0].AxisY.Title = "Кол-во проданных машин";
        }

        private void PopulateBrandStatistics()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT Brand, COUNT(*) AS Total 
                        FROM application 
                        WHERE Status = 'Оформлена' 
                        GROUP BY Brand";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string brand = reader.GetString("Brand");
                                int count = reader.GetInt32("Total");

                                chartgistogrammAdm_PDA.Series["Series1"].Points.AddXY(brand, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке статистики: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
