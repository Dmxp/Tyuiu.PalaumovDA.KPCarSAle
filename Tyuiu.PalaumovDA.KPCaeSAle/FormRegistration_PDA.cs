using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormRegistration_PDA : Form
    {
        private FormLogin_PDA formLog;
        public FormRegistration_PDA()
        {
            InitializeComponent();
            formLog = new FormLogin_PDA();
            this.FormClosing += FormRegistration_PDA_FormClosing;
        }

        private void ButtonRegistr_PDA_Click(object sender, EventArgs e)
        {
            string username = textBoxLoginRegistr_PDA.Text;
            string Surname = textBoxSurnameRegistr_PDA.Text;
            string Name = textBoxName_registr_PDA.Text;
            string Mail = textBoxemail_PDA.Text;
            string password = textBoxPassRegistr_PDA.Text;
            string confirmPassword = textBoxPassCheckReg_PDA.Text;
            string phoneNumber = textBoxNumbPhRegist_PDA.Text;
            string city = textBoxCityRegistrati_PDA.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Хэширование пароля
            string hashedPassword = HashPassword(password);
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InsertUser(username, Surname, Name, Mail, hashedPassword, phoneNumber, city))
            {
                MessageBox.Show("Пользователь зарегистрирован успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                formLog.Show();
                this.Hide();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации пользователя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsUsernameTaken(string username)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM user1 WHERE username = @Username";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при проверке уникальности логина: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }
            }
        }
        private bool InsertUser(string username, string Surname, string Name, string Mail, string password, string phoneNumber, string city)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO user1 (username, password, surname, name1, mail, phonenumber, City, Role) " +
                               "VALUES (@Username, @Password, @Surname, @Name1, @Mail, @PhoneNumber, @City, @Role)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Surname", Surname);
                    command.Parameters.AddWithValue("@Name1", Name);
                    command.Parameters.AddWithValue("@Mail", Mail);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@Role", "User"); // Значение роли

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при вставке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }


        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
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
        private void FormRegistration_PDA_Load(object sender, EventArgs e)
        {
            textBoxPassRegistr_PDA.UseSystemPasswordChar = true;
            textBoxPassCheckReg_PDA.UseSystemPasswordChar = true;
        }
        private void FormRegistration_PDA_FormClosing(object sender, FormClosingEventArgs e)
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

        private void checkBoxVisPassRegistr_PDA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVisPassRegistr_PDA.Checked)
            {
                textBoxPassRegistr_PDA.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassRegistr_PDA.UseSystemPasswordChar = true;
            }
        }

        private void checkBoxVissPass2Registr_PDA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVissPass2Registr_PDA.Checked)
            {
                textBoxPassCheckReg_PDA.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassCheckReg_PDA.UseSystemPasswordChar = true;
            }
        }

        private void rjButtonBacktoLogin_PDA_Click(object sender, EventArgs e)
        {
            formLog.Show();
            this.Hide();
            this.Dispose();
        }
    }
}
