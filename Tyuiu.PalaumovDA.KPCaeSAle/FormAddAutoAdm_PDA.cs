using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormAddAutoAdm_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

        public FormAddAutoAdm_PDA()
        {
            InitializeComponent();

            rjButtonPhoto1Add_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto2Add_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonAddAuto_DA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto3Add_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto4Add_PDA.BackColor = Color.FromArgb(94, 34, 123);
            rjButtonPhoto5Add_PDA.BackColor = Color.FromArgb(94, 34, 123);

        }
        private void rjButtonAddAuto_DA_Click(object sender, EventArgs e)
        {
            string photo1Path = textboxPhoto1Add_PDA.Text;
            string photo2Path = textBoxPhoto2Add_PDA.Text;
            string brand = textBoxBrandAdd_PDA.Text;
            string model = textBoxModelAdd_PDA.Text;
            int year = Convert.ToInt32(textBoxYearAdd_PDA.Text);
            string body = textBoxBodyAdd_PDA.Text;
            string engine = textBoxEngineAdd_PDA.Text;
            string equipment = textBoxEquipmentAdd_PDA.Text;
            int mileage = Convert.ToInt32(textBoxMileage_PDA.Text);
            string evaluation = textBoxEvaulation_PDA.Text;
            decimal price = Convert.ToDecimal(textBoxPriceAdd_PDA.Text);
            string typeofsale = textBoxtypeofsaleAutoJapAdd_PDA.Text;
            string comment = richTextBoxCommentAutoJapAdm_PDA.Text;
            string photo3path = textboxPhoto3Add_PDA.Text;
            string photo4path = textboxPhoto4Add_PDA.Text;
            string photo5path = textboxPhoto5Add_PDA.Text;
            string country = textBoxCountryAddJapAdm_PDA.Text;
            string status = "Активна"; // Значение по умолчанию

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "";

                    if (country == "Япония")
                    {
                        query = "INSERT INTO AutoJapan (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country, Status) " +
                                "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country, @Status)";
                    }
                    else if (country == "Корея")
                    {
                        query = "INSERT INTO AutoKorea (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country, Status) " +
                                "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country, @Status)";
                    }
                    else if (country == "Китай")
                    {
                        query = "INSERT INTO AutoChina (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country, Status) " +
                                "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country, @Status)";
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Photo1", photo1Path);
                        command.Parameters.AddWithValue("@Photo2", photo2Path);
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
                        command.Parameters.AddWithValue("@Photo3Watch", photo3path);
                        command.Parameters.AddWithValue("@Photo4Watch", photo4path);
                        command.Parameters.AddWithValue("@Photo5Watch", photo5path);
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@Status", status);

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
                            // Очистка полей и изображений после успешного добавления данных
                            textboxPhoto1Add_PDA.Clear();
                            textBoxPhoto2Add_PDA.Clear();
                            textBoxBrandAdd_PDA.Clear();
                            textBoxModelAdd_PDA.Clear();
                            textBoxYearAdd_PDA.Clear();
                            textBoxBodyAdd_PDA.Clear();
                            textBoxEngineAdd_PDA.Clear();
                            textBoxEquipmentAdd_PDA.Clear();
                            textBoxMileage_PDA.Clear();
                            textBoxEvaulation_PDA.Clear();
                            textBoxPriceAdd_PDA.Clear();
                            textBoxtypeofsaleAutoJapAdd_PDA.Clear();
                            richTextBoxCommentAutoJapAdm_PDA.Clear();
                            textboxPhoto3Add_PDA.Clear();
                            textboxPhoto4Add_PDA.Clear();
                            textboxPhoto5Add_PDA.Clear();
                            textBoxCountryAddJapAdm_PDA.Clear();

                            pictureBoxPhoto1AddAutoJap_PDA.Image = null;
                            pictureBoxPhoto2AddAutoJap_PDA.Image = null;
                            pictureBoxPhoto3AddAutoJapAdm_PDA.Image = null;
                            pictureBoxPhoto4AddAutoJapAdm_PDA.Image = null;
                            pictureBoxPhoto5AddAutoJapAdm_PDA.Image = null;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении данных в базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных в базу данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void rjButtonAddAuto_DA_Click(object sender, EventArgs e)
        //{
        //    string photo1Path = textboxPhoto1Add_PDA.Text;
        //    string photo2Path = textBoxPhoto2Add_PDA.Text;
        //    string brand = textBoxBrandAdd_PDA.Text;
        //    string model = textBoxModelAdd_PDA.Text;
        //    int year = Convert.ToInt32(textBoxYearAdd_PDA.Text);
        //    string body = textBoxBodyAdd_PDA.Text;
        //    string engine = textBoxEngineAdd_PDA.Text;
        //    string equipment = textBoxEquipmentAdd_PDA.Text;
        //    int mileage = Convert.ToInt32(textBoxMileage_PDA.Text);
        //    string evaluation = textBoxEvaulation_PDA.Text;
        //    decimal price = Convert.ToDecimal(textBoxPriceAdd_PDA.Text);
        //    string typeofsale = textBoxtypeofsaleAutoJapAdd_PDA.Text;
        //    string comment = richTextBoxCommentAutoJapAdm_PDA.Text;
        //    string photo3path = textboxPhoto3Add_PDA.Text;
        //    string photo4path = textboxPhoto4Add_PDA.Text;
        //    string photo5path = textboxPhoto5Add_PDA.Text;
        //    string country = textBoxCountryAddJapAdm_PDA.Text;

        //    try
        //    {
        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            string query = "";

        //            if (country == "Япония")
        //            {
        //                query = "INSERT INTO AutoJapan (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country) " +
        //                        "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country)";
        //            }
        //            else if (country == "Корея")
        //            {
        //                query = "INSERT INTO AutoKorea (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country) " +
        //                        "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country)";
        //            }
        //            else if (country == "Китай")
        //            {
        //                query = "INSERT INTO AutoChina (Photo1, Photo2, Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo3Watch, Photo4Watch, Photo5Watch, country) " +
        //                        "VALUES (@Photo1, @Photo2, @Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeofSale, @Comment, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Country)";
        //            }

        //            using (MySqlCommand command = new MySqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@Photo1", photo1Path);
        //                command.Parameters.AddWithValue("@Photo2", photo2Path);
        //                command.Parameters.AddWithValue("@Brand", brand);
        //                command.Parameters.AddWithValue("@Model", model);
        //                command.Parameters.AddWithValue("@Year", year);
        //                command.Parameters.AddWithValue("@Body", body);
        //                command.Parameters.AddWithValue("@EngCapacity", engine);
        //                command.Parameters.AddWithValue("@Equipment", equipment);
        //                command.Parameters.AddWithValue("@Mileage", mileage);
        //                command.Parameters.AddWithValue("@Evaluation", evaluation);
        //                command.Parameters.AddWithValue("@Price", price);
        //                command.Parameters.AddWithValue("@TypeofSale", typeofsale);
        //                command.Parameters.AddWithValue("@Comment", comment);
        //                command.Parameters.AddWithValue("@Photo3Watch", photo3path);
        //                command.Parameters.AddWithValue("@Photo4Watch", photo4path);
        //                command.Parameters.AddWithValue("@Photo5Watch", photo5path);
        //                command.Parameters.AddWithValue("@Country", country);

        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    this.Close();
        //                    if (Owner is FormAutoJapAdm_PDA parentForm)
        //                    {
        //                        parentForm.SearchButtonClicked();
        //                    }
        //                    if (Owner is FormAutoKorAdm_PDA parentForm1)
        //                    {
        //                        parentForm1.SearchButtonClicked();
        //                    }
        //                    if (Owner is FormAutoChinaAdm_PDA parentForm2)
        //                    {
        //                        parentForm2.SearchButtonClicked();
        //                    }
        //                    // Очистка полей и изображений после успешного добавления данных
        //                    textboxPhoto1Add_PDA.Clear();
        //                    textBoxPhoto2Add_PDA.Clear();
        //                    textBoxBrandAdd_PDA.Clear();
        //                    textBoxModelAdd_PDA.Clear();
        //                    textBoxYearAdd_PDA.Clear();
        //                    textBoxBodyAdd_PDA.Clear();
        //                    textBoxEngineAdd_PDA.Clear();
        //                    textBoxEquipmentAdd_PDA.Clear();
        //                    textBoxMileage_PDA.Clear();
        //                    textBoxEvaulation_PDA.Clear();
        //                    textBoxPriceAdd_PDA.Clear();
        //                    textBoxtypeofsaleAutoJapAdd_PDA.Clear();
        //                    richTextBoxCommentAutoJapAdm_PDA.Clear();
        //                    textboxPhoto3Add_PDA.Clear();
        //                    textboxPhoto4Add_PDA.Clear();
        //                    textboxPhoto5Add_PDA.Clear();
        //                    textBoxCountryAddJapAdm_PDA.Clear();

        //                    pictureBoxPhoto1AddAutoJap_PDA.Image = null;
        //                    pictureBoxPhoto2AddAutoJap_PDA.Image = null;
        //                    pictureBoxPhoto3AddAutoJapAdm_PDA.Image = null;
        //                    pictureBoxPhoto4AddAutoJapAdm_PDA.Image = null;
        //                    pictureBoxPhoto5AddAutoJapAdm_PDA.Image = null;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Ошибка при добавлении данных в базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка при добавлении данных в базу данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        private void rjButtonPhoto1Add_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Выберите фотографию 1",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto1Add_PDA.Text = openFileDialog1.FileName;

                pictureBoxPhoto1AddAutoJap_PDA.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBoxPhoto1AddAutoJap_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void rjButtonPhoto2Add_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                Title = "Выберите фотографию 2",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBoxPhoto2Add_PDA.Text = openFileDialog2.FileName;

                pictureBoxPhoto2AddAutoJap_PDA.Image = Image.FromFile(openFileDialog2.FileName);
                pictureBoxPhoto2AddAutoJap_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void rjButtonPhoto3Add_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog
            {
                Title = "Выберите фотографию 1",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto3Add_PDA.Text = openFileDialog3.FileName;

                pictureBoxPhoto3AddAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog3.FileName);
                pictureBoxPhoto3AddAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void rjButtonPhoto4Add_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog4 = new OpenFileDialog
            {
                Title = "Выберите фотографию 1",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto4Add_PDA.Text = openFileDialog4.FileName;

                pictureBoxPhoto4AddAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog4.FileName);
                pictureBoxPhoto4AddAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void rjButtonPhoto5Add_PDA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog5 = new OpenFileDialog
            {
                Title = "Выберите фотографию 1",
                Filter = "Изображения (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textboxPhoto5Add_PDA.Text = openFileDialog5.FileName;

                pictureBoxPhoto5AddAutoJapAdm_PDA.Image = Image.FromFile(openFileDialog5.FileName);
                pictureBoxPhoto5AddAutoJapAdm_PDA.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
