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
using Excel = Microsoft.Office.Interop.Excel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormAutoChinaAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public void SearchButtonClicked()
        {
            rbuttonresetsearchAdm_PDA_Click(null, EventArgs.Empty);
        }
        public FormAutoChinaAdm_PDA()
        {
            InitializeComponent();

            pictureBoxColor2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonAddAutoAdm_PDA_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonSearchAutChAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonSearchCarIDCh_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonEditAutoChAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonDeleteAutoChAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonExportExcellAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonresetsearchAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelAutoChinaAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonapplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);


            dataGridViewAutoChAdm_PDA.ReadOnly = true;


            FillBrandComboBox();
            comboBoxBrandAdmin_PDA.SelectedIndexChanged += comboBoxBrandAdmin_PDA_SelectedIndexChanged;
            //dataGridViewAutoJapAdm_PDA.CellPainting += dataGridViewAutoJapAdm_PDA_CellPainting;
            dataGridViewAutoChAdm_PDA.CellFormatting += dataGridViewAutoChAdm_PDA_CellFormatting;

            this.FormClosing += FormAutoChinaAdm_PDA_FormClosing;

            dataGridViewAutoChAdm_PDA.AutoGenerateColumns = false;
        }
        private void FormAutoChinaAdm_PDA_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormAutoChinaAdm_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewAutoChAdm_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewAutoChAdm_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewAutoChAdm_PDA.CellClick += dataGridViewAutoChAdm_PDA_CellClick;

            labelNameCh_PDA.Text = UserName;
            labelSurnameCh_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadAutoChinaData();
        }
        private void LoadAutoChinaData()
        {
            dataGridViewAutoChAdm_PDA.Rows.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autochina ORDER BY CASE WHEN Status = 'Активна' THEN 1 ELSE 2 END, Status";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewAutoChAdm_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoChAdm_PDA.Rows[rowIndex];

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
        private void rbuttonAddAutoAdm_PDA_PDA_Click(object sender, EventArgs e)
        {
            FormAddAutoAdm_PDA formAddAuto = new FormAddAutoAdm_PDA();
            formAddAuto.ShowDialog(this);
        }
        private void FillBrandComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Brand FROM autochina";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string brand = reader.GetString("Brand");
                                comboBoxBrandAdmin_PDA.Items.Add(brand);
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
        private void comboBoxBrandAdmin_PDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrandAdmin_PDA.SelectedItem != null)
            {
                string selectedBrand = comboBoxBrandAdmin_PDA.SelectedItem.ToString();
                FillModelComboBox(selectedBrand);
            }
        }
        private void FillModelComboBox(string selectedBrand)
        {
            comboBoxModelAdm_PDA.Items.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT Model FROM autochina WHERE Brand = @Brand";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Brand", selectedBrand);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string model = reader.GetString("Model");
                                comboBoxModelAdm_PDA.Items.Add(model);
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

        private void rbuttonSearchAutChAdm_PDA_Click(object sender, EventArgs e)
        {
            string selectedBrand = comboBoxBrandAdmin_PDA.SelectedItem?.ToString();
            string selectedModel = comboBoxModelAdm_PDA.SelectedItem?.ToString();

            if (selectedBrand == null || selectedModel == null)
            {
                MessageBox.Show("Выберите бренд и модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridViewAutoChAdm_PDA.Rows.Clear();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM autochina WHERE Brand = @Brand AND Model = @Model ORDER BY CASE WHEN Status = 'Активна' THEN 1 ELSE 2 END, Status";

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
                                    int rowIndex = dataGridViewAutoChAdm_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewAutoChAdm_PDA.Rows[rowIndex];

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
        private void dataGridViewAutoChAdm_PDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridViewAutoChAdm_PDA.Columns["Photo1"].Index || e.ColumnIndex == dataGridViewAutoChAdm_PDA.Columns["Photo2"].Index || e.ColumnIndex == dataGridViewAutoChAdm_PDA.Columns["Photo3Watch"].Index || e.ColumnIndex == dataGridViewAutoChAdm_PDA.Columns["Photo4Watch"].Index || e.ColumnIndex == dataGridViewAutoChAdm_PDA.Columns["Photo5Watch"].Index))
            {
                DataGridViewImageCell cell = dataGridViewAutoChAdm_PDA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;
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

        private void rbuttonSearchCarIDCh_PDA_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearchCarID_PDA.Text;

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Введите номер для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool found = false;

            foreach (DataGridViewRow row in dataGridViewAutoChAdm_PDA.Rows)
            {
                string carID = Convert.ToString(row.Cells["CarID"].Value);
                if (carID.Equals(searchValue))
                {
                    row.Selected = true;
                    dataGridViewAutoChAdm_PDA.CurrentCell = row.Cells["CarID"];
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("Автомобиль с указанным номером не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewAutoChAdm_PDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewAutoChAdm_PDA.Rows[e.RowIndex].Selected = true;
            }
        }

        private void rbuttonEditAutoChAdm_PDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutoChAdm_PDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow selectedRow = dataGridViewAutoChAdm_PDA.SelectedRows[0];
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

                                FormEditAutoAdm_PDA editForm = new FormEditAutoAdm_PDA(carID, photo1, photo2, photo3, photo4, photo5, brand, model, year, body, engine, equipment, mileage, evaluation, price, typeofsale, comment, country);
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

        private void rbuttonDeleteAutoChAdm_PDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutoChAdm_PDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для изменения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow selectedRow = dataGridViewAutoChAdm_PDA.SelectedRows[0];
            string carID = Convert.ToString(selectedRow.Cells["CarID"].Value);

            string query = "UPDATE autochina SET Status = 'Закрыта' WHERE CarID = @CarID";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@CarID", carID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Обновляем отображение данных после изменения
                            rbuttonSearchAutChAdm_PDA_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось изменить запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormMainAdmin_PDA formMainAdm = new FormMainAdmin_PDA();
            formMainAdm.UserName = this.UserName;
            formMainAdm.UserSurname = this.UserSurname;
            formMainAdm.UserID = this.UserID;
            formMainAdm.Show();
            this.Hide();
        }

        private void rjButtonAutojap2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapAdm_PDA formAutoJapAdm = new FormAutoJapAdm_PDA();
            formAutoJapAdm.UserName = this.UserName;
            formAutoJapAdm.UserSurname = this.UserSurname;
            formAutoJapAdm.UserID = this.UserID;
            formAutoJapAdm.Show();
            this.Hide();
        }

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
            this.Hide();
        }

        private void rjButtonAutoKor2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorAdm_PDA formAutokorAdm = new FormAutoKorAdm_PDA();
            formAutokorAdm.UserName = this.UserName;
            formAutokorAdm.UserSurname = this.UserSurname;
            formAutokorAdm.UserID = this.UserID;
            formAutokorAdm.Show();
            this.Hide();
        }

        private void rjButtonInfoComp2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormInfoCompAdm_PDA formincoAdm = new FormInfoCompAdm_PDA();
            formincoAdm.UserName = this.UserName;
            formincoAdm.UserSurname = this.UserSurname;
            formincoAdm.UserID = this.UserID;
            formincoAdm.Show();
            this.Hide();
        }

        private void rbuttonExportExcellAdm_PDA_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            for (int col = 0; col < dataGridViewAutoChAdm_PDA.Columns.Count; col++)
            {
                wsh.Cells[1, col + 1] = dataGridViewAutoChAdm_PDA.Columns[col].HeaderText;
            }

            for (int i = 0; i < dataGridViewAutoChAdm_PDA.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewAutoChAdm_PDA.Columns.Count; j++)
                {
                    wsh.Cells[i + 2, j + 1] = dataGridViewAutoChAdm_PDA[j, i].Value?.ToString();
                }
            }

            exApp.Visible = true;
        }

        private void rjButtonInfo2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormContactsAdm_PDA formcontAdm = new FormContactsAdm_PDA();
            formcontAdm.UserName = this.UserName;
            formcontAdm.UserSurname = this.UserSurname;
            formcontAdm.UserID = this.UserID;
            formcontAdm.Show();
            this.Hide();
        }

        private void rjButtonUsersAdm_PDA_Click(object sender, EventArgs e)
        {
            FormUsersAdm_PDA formusAdm = new FormUsersAdm_PDA();
            formusAdm.UserName = this.UserName;
            formusAdm.UserSurname = this.UserSurname;
            formusAdm.UserID = this.UserID;
            formusAdm.Show();
            this.Hide();
        }

        private void rbuttonresetsearchAdm_PDA_Click(object sender, EventArgs e)
        {
            LoadAutoChinaData();
        }

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormQuestionAdm_PDA formquestAdm = new FormQuestionAdm_PDA();
            formquestAdm.UserName = this.UserName;
            formquestAdm.UserSurname = this.UserSurname;
            formquestAdm.UserID = this.UserID;
            formquestAdm.Show();
            this.Hide();
        }

        private void rjButtonapplicationAdm_PDA_Click(object sender, EventArgs e)
        {
            FormApplicationAdm_PDA formappliAdm = new FormApplicationAdm_PDA();
            formappliAdm.UserName = this.UserName;
            formappliAdm.UserSurname = this.UserSurname;
            formappliAdm.UserID = this.UserID;
            formappliAdm.Show();
            this.Hide();
        }
    }
}
