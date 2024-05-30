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

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormContactsAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormContactsAdm_PDA()
        {
            InitializeComponent();

            pictureBoxColor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelContsctsAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);
            rbuttonSaveChangContactsAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonapplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);

            this.Load += FormContactsAdm_PDA_Load;
            this.FormClosing += FormContactsAdm_PDA_FormClosing;
        }
        private void FormContactsAdm_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
        private void FormContactsAdm_PDA_Load(object sender, EventArgs e)
        {
            labelNameInCo_PDA.Text = UserName;
            labelSurnameInCo_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadDataFromDatabase();
        }
        private void LoadDataFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT infocontacts, phonenumber, whatsapp, telegram, mail, bannedgram, address FROM infocomp LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        richTextBoxinfocontactsAdm_PDA.Text = reader["infocontacts"].ToString();
                        textBoxPhoneNumberAdm_PDA.Text = reader["phonenumber"].ToString();
                        textBoxwhatsappAdm_PDA.Text = reader["whatsapp"].ToString();
                        textBoxtelegramAdm_PDA.Text = reader["telegram"].ToString();
                        textBoxmailAdm_PDA.Text = reader["mail"].ToString();
                        textBoxbannedgramAdm_PDA.Text = reader["bannedgram"].ToString();
                        textBoxaddressAdm_PDA.Text = reader["address"].ToString();
                    }

                    reader.Close();
                }
            }
        }

        private void rbuttonSaveChangContactsAdm_PDA_Click(object sender, EventArgs e)
        {
            string query = "UPDATE infocomp SET infocontacts = @infocontacts, phonenumber = @phonenumber, whatsapp = @whatsapp, telegram = @telegram, mail = @mail, bannedgram = @bannedgram, address = @address";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@infocontacts", richTextBoxinfocontactsAdm_PDA.Text);
                    command.Parameters.AddWithValue("@phonenumber", textBoxPhoneNumberAdm_PDA.Text);
                    command.Parameters.AddWithValue("@whatsapp", textBoxwhatsappAdm_PDA.Text);
                    command.Parameters.AddWithValue("@telegram", textBoxtelegramAdm_PDA.Text);
                    command.Parameters.AddWithValue("@mail", textBoxmailAdm_PDA.Text);
                    command.Parameters.AddWithValue("@bannedgram",textBoxbannedgramAdm_PDA.Text);
                    command.Parameters.AddWithValue("@address", textBoxaddressAdm_PDA.Text);

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Данные не были изменены. Пожалуйста, проверьте введенные значения и повторите попытку.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при обновлении данных: " + ex.Message);
                    }
                }
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormMainAdmin_PDA formAdm = new FormMainAdmin_PDA();
            formAdm.UserName = this.UserName;
            formAdm.UserSurname = this.UserSurname;
            formAdm.UserID = this.UserID;
            formAdm.Show();
            this.Hide();
        }

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
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
