using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormAutoJapan_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }

        public FormAutoJapan_PDA()
        {
            InitializeComponent();

            pictureBoxColor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonSearchAutJap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonOpenWatchAutoJap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonresetsearchJap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelAutoJapAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);

            dataGridViewAutoJap_PDA.ReadOnly = true;

            FillBrandComboBox();
            comboBoxBrand_PDA.SelectedIndexChanged += comboBoxBrand_PDA_SelectedIndexChanged;
            //dataGridViewAutoJap_PDA.CellPainting += dataGridViewAutoJap_PDA_CellPainting;
            dataGridViewAutoJap_PDA.CellFormatting += dataGridViewAutoJapAdm_PDA_CellFormatting;
            dataGridViewAutoJap_PDA.AutoGenerateColumns = false;



            this.FormClosing += FormMainAutoJapan_FormClosing;
        }

        private void FillBrandComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Brand FROM autojapan WHERE Status != 'Закрыта'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string brand = reader.GetString("Brand");
                                comboBoxBrand_PDA.Items.Add(brand);
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

        private void dataGridViewAutoJapAdm_PDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridViewAutoJap_PDA.Columns["Photo1"].Index || e.ColumnIndex == dataGridViewAutoJap_PDA.Columns["Photo2"].Index || e.ColumnIndex == dataGridViewAutoJap_PDA.Columns["Photo3Watch"].Index || e.ColumnIndex == dataGridViewAutoJap_PDA.Columns["Photo4Watch"].Index || e.ColumnIndex == dataGridViewAutoJap_PDA.Columns["Photo5Watch"].Index))
            {
                DataGridViewImageCell cell = dataGridViewAutoJap_PDA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;
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
        private void comboBoxBrand_PDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrand_PDA.SelectedItem != null)
            {
                string selectedBrand = comboBoxBrand_PDA.SelectedItem.ToString();
                FillModelComboBox(selectedBrand);
            }
        }
        private void FillModelComboBox(string selectedBrand)
        {
            comboBoxModel_PDA.Items.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Model FROM autojapan WHERE Brand = @Brand AND Status != 'Закрыта'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Brand", selectedBrand);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string model = reader.GetString("Model");
                                comboBoxModel_PDA.Items.Add(model);
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
        private void FormMainAutoJapan_FormClosing(object sender, FormClosingEventArgs e)
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

        private void rbuttonSearchAutJap_PDA_Click(object sender, EventArgs e)
        {
            string selectedBrand = comboBoxBrand_PDA.SelectedItem?.ToString();
            string selectedModel = comboBoxModel_PDA.SelectedItem?.ToString();

            if (selectedBrand == null || selectedModel == null)
            {
                MessageBox.Show("Выберите бренд и модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridViewAutoJap_PDA.Rows.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autojapan WHERE Brand = @Brand AND Model = @Model AND Status != 'Закрыта'";

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
                                    int rowIndex = dataGridViewAutoJap_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoJap_PDA.Rows[rowIndex];

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

        private void FormAutoJapan_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewAutoJap_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewAutoJap_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewAutoJap_PDA.CellClick += dataGridViewAutoJap_PDA_CellClick;

            labelNameAutJap_PDA.Text = UserName;
            labelSurnameAutoJap_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadAutoJapanData();
        }
        private void LoadAutoJapanData()
        {
            dataGridViewAutoJap_PDA.Rows.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autojapan WHERE Status != 'Закрыта'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewAutoJap_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoJap_PDA.Rows[rowIndex];

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
        private void dataGridViewAutoJap_PDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewAutoJap_PDA.Rows[e.RowIndex].Selected = true;
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

        private void rbuttonOpenWatchAutoJap_PDA_Click(object sender, EventArgs e)
        {

            if (dataGridViewAutoJap_PDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow selectedRow = dataGridViewAutoJap_PDA.SelectedRows[0];
            string carID = Convert.ToString(selectedRow.Cells["CarID"].Value);

            string query = "SELECT * FROM autojapan WHERE CarID = @CarID";

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
                                editForm.Show(this);
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
        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
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

        private void rjButtonAutoChina_PDA_Click(object sender, EventArgs e)
        {
            FormAutoChina_PDA formautochina = new FormAutoChina_PDA();
            formautochina.UserName = this.UserName;
            formautochina.UserSurname = this.UserSurname;
            formautochina.UserID = this.UserID;
            formautochina.Show();
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

        private void rbuttonresetsearchJap_PDA_Click(object sender, EventArgs e)
        {
            LoadAutoJapanData();
        }

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormqQuestion_PDA formaauest = new FormqQuestion_PDA();
            formaauest.UserID = this.UserID;
            formaauest.Show();
        }
    }
}
