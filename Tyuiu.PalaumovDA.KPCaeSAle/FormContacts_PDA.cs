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
    public partial class FormContacts_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormContacts_PDA()
        {
            InitializeComponent();

            pictureBoxColor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelOComPAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);

            this.FormClosing += FormContacts_FormClosing;
        }
        private void FormContacts_FormClosing(object sender, FormClosingEventArgs e)
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
        private void FormContacts_PDA_Load(object sender, EventArgs e)
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
                string query = "SELECT infocontacts, phonenumber, whatsapp, telegram, bannedgram, mail, address FROM infocomp LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            labelmainText_PDA.Text = reader["infocontacts"].ToString();
                            labelphonenumber.Text = reader["phonenumber"].ToString();
                            labelwhatsapp.Text = reader["whatsapp"].ToString();
                            labeltelegram.Text = reader["telegram"].ToString();
                            labelbannedgram.Text = reader["bannedgram"].ToString();
                            labelmail.Text = reader["mail"].ToString();
                            labeladdress.Text = reader["address"].ToString();
                        }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormMain_PDA formMain = new FormMain_PDA();
            formMain.UserName = this.UserName;
            formMain.UserSurname = this.UserSurname;
            formMain.UserID = this.UserID;
            formMain.Show();
            this.Hide();
        }

        private void rjButtonAutojap1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapan_PDA formAutoJap = new FormAutoJapan_PDA();
            formAutoJap.UserName = this.UserName;
            formAutoJap.UserSurname = this.UserSurname;
            formAutoJap.UserID = this.UserID;
            formAutoJap.Show();
            this.Hide();
        }

        private void rjButtonAutoKor1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorea_PDA formAutokor = new FormAutoKorea_PDA();
            formAutokor.UserName = this.UserName;
            formAutokor.UserSurname = this.UserSurname;
            formAutokor.UserID = this.UserID;
            formAutokor.Show();
            this.Hide();
        }

        private void rjButtonAutoChina1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoChina_PDA formautochina = new FormAutoChina_PDA();
            formautochina.UserName = this.UserName;
            formautochina.UserSurname = this.UserSurname;
            formautochina.UserID = this.UserID;
            formautochina.Show();
            this.Hide();
        }

        private void rjButtonInfoComp1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormInfoComp_PDA forminco = new FormInfoComp_PDA();
            forminco.UserName = this.UserName;
            forminco.UserSurname = this.UserSurname;
            forminco.UserID = this.UserID;
            forminco.Show();
            this.Hide();
        }

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormqQuestion_PDA formaauest = new FormqQuestion_PDA();
            formaauest.UserID = this.UserID;
            formaauest.Show();
        }
    }
}
