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
    public partial class FormInfoComp_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormInfoComp_PDA()
        {
            InitializeComponent();

            pictureBoxColor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            labelOComPAdm_PDA.ForeColor = Color.FromArgb(94, 34, 123);

            this.FormClosing += FormInfoComp_FormClosing;
        }

        private void FormInfoComp_PDA_Load(object sender, EventArgs e)
        {
            labelNameInCo_PDA.Text = UserName;
            labelSurnameInCo_PDA.Text = UserSurname;
            labeluserid.Text = UserID;

            LoadDataFromDatabase();

        }
        private void FormInfoComp_FormClosing(object sender, FormClosingEventArgs e)
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
        private void LoadDataFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT maininfo, nameorg, inn, slogan1, slogan2, slogan3, slogan4 FROM infocomp LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            labelmainText_PDA.Text = reader["maininfo"].ToString();
                            labelNameOrg_PDA.Text = reader["nameorg"].ToString();
                            labelinn_PDA.Text = reader["inn"].ToString();
                            labelslogan1_PDA.Text = reader["slogan1"].ToString();
                            labelslogan2_PDA.Text = reader["slogan2"].ToString();
                            labelslogan3_PDA.Text = reader["slogan3"].ToString();
                            labelslogan4_PDA.Text = reader["slogan4"].ToString();
                        }
                    }
                }
            }
        }
        private void labelmainText_PDA_Click(object sender, EventArgs e)
        {

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

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
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

        private void rjButtonInfo1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormContacts_PDA formacont = new FormContacts_PDA();
            formacont.UserName = this.UserName;
            formacont.UserSurname = this.UserSurname;
            formacont.UserID = this.UserID;
            formacont.Show();
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
