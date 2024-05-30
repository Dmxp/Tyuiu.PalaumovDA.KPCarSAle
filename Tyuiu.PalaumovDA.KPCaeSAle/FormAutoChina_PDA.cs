using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormAutoChina_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormAutoChina_PDA()
        {
            InitializeComponent();

            pictureBoxColor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonSearchAutCh_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonOpenWatchAutoCh_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserAutoCh_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonresetsearchCh_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelAutoChinaAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);

            dataGridViewAutoCh_PDA.ReadOnly = true;

            FillBrandComboBoxAutoCh();
            comboBoxBrandAutoCh_PDA.SelectedIndexChanged += comboBoxBrandAutoCh_PDA_SelectedIndexChanged;
            dataGridViewAutoCh_PDA.CellFormatting += dataGridViewAutoCh_PDA_CellFormatting;
            dataGridViewAutoCh_PDA.AutoGenerateColumns = false;

            this.FormClosing += FormAutoChina_PDA_FormClosing;

        }

        private void FormAutoChina_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewAutoCh_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewAutoCh_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewAutoCh_PDA.CellClick += dataGridViewAutoCh_PDA_CellClick;

            labelNameAutCh_PDA.Text = UserName;
            labelSurnameAutoCh_PDA.Text = UserSurname;
            LoadAutoChinaData();
        }
        private void LoadAutoChinaData()
        {
            dataGridViewAutoCh_PDA.Rows.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autochina WHERE Status != 'Закрыта'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewAutoCh_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoCh_PDA.Rows[rowIndex];

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        row.Cells[columnName].Value = reader[i];
                                    }
                                    if (row.Cells["Photo1"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo1"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo1"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo1"].Value = null;
                                        }
                                    }

                                    if (row.Cells["Photo2"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo2"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo2"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo2"].Value = null;
                                        }
                                    }

                                    if (row.Cells["Photo3Watch"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo3Watch"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo3Watch"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo3Watch"].Value = null;
                                        }
                                    }
                                    if (row.Cells["Photo4Watch"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo4Watch"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo4Watch"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo4Watch"].Value = null;
                                        }
                                    }
                                    if (row.Cells["Photo5Watch"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo5Watch"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo5Watch"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo5Watch"].Value = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbuttonChangeUserAutoCh_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
            this.Hide();
        }
        private void FillBrandComboBoxAutoCh()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Brand FROM autochina WHERE Status != 'Закрыта'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string brand = reader.GetString("Brand");
                                comboBoxBrandAutoCh_PDA.Items.Add(brand);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заполнении списка брендов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewAutoCh_PDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridViewAutoCh_PDA.Columns["Photo1"].Index || e.ColumnIndex == dataGridViewAutoCh_PDA.Columns["Photo2"].Index || e.ColumnIndex == dataGridViewAutoCh_PDA.Columns["Photo3Watch"].Index || e.ColumnIndex == dataGridViewAutoCh_PDA.Columns["Photo4Watch"].Index || e.ColumnIndex == dataGridViewAutoCh_PDA.Columns["Photo5Watch"].Index))
            {
                DataGridViewImageCell cell = dataGridViewAutoCh_PDA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;
                if (cell != null && cell.Value != null)
                {
                    Image originalImage = (Image)cell.Value;
                    int newWidth = 120; // ваша новая ширина
                    int newHeight = 90; // ваша новая высота
                    Bitmap resizedImage = new Bitmap(newWidth, newHeight);
                    using (Graphics gr = Graphics.FromImage(resizedImage))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight));
                    }
                    e.Value = resizedImage;
                }
            }
        }
        private void comboBoxBrandAutoCh_PDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrandAutoCh_PDA.SelectedItem != null)
            {
                string selectedBrand = comboBoxBrandAutoCh_PDA.SelectedItem.ToString();
                FillModelComboBoxAutoCh(selectedBrand);
            }
        }
        private void FillModelComboBoxAutoCh(string selectedBrand)
        {
            comboBoxModelAutoCh_PDA.Items.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Model FROM autochina WHERE Brand = @Brand AND Status != 'Закрыта'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Brand", selectedBrand);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string model = reader.GetString("Model");
                                comboBoxModelAutoCh_PDA.Items.Add(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заполнении списка моделей: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormAutoChina_PDA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void rbuttonSearchAutCh_PDA_Click(object sender, EventArgs e)
        {
            string selectedBrand = comboBoxBrandAutoCh_PDA.SelectedItem?.ToString();
            string selectedModel = comboBoxModelAutoCh_PDA.SelectedItem?.ToString();

            if (selectedBrand == null || selectedModel == null)
            {
                MessageBox.Show("Выберите бренд и модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridViewAutoCh_PDA.Rows.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autochina WHERE Brand = @Brand AND Model = @Model";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Brand", selectedBrand);
                        command.Parameters.AddWithValue("@Model", selectedModel);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewAutoCh_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoCh_PDA.Rows[rowIndex];

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        row.Cells[columnName].Value = reader[i];
                                    }

                                    if (row.Cells["Photo1"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo1"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo1"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo1"].Value = null;
                                        }
                                    }

                                    if (row.Cells["Photo2"].Value != null)
                                    {
                                        string imagePath = row.Cells["Photo2"].Value.ToString();
                                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                                        {
                                            Image image = Image.FromFile(imagePath);
                                            row.Cells["Photo2"].Value = image;
                                        }
                                        else
                                        {
                                            row.Cells["Photo2"].Value = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Нет данных для выбранных бренда и модели", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewAutoCh_PDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewAutoCh_PDA.Rows[e.RowIndex].Selected = true;
            }
        }

        private void pictureBoxLogomainAutoJap_Click(object sender, EventArgs e)
        {
            FormMain_PDA formmain_pda = new FormMain_PDA();
            formmain_pda.UserName = this.UserName;
            formmain_pda.UserSurname = this.UserSurname;
            formmain_pda.UserID = this.UserID;
            formmain_pda.Show();
            this.Hide();
        }

        private void rbuttonOpenWatchAutoCh_PDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutoCh_PDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow selectedRow = dataGridViewAutoCh_PDA.SelectedRows[0];
            string carID = Convert.ToString(selectedRow.Cells["CarID"].Value);

            string query = "SELECT * FROM autochina WHERE CarID = @CarID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarID", carID);
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string photo1 = Convert.ToString(reader["Photo1"]);
                                string photo2 = Convert.ToString(reader["Photo2"]);
                                string photo3 = Convert.ToString(reader["Photo3Watch"]);
                                string photo4 = Convert.ToString(reader["Photo4Watch"]);
                                string photo5 = Convert.ToString(reader["Photo5Watch"]);
                                string brand = Convert.ToString(reader["Brand"]);
                                string model = Convert.ToString(reader["Model"]);
                                string year = Convert.ToString(reader["Year"]);
                                string body = Convert.ToString(reader["Body"]);
                                string engine = Convert.ToString(reader["EngCapacity"]);
                                string equipment = Convert.ToString(reader["Equipment"]);
                                string mileage = Convert.ToString(reader["Mileage"]);
                                string evaluation = Convert.ToString(reader["Evaluation"]);
                                string price = Convert.ToString(reader["Price"]);
                                string typeofsale = Convert.ToString(reader["TypeofSale"]);
                                string comment = Convert.ToString(reader["Comment"]);
                                string country = Convert.ToString(reader["Country"]);

                                FormWatchAutoJap_PDA editForm = new FormWatchAutoJap_PDA(carID, photo1, photo2, photo3, photo4, photo5, brand, model, year, body, engine, equipment, mileage, evaluation, price, typeofsale, comment, country);
                                editForm.UserID = this.UserID;
                                editForm.ShowDialog(this);
                            }
                            else
                            {
                                MessageBox.Show("Автомобиль с указанным идентификатором не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void rjButtonAutojap_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapan_PDA formautojap = new FormAutoJapan_PDA();
            formautojap.UserName = this.UserName;
            formautojap.UserSurname = this.UserSurname;
            formautojap.UserID = this.UserID;
            formautojap.Show();
            this.Hide();
        }

        private void rjButtonAutoKor_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorea_PDA formautokor = new FormAutoKorea_PDA();
            formautokor.UserName = this.UserName;
            formautokor.UserSurname = this.UserSurname;
            formautokor.UserID = this.UserID;
            formautokor.Show();
            this.Hide();
        }

        private void rjButtonInfoComp_PDA_Click(object sender, EventArgs e)
        {
            FormInfoComp_PDA forminfoComp = new FormInfoComp_PDA();
            forminfoComp.UserName = this.UserName;
            forminfoComp.UserSurname = this.UserSurname;
            forminfoComp.UserID = this.UserID;
            forminfoComp.Show();
            this.Hide();
        }

        private void rjButtonInfo_PDA_Click(object sender, EventArgs e)
        {
            FormContacts_PDA formacont = new FormContacts_PDA();
            formacont.UserName = this.UserName;
            formacont.UserSurname = this.UserSurname;
            formacont.UserID = this.UserID;
            formacont.Show();
            this.Hide();
        }

        private void rbuttonresetsearchCh_PDA_Click(object sender, EventArgs e)
        {
            LoadAutoChinaData();
        }

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormqQuestion_PDA formaauest = new FormqQuestion_PDA();
            formaauest.UserID = this.UserID;
            formaauest.Show();
        }
    }
}
