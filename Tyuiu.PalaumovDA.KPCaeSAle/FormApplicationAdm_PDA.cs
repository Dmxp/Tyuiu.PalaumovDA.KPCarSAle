using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormApplicationAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormApplicationAdm_PDA()
        {
            InitializeComponent();

            pictureBoxColor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelApplicationAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            labelStatusAdM_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rjButtonapplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonchangestatusapplic_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonOpenAppliAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonExportExcellAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonGotoStatistic_PDA.BackColor = Color.FromArgb(94, 34, 123);

            this.FormClosing += FormApplication_PDA_FormClosing;
            dataGridViewApplicationAdm_PDA.ReadOnly = true;
            dataGridViewApplicationAdm_PDA.CellFormatting += dataGridViewApplicationAdm_PDA_CellFormatting;
            dataGridViewApplicationAdm_PDA.AutoGenerateColumns = false;
        }
        private void FormApplication_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
        private void dataGridViewApplicationAdm_PDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridViewApplicationAdm_PDA.Columns["Photo1"].Index || e.ColumnIndex == dataGridViewApplicationAdm_PDA.Columns["Photo2"].Index || e.ColumnIndex == dataGridViewApplicationAdm_PDA.Columns["Photo3Watch"].Index || e.ColumnIndex == dataGridViewApplicationAdm_PDA.Columns["Photo4Watch"].Index || e.ColumnIndex == dataGridViewApplicationAdm_PDA.Columns["Photo5Watch"].Index))
            {
                DataGridViewImageCell cell = dataGridViewApplicationAdm_PDA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;
                if (cell != null && cell.Value != null)
                {
                    Image originalImage = (Image)cell.Value;
                    int newWidth = 120;
                    int newHeight = 90;
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

        private void FormApplicationAdm_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewApplicationAdm_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewApplicationAdm_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewApplicationAdm_PDA.CellClick += dataGridViewApplicationAdm_PDA_CellClick;

            labelNameUser_PDA.Text = UserName;
            labelSurnameUser_PDA.Text = UserSurname;
            labeluserid.Text = UserID;
            LoadApplicationData();

        }

        private void dataGridViewApplicationAdm_PDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewApplicationAdm_PDA.Rows[e.RowIndex].Selected = true;
            }
        }
        private void LoadApplicationData()
        {
            dataGridViewApplicationAdm_PDA.Rows.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                                    SELECT 
                                        application.*, 
                                    user1.Name1, user1.Surname, user1.Mail, user1.PhoneNumber, user1.City,
                                        COALESCE(autojapan.Brand, autokorea.Brand, autochina.Brand) AS Brand,
                                        COALESCE(autojapan.Model, autokorea.Model, autochina.Model) AS Model,
                                        COALESCE(autojapan.Year, autokorea.Year, autochina.Year) AS Year,
                                        COALESCE(autojapan.Body, autokorea.Body, autochina.Body) AS Body,
                                        COALESCE(autojapan.EngCapacity, autokorea.EngCapacity, autochina.EngCapacity) AS EngCapacity,
                                        COALESCE(autojapan.Equipment, autokorea.Equipment, autochina.Equipment) AS Equipment,
                                        COALESCE(autojapan.Photo1, autokorea.Photo1, autochina.Photo1) AS Photo1,
                                        COALESCE(autojapan.Photo2, autokorea.Photo2, autochina.Photo2) AS Photo2,
                                        COALESCE(autojapan.Photo3Watch, autokorea.Photo3Watch, autochina.Photo3Watch) AS Photo3Watch,
                                        COALESCE(autojapan.Photo4Watch, autokorea.Photo4Watch, autochina.Photo4Watch) AS Photo4Watch,
                                        COALESCE(autojapan.Photo5Watch, autokorea.Photo5Watch, autochina.Photo5Watch) AS Photo5Watch,
                                        COALESCE(autojapan.Mileage, autokorea.Mileage, autochina.Mileage) AS Mileage,
                                        COALESCE(autojapan.Evaluation, autokorea.Evaluation, autochina.Evaluation) AS Evaluation,
                                        COALESCE(autojapan.Price, autokorea.Price, autochina.Price) AS Price,
                                        COALESCE(autojapan.typeofsale, autokorea.typeofsale, autochina.typeofsale) AS typeofsale,
                                        COALESCE(autojapan.comment, autokorea.comment, autochina.comment) AS comment
                                    FROM application
                                    INNER JOIN user1 ON application.userID = user1.userID
                                    LEFT JOIN autojapan ON application.CarID = autojapan.CarID
                                    LEFT JOIN autokorea ON application.CarIDK = autokorea.CarID
                                    LEFT JOIN autochina ON application.CarIDC = autochina.CarID
                                    ORDER BY 
                                        CASE 
                                            WHEN application.Status = 'Открыта' THEN 0 
                                            WHEN application.Status = 'В работе' THEN 1 
                                            ELSE 2 
                                        END, 
                                        application.Status ASC";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewApplicationAdm_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewApplicationAdm_PDA.Rows[rowIndex];

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
        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
            this.Hide();
        }

        private void rjButtonAutojap1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapAdm_PDA formAutoJap = new FormAutoJapAdm_PDA();
            formAutoJap.UserName = this.UserName;
            formAutoJap.UserSurname = this.UserSurname;
            formAutoJap.UserID = this.UserID;
            formAutoJap.Show();
            this.Hide();
        }

        private void rjButtonAutoKor1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorAdm_PDA formAutoKorAdm = new FormAutoKorAdm_PDA();
            formAutoKorAdm.UserName = this.UserName;
            formAutoKorAdm.UserSurname = this.UserSurname;
            formAutoKorAdm.UserID = this.UserID;
            formAutoKorAdm.Show();
            this.Hide();
        }

        private void rjButtonAutoChina1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoChinaAdm_PDA formChAdm = new FormAutoChinaAdm_PDA();
            formChAdm.UserName = this.UserName;
            formChAdm.UserSurname = this.UserSurname;
            formChAdm.UserID = this.UserID;
            formChAdm.Show();
            this.Hide();
        }

        private void rjButtonInfoComp1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormInfoCompAdm_PDA formincoAdm = new FormInfoCompAdm_PDA();
            formincoAdm.UserName = this.UserName;
            formincoAdm.UserSurname = this.UserSurname;
            formincoAdm.UserID = this.UserID;
            formincoAdm.Show();
            this.Hide();
        }

        private void rjButtonInfo1Adm_PDA_Click(object sender, EventArgs e)
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

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormQuestionAdm_PDA formquestAdm = new FormQuestionAdm_PDA();
            formquestAdm.UserName = this.UserName;
            formquestAdm.UserSurname = this.UserSurname;
            formquestAdm.UserID = this.UserID;
            formquestAdm.Show();
            this.Hide();
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

        private void rbuttonchangestatusapplic_PDA_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewApplicationAdm_PDA.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridViewApplicationAdm_PDA.SelectedRows[0];

                    string selectedStatus = comboBoxapplicationAdm_PDas.SelectedItem.ToString();

                    if (!string.IsNullOrEmpty(selectedStatus))
                    {
                        selectedRow.Cells["Status"].Value = selectedStatus;

                        UpdateStatusInDatabase(selectedRow.Cells["applicationID"].Value.ToString(), selectedStatus);
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите статус из выпадающего списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для изменения статуса.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении статуса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateStatusInDatabase(string id, string newStatus)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
            string query = "UPDATE application SET Status = @Status WHERE applicationID = @applicationID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@applicationID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void rbuttonExportExcellAdm_PDA_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            for (int col = 0; col < dataGridViewApplicationAdm_PDA.Columns.Count; col++)
            {
                wsh.Cells[1, col + 1] = dataGridViewApplicationAdm_PDA.Columns[col].HeaderText;
            }

            for (int i = 0; i < dataGridViewApplicationAdm_PDA.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewApplicationAdm_PDA.Columns.Count; j++)
                {
                    wsh.Cells[i + 2, j + 1] = dataGridViewApplicationAdm_PDA[j, i].Value?.ToString();
                }
            }

            exApp.Visible = true;
        }

        private void rbuttonOpenAppliAdm_PDA_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridViewApplicationAdm_PDA.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridViewApplicationAdm_PDA.SelectedRows[0];

                    object modelValue = selectedRow.Cells["Model"].Value;
                    string applicationID = selectedRow.Cells["applicationID"].Value.ToString();

                    if (modelValue == null || string.IsNullOrEmpty(modelValue.ToString()))
                    {
                        FormApplicationSmallAdM_DA form1 = new FormApplicationSmallAdM_DA();
                        form1.applicationID = applicationID;
                        form1.Show();
                    }
                    else
                    {
                        FormApplicationBidAdm_PDA form2 = new FormApplicationBidAdm_PDA();
                        form2.applicationID = applicationID;
                        form2.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии формы: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbuttonGotoStatistic_PDA_Click(object sender, EventArgs e)
        {
            FormStatisticsAdm_PDA formHistogram = new FormStatisticsAdm_PDA();
            formHistogram.Show();
        }
    }
}
