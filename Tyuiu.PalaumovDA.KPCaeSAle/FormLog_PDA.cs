using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormLogin_PDA : Form
    {
        private string loggedInUserName;
        private string loggedInUserSurname;
        private string loggedUserID;

        public FormLogin_PDA()
        {
            InitializeComponent();
            this.FormClosing += FormLog_PDA_FormClosing;
        }

        private bool CheckLogin(string username, string password)
        {
            string connectionString = "Server=127.0.0.1;Database=KP_CARSALE_PDA;Uid=root;Pwd=Dimap2634;SslMode=None;";

            string query = "SELECT UserID, password, Name1, Surname FROM user1 WHERE username=@username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string savedPasswordHash = reader.GetString("password");
                            loggedInUserName = reader.GetString("Name1");
                            loggedInUserSurname = reader.GetString("Surname");
                            loggedUserID = reader.GetInt32("UserID").ToString();

                            if (VerifyHashedPassword(savedPasswordHash, password))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;

        }

        private bool VerifyHashedPassword(string savedPasswordHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private string GetUserRole(string username)
        {
            string connectionString = "Server=127.0.0.1;Database=KP_CARSALE_PDA;Uid=root;Pwd=Dimap2634;SslMode=None;";

            string query = "SELECT Role FROM user1 WHERE Username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Роль пользователя не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при получении роли пользователя: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
        }

        private void FormLogin_PDA_Load(object sender, EventArgs e)
        {
            textBoxPass_PDA.UseSystemPasswordChar = true;
        }

        private void checkBoxVisiblePass_PDA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVisiblePass_PDA.Checked)
            {
                textBoxPass_PDA.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPass_PDA.UseSystemPasswordChar = true;
            }
        }

        private void rjButtonGoRegistr_PDA_Click(object sender, EventArgs e)
        {
            var formr = new FormRegistration_PDA();
            formr.Show();
            Hide();
        }

        private void rjButtonVhod_PDA_Click_1(object sender, EventArgs e)
        {
            string username = textBoxLog_PDA.Text;
            string password = textBoxPass_PDA.Text;

            if (CheckLogin(username, password))
            {
                string role = GetUserRole(username);

                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Admin")
                    {
                        FormMainAdmin_PDA form = new FormMainAdmin_PDA();
                        form.UserName = loggedInUserName;
                        form.UserSurname = loggedInUserSurname;
                        form.UserID = loggedUserID;
                        form.Show();
                        form.Show();
                    }
                    else if (role == "User")
                    {
                        FormMain_PDA form = new FormMain_PDA();
                        form.UserName = loggedInUserName;
                        form.UserSurname = loggedInUserSurname;
                        form.UserID = loggedUserID;
                        form.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Ваш аккаунт заблокировани. Обратитесь в поддержку: +7-996-173-01-57", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Hide();
                }
            }
            else
            {
                MessageBox.Show("Неверные имя пользователя или пароль.");
            }
        }

        private void FormLog_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}



