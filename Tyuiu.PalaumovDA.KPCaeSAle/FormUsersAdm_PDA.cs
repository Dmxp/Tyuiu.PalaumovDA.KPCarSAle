using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{

    public partial class FormUsersAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }

        private string selectedUserID;
        public FormUsersAdm_PDA()
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
            labelUsersPAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonSearchUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonchangeroleAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonsavetoexcelAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelpoiskuserid_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            labelchangeroleAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonapplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);

            dataGridViewUsersAdm_PDA.ReadOnly = true;

            this.FormClosing += FormUsersAdm_PDA_FormClosing;

        }

        private void FormUsersAdm_PDA_Load(object sender, EventArgs e)
        {
            dataGridViewUsersAdm_PDA.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);
            dataGridViewUsersAdm_PDA.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14);

            dataGridViewUsersAdm_PDA.CellClick += dataGridViewUsersAdm_PDA_CellClick;

            labelNameUser_PDA.Text = UserName;
            labelSurnameUser_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadUserData();
        }
        private void dataGridViewUsersAdm_PDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridViewUsersAdm_PDA.Rows[e.RowIndex].Selected = true;
                DataGridViewRow selectedRow = dataGridViewUsersAdm_PDA.Rows[e.RowIndex];
                selectedUserID = selectedRow.Cells["USERID"].Value.ToString();
            }
        }
        private void FormUsersAdm_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
        private void LoadUserData()
        {
            try
            {
                dataGridViewUsersAdm_PDA.Rows.Clear();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM user1";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int rowIndex = dataGridViewUsersAdm_PDA.Rows.Add();
                                    DataGridViewRow row = dataGridViewUsersAdm_PDA.Rows[rowIndex];

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        row.Cells[columnName].Value = reader[i];
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

        private void rbuttonSearchUsersAdm_PDA_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxsearchuserid_PDA.Text;

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Введите номер для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool found = false;

            foreach (DataGridViewRow row in dataGridViewUsersAdm_PDA.Rows)
            {
                string carID = Convert.ToString(row.Cells["USERID"].Value);
                if (carID.Equals(searchValue))
                {
                    row.Selected = true;
                    dataGridViewUsersAdm_PDA.CurrentCell = row.Cells["USERID"];
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("Автомобиль с указанным номером не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbuttonchangeroleAdm_PDA_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedUserID))
            {
                MessageBox.Show("Выберите пользователя для изменения роли", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newRole = comboBoxchangeroleAdm_PDA.SelectedItem.ToString();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE user1 SET Role = @Role WHERE UserID = @UserID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Role", newRole);
                        command.Parameters.AddWithValue("@UserID", selectedUserID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            UpdateUserData();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить роль пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении роли пользователя: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateUserData()
        {
            dataGridViewUsersAdm_PDA.Rows.Clear();
            LoadUserData();
        }

        private void rbuttonsavetoexcelAdm_PDA_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;

            for (int col = 0; col < dataGridViewUsersAdm_PDA.Columns.Count; col++)
            {
                wsh.Cells[1, col + 1] = dataGridViewUsersAdm_PDA.Columns[col].HeaderText;
            }

            for (int i = 0; i < dataGridViewUsersAdm_PDA.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewUsersAdm_PDA.Columns.Count; j++)
                {
                    wsh.Cells[i + 2, j + 1] = dataGridViewUsersAdm_PDA[j, i].Value?.ToString();
                }
            }

            exApp.Visible = true;
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

        private void rjButtonAutojap1Adm_PDA_Click(object sender, EventArgs e)
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

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
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

        private void rjButtonapplicationAdm_PDA_Click(object sender, EventArgs e)
        {
            FormApplicationAdm_PDA formappliAdm = new FormApplicationAdm_PDA();
            formappliAdm.UserName = this.UserName;
            formappliAdm.UserSurname = this.UserSurname;
            formappliAdm.UserID = this.UserID;
            formappliAdm.Show();
            this.Hide();
        }

        private void rbuttondeleteuser_PDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsersAdm_PDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow selectedRow = dataGridViewUsersAdm_PDA.SelectedRows[0];
            string userID = Convert.ToString(selectedRow.Cells["UserID"].Value);

            string query = "DELETE FROM user1 WHERE UserID = @UserID";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LoadUserData();
                            //MessageBox.Show("Запись успешно удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось удалить запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
