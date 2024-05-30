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
    public partial class FormMainAdmin_PDA : Form
    {

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserID { get; set; }
        public FormMainAdmin_PDA()
        {
            InitializeComponent();

            pictureBoxColor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutojap1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoKor1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAutoChina1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfoComp1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonInfo1Adm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonChangeUserMain_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonUsersAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonApplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rbuttonquestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonApplicationAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);

            this.FormClosing += FormMainAdmin_PDA_FormClosing;
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

        private void FormMainAdmin_PDA_FormClosing(object sender, FormClosingEventArgs e)
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

        private void rjButtonAutoKor1Adm_PDA_Click(object sender, EventArgs e)
        {
            FormAutoKorAdm_PDA formAutoKorAdm = new FormAutoKorAdm_PDA();
            formAutoKorAdm.UserName = this.UserName;
            formAutoKorAdm.UserSurname = this.UserSurname;
            formAutoKorAdm.UserID = this.UserID;
            formAutoKorAdm.Show();
            this.Hide();
        }

        private void FormMainAdmin_PDA_Load(object sender, EventArgs e)
        {
            labelNameMain_PDA.Text = UserName;
            labelSurnameMain_PDA.Text = UserSurname;
            labeluserid.Text = UserID;
        }

        private void rbuttonChangeUserMain_PDA_Click(object sender, EventArgs e)
        {
            FormLogin_PDA formLogin = new FormLogin_PDA();
            formLogin.Show();
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

        private void rjButtonApplicationAdm_PDA_Click(object sender, EventArgs e)
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

