using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormWatchAutoJap_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

        private int currentPhotoIndex = 1;

        public string UserID { get; set; }
        public FormWatchAutoJap_PDA(string carID, string photo1, string photo2, string photo3, string photo4, string photo5, string brand, string model, string year, string body, string engine, string equipment, string mileage, string evaluation, string price, string typeofsale, string comment, string country)
        {
            InitializeComponent();

            rbuttonrequestAutoJap_PDA.BackColor = Color.FromArgb(94, 34, 123);

            textBoxCarID_PDA.Text = carID;
            textboxPhoto1Watch_PDA.Text = photo1;
            textBoxPhoto2Watch_PDA.Text = photo2;
            textBoxPhoto3Watch_PDA.Text = photo3;
            textBoxPhoto4Watch_PDA.Text = photo4;
            textBoxPhoto5Watch_PDA.Text = photo5;
            textBoxBrandWatch_PDA.Text = brand;
            textBoxModelWatch_PDA.Text = model;
            textBoxYearWatchAutoJap_PDA.Text = year;
            textBoxBodyWatch_PDA.Text = body;
            textBoxEngineWatch_PDA.Text = engine;
            textBoxEquipmentWatch_PDA.Text = equipment;
            textBoxMileageWatch_PDA.Text = mileage;
            textBoxEvaulationWatch_PDA.Text = evaluation;
            textBoxPriceWatch_PDA.Text = price;
            textBoxtypeofsaleWatchAutoJapAd_PDA.Text = typeofsale;
            richTextBoxCommentWatchAutoJapAdm_PDA.Text = comment;
            textBoxcountryWatch_PDA.Text = country;
        }

        private void FormWatchAutoJap_PDA_Load(object sender, EventArgs e)
        {
            LoadImageToPictureBox(textboxPhoto1Watch_PDA.Text, pictureBoxPhoto1AutoJap_PDA);

            textBoxuserid.Text = UserID;
        }
        private void LoadImageToPictureBox(string imagePath, PictureBox pictureBox)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    pictureBox.Image = Image.FromFile(imagePath);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        private void buttonnext_Click(object sender, EventArgs e)
        {
            currentPhotoIndex++;
            string textBoxName = "textBoxPhoto" + currentPhotoIndex + "Watch_PDA";
            TextBox textBox = this.Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                LoadImageToPictureBox(textBox.Text, pictureBoxPhoto1AutoJap_PDA);
            }
            else
            {
                currentPhotoIndex--;
            }
        }

        private void buttonprevious_Click(object sender, EventArgs e)
        {
            if (currentPhotoIndex > 1)
            {
                currentPhotoIndex--;
                string textBoxName = "textBoxPhoto" + currentPhotoIndex + "Watch_PDA";
                TextBox textBox = this.Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

                if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
                {
                    LoadImageToPictureBox(textBox.Text, pictureBoxPhoto1AutoJap_PDA);
                }
            }
        }

        private void rbuttonrequestAutoJap_PDA_Click(object sender, EventArgs e)
        {
            FormAplicationCar_PDA formreq = new FormAplicationCar_PDA();
            formreq.CarID = textBoxCarID_PDA.Text;
            formreq.UserID = textBoxuserid.Text;
            formreq.Country = textBoxcountryWatch_PDA.Text;
            formreq.Show();
        }
    }
    
}
