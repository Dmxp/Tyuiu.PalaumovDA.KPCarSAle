using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormMain_PDA : Form
    {
        //string connectionString = "Server=127.0.0.1;Database=KP_CARSALE_PDA;Uid=root;Pwd=Dimap2634;SslMode=None;";

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        //public string LoginName { get; set; }

        public FormMain_PDA()
        {
            InitializeComponent();

            pictureBoxColor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonCalcCost_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            this.FormClosing += FormMain_PDA_FormClosing;

        }
        private void FormMain_PDA_FormClosing(object sender, FormClosingEventArgs e)
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
        private void FormMain_PDA_Load(object sender, EventArgs e)
        {
            labelNameMain_PDA.Text = UserName;
            labelSurnameMain_PDA.Text = UserSurname;
            labeluserid.Text = UserID;
        }
        private void rjButtonAutojap_PDA_Click(object sender, EventArgs e)
        {
            FormAutoJapan_PDA formAutoJap = new FormAutoJapan_PDA();
            formAutoJap.UserName = this.UserName;
            formAutoJap.UserSurname = this.UserSurname;
            formAutoJap.UserID = this.UserID;
            formAutoJap.Show();
            this.Hide();
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

        private void rbuttonquestion_PDA_Click(object sender, EventArgs e)
        {
            FormqQuestion_PDA formaauest = new FormqQuestion_PDA();
            formaauest.UserID = this.UserID;
            formaauest.Show();
        }

        private void rbuttonCalcCost_PDA_Click(object sender, EventArgs e)
        {
            FormApplication_PDA formapp = new FormApplication_PDA();
            formapp.UserID = this.UserID;
            formapp.Show();
        }
    }
}

