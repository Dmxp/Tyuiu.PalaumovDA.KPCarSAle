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
    public partial class FormApplicationBidAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string applicationID { get; set; }
        private int currentPhotoIndex = 1;
        public FormApplicationBidAdm_PDA()
        {
            InitializeComponent();
        }

        private void FormApplicationBidAdm_PDA_Load(object sender, EventArgs e)
        {
            LoadApplicationData();
            LoadImageToPictureBox(textboxPhoto1Watch_PDA.Text, pictureBoxPhoto1Auto_PDA);
            
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
        private void LoadApplicationData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                            SELECT 
                                application.*, 
                            user1.Name1, user1.Surname, user1.Mail, user1.PhoneNumber, user1.City,
                                COALESCE(autojapan.Brand, autokorea.Brand, autochina.Brand) AS Brand,
                                COALESCE(autojapan.Model, autokorea.Model, autochina.Model) AS Model,
                                COALESCE(autojapan.Year, autokorea.Year, autochina.Year) AS Year,
                                COALESCE(autojapan.Body, autokorea.Body, autochina.Body) AS Body,
                                COALESCE(autojapan.EngCapacity, autokorea.EngCapacity, autochina.EngCapacity) AS EngCapacity,
                                COALESCE(autojapan.Equipment, autokorea.Equipment, autochina.Equipment) AS Equipment,
                                COALESCE(autojapan.Photo1, autokorea.Photo1, autochina.Photo1) AS Photo1,
                                COALESCE(autojapan.Photo2, autokorea.Photo2, autochina.Photo2) AS Photo2,
                                COALESCE(autojapan.Photo3Watch, autokorea.Photo3Watch, autochina.Photo3Watch) AS Photo3Watch,
                                COALESCE(autojapan.Photo4Watch, autokorea.Photo4Watch, autochina.Photo4Watch) AS Photo4Watch,
                                COALESCE(autojapan.Photo5Watch, autokorea.Photo5Watch, autochina.Photo5Watch) AS Photo5Watch,
                                COALESCE(autojapan.Mileage, autokorea.Mileage, autochina.Mileage) AS Mileage,
                                COALESCE(autojapan.Evaluation, autokorea.Evaluation, autochina.Evaluation) AS Evaluation,
                                COALESCE(autojapan.Price, autokorea.Price, autochina.Price) AS Price,
                                COALESCE(autojapan.typeofsale, autokorea.typeofsale, autochina.typeofsale) AS typeofsale,
                                COALESCE(autojapan.comment, autokorea.comment, autochina.comment) AS comment
                            FROM application
                            INNER JOIN user1 ON application.userID = user1.userID
                            LEFT JOIN autojapan ON application.CarID = autojapan.CarID
                            LEFT JOIN autokorea ON application.CarIDK = autokorea.CarID
                            LEFT JOIN autochina ON application.CarIDC = autochina.CarID
                            WHERE application.applicationID = @applicationID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@applicationID", applicationID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NumberApplicAdm_PDA.Text = reader["applicationID"].ToString();
                                textboxName1Adm_PDA.Text = reader["Name1"].ToString();
                                textBoxSurNameAdm_PDA.Text = reader["Surname"].ToString();
                                textBoxMailAdm_PDA.Text = reader["Mail"].ToString();
                                textBoxPhoneNumber.Text = reader["PhoneNumber"].ToString();
                                textBoxCityAdm_PDA.Text = reader["City"].ToString();
                                textBoxBrandWatch_PDA.Text = reader["Brand"].ToString();
                                textBoxModelWatch_PDA.Text = reader["Model"].ToString();
                                textBoxYearWatchAutoJap_PDA.Text = reader["Year"].ToString();
                                textBoxBodyWatch_PDA.Text = reader["Body"].ToString();
                                textBoxEngineWatch_PDA.Text = reader["EngCapacity"].ToString();
                                textBoxEquipmentWatch_PDA.Text = reader["Equipment"].ToString();
                                textBoxMileageWatch_PDA.Text = reader["Mileage"].ToString();
                                textBoxEvaulationWatch_PDA.Text = reader["Evaluation"].ToString();
                                textBoxPriceWatch_PDA.Text = reader["Price"].ToString();
                                textBoxtypeofsaleWatchAutoJapAd_PDA.Text = reader["typeofsale"].ToString();
                                textBoxcountryWatch_PDA.Text = reader["Country"].ToString();
                                richTextBoxCommentWatchAutoJapAdm_PDA.Text = reader["comment"].ToString();
                                textboxmessengerAdm_PDA.Text = reader["Messenger"].ToString();
                                richTextBoxaddcomentsAdm_PDA.Text = reader["AddComents"].ToString();
                                textboxPhoto1Watch_PDA.Text = reader["Photo1"].ToString();
                                textBoxPhoto2Watch_PDA.Text = reader["Photo2"].ToString();
                                textBoxPhoto3Watch_PDA.Text = reader["Photo3Watch"].ToString();
                                textBoxPhoto4Watch_PDA.Text = reader["Photo4Watch"].ToString();
                                textBoxPhoto5Watch_PDA.Text = reader["Photo5Watch"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Запись с указанным applicationID не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    LoadImageToPictureBox(textBox.Text, pictureBoxPhoto1Auto_PDA);
                }
            }
        }

        private void buttonnext_Click(object sender, EventArgs e)
        {
            currentPhotoIndex++;
            string textBoxName = "textBoxPhoto" + currentPhotoIndex + "Watch_PDA";
            TextBox textBox = this.Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;

            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                LoadImageToPictureBox(textBox.Text, pictureBoxPhoto1Auto_PDA);
            }
            else
            {
                currentPhotoIndex--;
            }
        }
    }
}
