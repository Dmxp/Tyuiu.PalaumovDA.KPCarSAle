using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormQuestionAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }

        private string selectedquestionID;
        public FormQuestionAdm_PDA()
        {
            InitializeComponent();

            pictureBoxColor2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo2Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelWuestionsAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonsavetoexcelAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelStatusAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonchangestatusAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonapplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);


            dataGridViewQuestionsAdm_PDA.ReadOnly = true;

            this.FormClosing += FormQuestionAdm_PDA_FormClosing;
        }
        private void FormQuestionAdm_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
        private void FormQuestionAdm_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewQuestionsAdm_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewQuestionsAdm_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewQuestionsAdm_PDA.CellClick += dataGridViewQuestionsAdm_CellClick;

            labelNameQuest_PDA.Text = UserName;
            labelSurnameQuest_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadQuestionData();
        }
        private void dataGridViewQuestionsAdm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewQuestionsAdm_PDA.Rows[e.RowIndex].Selected = true;
                DataGridViewRow selectedRow = dataGridViewQuestionsAdm_PDA.Rows[e.RowIndex];
                selectedquestionID = selectedRow.Cells["questionID"].Value.ToString();
            }
        }
        private void LoadQuestionData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    questions.questionID, 
                    questions.UserID, 
                    questions.Messenger, 
                    questions.Question, 
                    questions.Status, 
                    user1.Name1, 
                    user1.Surname, 
                    user1.Mail, 
                    user1.PhoneNumber, 
                    user1.City
                FROM 
                    questions
                INNER JOIN 
                    user1 ON questions.UserID = user1.UserID
                ORDER BY 
                    CASE 
                        WHEN Status = 'Не рассмотрен' THEN 1 
                        WHEN Status = 'В работе' THEN 2 
                        WHEN Status = 'Закрыт' THEN 3 
                        ELSE 4 
                    END";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewQuestionsAdm_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewQuestionsAdm_PDA.Rows[rowIndex];

                                    row.Cells["questionID"].Value = reader["questionID"].ToString();
                                    row.Cells["UserID"].Value = reader["UserID"].ToString();
                                    row.Cells["Messenger"].Value = reader["Messenger"].ToString();
                                    row.Cells["Question"].Value = reader["Question"].ToString();
                                    row.Cells["Status"].Value = reader["Status"].ToString();
                                    row.Cells["Name1"].Value = reader["Name1"].ToString();
                                    row.Cells["Surname"].Value = reader["Surname"].ToString();
                                    row.Cells["Mail"].Value = reader["Mail"].ToString();
                                    row.Cells["PhoneNumber"].Value = reader["PhoneNumber"].ToString();
                                    row.Cells["City"].Value = reader["City"].ToString();
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

        private void rbuttonchangestatusAdm_PDA_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedquestionID))
            {
                MessageBox.Show("Выберите вопрос для изменения статуса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newStatus = comboBoxchangeroleAdm_PDA.SelectedItem.ToString();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE questions SET Status = @Status WHERE questionID = @questionID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", newStatus);
                        command.Parameters.AddWithValue("@questionID", selectedquestionID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            UpdateQuestionData();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить статус вопроса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении статуса вопроса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateQuestionData()
        {
            dataGridViewQuestionsAdm_PDA.Rows.Clear();
            LoadQuestionData();
        }

        private void rbuttonsavetoexcelAdm_PDA_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            for (int col = 0; col < dataGridViewQuestionsAdm_PDA.Columns.Count; col++)
            {
                wsh.Cells[1, col + 1] = dataGridViewQuestionsAdm_PDA.Columns[col].HeaderText;
            }

            for (int i = 0; i < dataGridViewQuestionsAdm_PDA.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewQuestionsAdm_PDA.Columns.Count; j++)
                {
                    wsh.Cells[i + 2, j + 1] = dataGridViewQuestionsAdm_PDA[j, i].Value?.ToString();
                }
            }

            exApp.Visible = true;
        }

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
            this.Hide();
        }

        private void rjButtonAutojap2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapAdm_PDA formjapadm = new FormAutoJapAdm_PDA();
            formjapadm.UserName = this.UserName;
            formjapadm.UserSurname = this.UserSurname;
            formjapadm.UserID = this.UserID;
            formjapadm.Show();
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

        private void rjButtonAutoChina2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoChinaAdm_PDA formChAdm = new FormAutoChinaAdm_PDA();
            formChAdm.UserName = this.UserName;
            formChAdm.UserSurname = this.UserSurname;
            formChAdm.UserID = this.UserID;
            formChAdm.Show();
            this.Hide();
        }

        private void rjButtonAutoKor2Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorAdm_PDA formAutoKorAdm = new FormAutoKorAdm_PDA();
            formAutoKorAdm.UserName = this.UserName;
            formAutoKorAdm.UserSurname = this.UserSurname;
            formAutoKorAdm.UserID = this.UserID;
            formAutoKorAdm.Show();
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
            FormUsersAdm_PDA formusersAdm = new FormUsersAdm_PDA();
            formusersAdm.UserName = this.UserName;
            formusersAdm.UserSurname = this.UserSurname;
            formusersAdm.UserID = this.UserID;
            formusersAdm.Show();
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
