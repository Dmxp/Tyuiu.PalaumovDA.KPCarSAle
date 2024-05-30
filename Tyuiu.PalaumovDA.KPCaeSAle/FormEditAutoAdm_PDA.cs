using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormEditAutoAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        private string country;

        private bool changesMade = false;
        private bool isClosingByButton = false;

        public FormEditAutoAdm_PDA(string carID, string photo1, string photo2, string photo3, string photo4, string photo5, string brand, string model, string year, string body, string engine, string equipment, string mileage, string evaluation, string price, string typeofsale, string comment, string country)
        {
            InitializeComponent();

            this.country = country;

            rjButtonPhoto1EditAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto2EditAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonSaveChangEditAutoJapAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto3Edit_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonEditAutoJapAdm_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto5EditAutoJAp_PDA.BackColor = Color.FromArgb(94, 34, 123);

            textBoxCarIDEditAutoJapAdm_PDA.Text = carID;
            textboxPhoto1EditAdm_PDA.Text = photo1;
            textBoxPhoto2EditAdm_PDA.Text = photo2;
            textboxPhoto3EditAutoJapAdm_PDA.Text = photo3;
            textboxPhoto4EditAutoJapAdm_PDA.Text = photo4;
            textboxPhoto5EditAutoJapAdm_PDA.Text = photo5;
            textBoxBrandEditAdm_PDA.Text = brand;
            textBoxModelEditAdm_PDA.Text = model;
            textBoxYearEditAdm_PDA.Text = year;
            textBoxBodyEditAdm_PDA.Text = body;
            textBoxEngineEditAdm_PDA.Text = engine;
            textBoxEquipmentEditAdm_PDA.Text = equipment;
            textBoxMileageEditAdm_PDA.Text = mileage;
            textBoxEvaulationEditAdm_PDA.Text = evaluation;
            textBoxPriceEditAdm_PDA.Text = price;
            textBoxtypeofsaleAutoJapAd_PDA.Text = typeofsale;
            richTextBoxCommentAutoJapAdm_PDA.Text = comment;
            textBoxCountryEdit_PDA.Text = country;

            this.FormClosing += FormEditAutoJapAdm_PDA_FormClosing;
            foreach (Control control in Controls)
            {
                if (control is TextBox || control is RichTextBox)
                {
                    control.TextChanged += Control_TextChanged;
                }
            }
        }

        private void rjButtonPhoto1EditAdm_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Выберите фотографию 1",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto1EditAdm_PDA.Text = openFileDialog1.FileName;

                pictureBoxPhoto1EditAutoJap_PDA.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBoxPhoto1EditAutoJap_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void rjButtonPhoto2EditAdm_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                Title = "Выберите фотографию 2",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBoxPhoto2EditAdm_PDA.Text = openFileDialog2.FileName;

                pictureBoxPhoto2EditAutoJap_PDA.Image = Image.FromFile(openFileDialog2.FileName);
                pictureBoxPhoto2EditAutoJap_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void rjButtonPhoto3Edit_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog
            {
                Title = "Выберите фотографию 3",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto3EditAutoJapAdm_PDA.Text = openFileDialog3.FileName;

                pictureBoxPhoto3EditAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog3.FileName);
                pictureBoxPhoto3EditAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void rjButtonEditAutoJapAdm_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog4 = new OpenFileDialog
            {
                Title = "Выберите фотографию 4",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto4EditAutoJapAdm_PDA.Text = openFileDialog4.FileName;

                pictureBoxPhoto4EditAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog4.FileName);
                pictureBoxPhoto4EditAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void rjButtonPhoto5EditAutoJAp_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog5 = new OpenFileDialog
            {
                Title = "Выберите фотографию 5",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto5EditAutoJapAdm_PDA.Text = openFileDialog5.FileName;

                pictureBoxPhoto5EditAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog5.FileName);
                pictureBoxPhoto5EditAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void rjButtonSaveChangEditAutoJapAdm_PDA_Click(object sender, EventArgs e)
        {
            isClosingByButton = true;

            string carID = textBoxCarIDEditAutoJapAdm_PDA.Text;
            string photo1 = textboxPhoto1EditAdm_PDA.Text;
            string photo2 = textBoxPhoto2EditAdm_PDA.Text;
            string photo3 = textboxPhoto3EditAutoJapAdm_PDA.Text;
            string photo4 = textboxPhoto4EditAutoJapAdm_PDA.Text;
            string photo5 = textboxPhoto5EditAutoJapAdm_PDA.Text;
            string brand = textBoxBrandEditAdm_PDA.Text;
            string model = textBoxModelEditAdm_PDA.Text;
            string year = textBoxYearEditAdm_PDA.Text;
            string body = textBoxBodyEditAdm_PDA.Text;
            string engine = textBoxEngineEditAdm_PDA.Text;
            string equipment = textBoxEquipmentEditAdm_PDA.Text;
            string mileage = textBoxMileageEditAdm_PDA.Text;
            string evaluation = textBoxEvaulationEditAdm_PDA.Text;
            string price = textBoxPriceEditAdm_PDA.Text;
            string typeofsale = textBoxtypeofsaleAutoJapAd_PDA.Text;
            string comment = richTextBoxCommentAutoJapAdm_PDA.Text;
            string country = textBoxCountryEdit_PDA.Text;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "";
                    if (country == "Япония")
                    {
                        query = "UPDATE autojapan SET Photo1 = @Photo1, Photo2 = @Photo2, Photo3Watch = @Photo3Watch,  Photo4Watch = @Photo4Watch,  Photo5Watch = @Photo5Watch, Brand = @Brand, Model = @Model, Year = @Year, Body = @Body, EngCapacity = @EngCapacity, Equipment = @Equipment, Mileage = @Mileage, Evaluation = @Evaluation, Price = @Price, TypeofSale = @TypeofSale, comment = @Comment, country = @Country WHERE CarID = @CarID";
                    }
                    else if (country == "Корея")
                    {
                        query = "UPDATE autokorea SET Photo1 = @Photo1, Photo2 = @Photo2, Photo3Watch = @Photo3Watch,  Photo4Watch = @Photo4Watch,  Photo5Watch = @Photo5Watch, Brand = @Brand, Model = @Model, Year = @Year, Body = @Body, EngCapacity = @EngCapacity, Equipment = @Equipment, Mileage = @Mileage, Evaluation = @Evaluation, Price = @Price, TypeofSale = @TypeofSale, comment = @Comment, country = @Country WHERE CarID = @CarID";
                    }
                    else if (country == "Китай")
                    {
                        query = "UPDATE autochina SET Photo1 = @Photo1, Photo2 = @Photo2, Photo3Watch = @Photo3Watch,  Photo4Watch = @Photo4Watch,  Photo5Watch = @Photo5Watch, Brand = @Brand, Model = @Model, Year = @Year, Body = @Body, EngCapacity = @EngCapacity, Equipment = @Equipment, Mileage = @Mileage, Evaluation = @Evaluation, Price = @Price, TypeofSale = @TypeofSale, comment = @Comment, country = @Country WHERE CarID = @CarID";
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarID", carID);
                        command.Parameters.AddWithValue("@Photo1", photo1);
                        command.Parameters.AddWithValue("@Photo2", photo2);
                        command.Parameters.AddWithValue("@Photo3Watch", photo3);
                        command.Parameters.AddWithValue("@Photo4Watch", photo4);
                        command.Parameters.AddWithValue("@Photo5Watch", photo5);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Model", model);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@Body", body);
                        command.Parameters.AddWithValue("@EngCapacity", engine);
                        command.Parameters.AddWithValue("@Equipment", equipment);
                        command.Parameters.AddWithValue("@Mileage", mileage);
                        command.Parameters.AddWithValue("@Evaluation", evaluation);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@TypeofSale", typeofsale);
                        command.Parameters.AddWithValue("@Comment", comment);
                        command.Parameters.AddWithValue("@Country", country);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.Close();
                            if (Owner is FormAutoJapAdm_PDA parentForm)
                            {
                                parentForm.SearchButtonClicked();
                            }
                            if (Owner is FormAutoKorAdm_PDA parentForm1)
                            {
                                parentForm1.SearchButtonClicked();
                            }
                            if (Owner is FormAutoChinaAdm_PDA parentForm2)
                            {
                                parentForm2.SearchButtonClicked();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ни одна строка не была изменена.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void FormEditAutoJapAdm_PDA_Load(object sender, EventArgs e)
        {
            LoadImageToPictureBox(textboxPhoto1EditAdm_PDA.Text, pictureBoxPhoto1EditAutoJap_PDA);
            LoadImageToPictureBox(textBoxPhoto2EditAdm_PDA.Text, pictureBoxPhoto2EditAutoJap_PDA);
            LoadImageToPictureBox(textboxPhoto3EditAutoJapAdm_PDA.Text, pictureBoxPhoto3EditAutoJapAdm_PDA);
            LoadImageToPictureBox(textboxPhoto4EditAutoJapAdm_PDA.Text, pictureBoxPhoto4EditAutoJapAdm_PDA);
            LoadImageToPictureBox(textboxPhoto5EditAutoJapAdm_PDA.Text, pictureBoxPhoto5EditAutoJapAdm_PDA);
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void FormEditAutoJapAdm_PDA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosingByButton && changesMade)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    rjButtonSaveChangEditAutoJapAdm_PDA_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

