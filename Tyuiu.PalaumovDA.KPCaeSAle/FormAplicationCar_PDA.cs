//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
//using MySql.Data.MySqlClient;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

//namespace Tyuiu.PalaumovDA.KPCaeSAle
//{
//    public partial class FormAplicationCar_PDA : Form
//    {
//        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

//        public string UserID { get; set; }
//        public string CarID { get; set; }
//        public string Country { get; set; }

//        public FormAplicationCar_PDA()
//        {
//            InitializeComponent();
//            richTextBoxInfoRewuest_PDA.BackColor = Color.FromArgb(94, 34, 123);
//        }

//        private void FormAplicationCar_PDA_Load(object sender, EventArgs e)
//        {
//            this.BackColor = Color.FromArgb(94, 34, 123);
//            textBoxUserID.Text = UserID;
//            textBoxcarID.Text = CarID;
//        }

//        private void rjButtonLeaveReq_PDA_Click(object sender, EventArgs e)
//        {
//            string MethodOfComm = comboBoxMessengerApl_PDA.SelectedItem?.ToString();
//            string AdditionalComment = richTextBoxAdditionalComment_PDA.Text;

//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    string brand = "";
//                    string model = "";
//                    string year = "";
//                    string body = "";
//                    string engCapacity = "";
//                    string equipment = "";
//                    string mileage = "";
//                    string evaluation = "";
//                    string price = "";
//                    string typeofsale = "";
//                    string comment = "";
//                    string photo1 = "";
//                    string photo2 = "";
//                    string photo3Watch = "";
//                    string photo4Watch = "";
//                    string photo5Watch = "";
//                    string name = "";
//                    string surname = "";
//                    string mail = "";
//                    string phoneNumber = "";
//                    string city = "";
//                    string country = "";
//                    string query = "";

//                    if (Country == "Япония")
//                    {
//                        query = @"SELECT
//                                Brand, 
//                                Model, 
//                                Year,
//                                Body,
//                                EngCapacity,
//                                Equipment,
//                                Mileage,
//                                Evaluation,
//                                Price,
//                                typeofsale,
//                                comment,
//                                Photo1,
//                                Photo2,
//                                Photo3Watch,
//                                Photo4Watch,
//                                Photo5Watch,
//                                country
//                            FROM 
//                                autojapan
//                            WHERE 
//                                carID = @CarID;";
//                    }
//                    else if (Country == "Корея")
//                    {
//                        query = @"SELECT
//                                Brand, 
//                                Model, 
//                                Year,
//                                Body,
//                                EngCapacity,
//                                Equipment,
//                                Mileage,
//                                Evaluation,
//                                Price,
//                                typeofsale,
//                                comment,
//                                Photo1,
//                                Photo2,
//                                Photo3Watch,
//                                Photo4Watch,
//                                Photo5Watch,
//                                country
//                            FROM 
//                                autokorea
//                            WHERE 
//                                carID = @CarIDK;";
//                    }
//                    else if (Country == "Китай")
//                    {
//                        query = @"SELECT
//                                Brand, 
//                                Model, 
//                                Year,
//                                Body,
//                                EngCapacity,
//                                Equipment,
//                                Mileage,
//                                Evaluation,
//                                Price,
//                                typeofsale,
//                                comment,
//                                Photo1,
//                                Photo2,
//                                Photo3Watch,
//                                Photo4Watch,
//                                Photo5Watch,
//                                country
//                            FROM 
//                                autochina
//                            WHERE 
//                                carID = @CarIDC;";
//                    }

//                    MySqlCommand commandAuto = new MySqlCommand(query, connection);
//                    commandAuto.Parameters.AddWithValue("@CarID", textBoxcarID.Text);
//                    commandAuto.Parameters.AddWithValue("@CarIDK", textBoxcarID.Text);
//                    commandAuto.Parameters.AddWithValue("@CarIDC", textBoxcarID.Text);

//                    using (MySqlDataReader readerAutoJapan = commandAuto.ExecuteReader())
//                    {
//                        if (readerAutoJapan.Read())
//                        {
//                            brand = readerAutoJapan["Brand"].ToString();
//                            model = readerAutoJapan["Model"].ToString();
//                            year = readerAutoJapan["Year"].ToString();
//                            body = readerAutoJapan["Body"].ToString();
//                            engCapacity = readerAutoJapan["EngCapacity"].ToString();
//                            equipment = readerAutoJapan["Equipment"].ToString();
//                            mileage = readerAutoJapan["Mileage"].ToString();
//                            evaluation = readerAutoJapan["Evaluation"].ToString();
//                            price = readerAutoJapan["Price"].ToString();
//                            typeofsale = readerAutoJapan["typeofsale"].ToString();
//                            comment = readerAutoJapan["comment"].ToString();
//                            photo1 = readerAutoJapan["Photo1"].ToString();
//                            photo2 = readerAutoJapan["Photo2"].ToString();
//                            photo3Watch = readerAutoJapan["Photo3Watch"].ToString();
//                            photo4Watch = readerAutoJapan["Photo4Watch"].ToString();
//                            photo5Watch = readerAutoJapan["Photo5Watch"].ToString();
//                            country = readerAutoJapan["Country"].ToString();
//                        }
//                        else
//                        {
//                            MessageBox.Show("Не удалось найти данные в таблице autojapan с указанным CarID", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }
//                    }
//                    string queryUser1 = @"SELECT 
//                                    Name1, 
//                                    Surname, 
//                                    Mail,
//                                    PhoneNumber,
//                                    City
//                                FROM 
//                                    user1
//                                WHERE 
//                                    UserID = @UserID;";

//                    MySqlCommand commandUser1 = new MySqlCommand(queryUser1, connection);
//                    commandUser1.Parameters.AddWithValue("@UserID", textBoxUserID.Text);

//                    using (MySqlDataReader readerUser1 = commandUser1.ExecuteReader())
//                    {
//                        if (readerUser1.Read())
//                        {
//                            name = readerUser1["Name1"].ToString();
//                            surname = readerUser1["Surname"].ToString();
//                            mail = readerUser1["Mail"].ToString();
//                            phoneNumber = readerUser1["PhoneNumber"].ToString();
//                            city = readerUser1["City"].ToString();
//                        }
//                        else
//                        {
//                            MessageBox.Show("Не удалось найти пользователя с указанным UserID", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }
//                    }
//                    string queryInsert = @"INSERT INTO application (Brand, Model, Year, Body, EngCapacity, Equipment, Mileage, Evaluation, Price, typeofsale, comment, Photo1, Photo2, Photo3Watch, Photo4Watch, Photo5Watch, Name1, Surname, Mail, PhoneNumber, City, UserID, Messenger, AddComents, Country, CarIdent, status)
//                                    VALUES (@Brand, @Model, @Year, @Body, @EngCapacity, @Equipment, @Mileage, @Evaluation, @Price, @TypeOfSale, @Comment, @Photo1, @Photo2, @Photo3Watch, @Photo4Watch, @Photo5Watch, @Name1, @Surname, @Mail, @PhoneNumber, @City, @UserID, @Messenger, @AddComents, @Country, @CarIdent, @Status);";

//                    MySqlCommand commandInsert = new MySqlCommand(queryInsert, connection);
//                    commandInsert.Parameters.AddWithValue("@Brand", brand);
//                    commandInsert.Parameters.AddWithValue("@Model", model);
//                    commandInsert.Parameters.AddWithValue("@Year", year);
//                    commandInsert.Parameters.AddWithValue("@Body", body);
//                    commandInsert.Parameters.AddWithValue("@EngCapacity", engCapacity);
//                    commandInsert.Parameters.AddWithValue("@Equipment", equipment);
//                    commandInsert.Parameters.AddWithValue("@Mileage", mileage);
//                    commandInsert.Parameters.AddWithValue("@Evaluation", evaluation);
//                    commandInsert.Parameters.AddWithValue("@Price", price);
//                    commandInsert.Parameters.AddWithValue("@TypeOfSale", typeofsale);
//                    commandInsert.Parameters.AddWithValue("@Comment", comment);
//                    commandInsert.Parameters.AddWithValue("@Photo1", photo1);
//                    commandInsert.Parameters.AddWithValue("@Photo2", photo2);
//                    commandInsert.Parameters.AddWithValue("@Photo3Watch", photo3Watch);
//                    commandInsert.Parameters.AddWithValue("@Photo4Watch", photo4Watch);
//                    commandInsert.Parameters.AddWithValue("@Photo5Watch", photo5Watch);
//                    commandInsert.Parameters.AddWithValue("@Name1", name);
//                    commandInsert.Parameters.AddWithValue("@Surname", surname);
//                    commandInsert.Parameters.AddWithValue("@Mail", mail);
//                    commandInsert.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
//                    commandInsert.Parameters.AddWithValue("@City", city);
//                    commandInsert.Parameters.AddWithValue("@UserID", textBoxUserID.Text);
//                    commandInsert.Parameters.AddWithValue("@CarIdent", textBoxcarID.Text);
//                    commandInsert.Parameters.AddWithValue("@Messenger", MethodOfComm);
//                    commandInsert.Parameters.AddWithValue("@AddComents", AdditionalComment);
//                    commandInsert.Parameters.AddWithValue("@Country", country);
//                    commandInsert.Parameters.AddWithValue("@Status", "Открыта"); 

//                    int rowsAffected = commandInsert.ExecuteNonQuery();

//                    if (rowsAffected > 0)
//                    {
//                        this.Close();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Произошла ошибка при добавлении данных в таблицу application", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Произошла ошибка при обращении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }
//    }
//}
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormAplicationCar_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";

        public string UserID { get; set; }
        public string CarID { get; set; }
        public string Country { get; set; }

        public FormAplicationCar_PDA()
        {
            InitializeComponent();
            richTextBoxInfoRewuest_PDA.BackColor = Color.FromArgb(94, 34, 123);
        }

        private void FormAplicationCar_PDA_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(94, 34, 123);
            textBoxUserID.Text = UserID;
            textBoxcarID.Text = CarID;
        }

        private void rjButtonLeaveReq_PDA_Click(object sender, EventArgs e)
        {
            string MethodOfComm = comboBoxMessengerApl_PDA.SelectedItem?.ToString();
            string AdditionalComment = richTextBoxAdditionalComment_PDA.Text;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string queryInsert = "";

                    if (Country == "Япония")
                    {
                        queryInsert = @"INSERT INTO application (UserID, Messenger, AddComents, Country, CarID, Status)
                                        VALUES (@UserID, @Messenger, @AddComents, @Country, @CarID, @Status);";
                    }
                    else if (Country == "Корея")
                    {
                        queryInsert = @"INSERT INTO application (UserID, Messenger, AddComents, Country, CarIDK, Status)
                                        VALUES (@UserID, @Messenger, @AddComents, @Country, @CarID, @Status);";
                    }
                    else if (Country == "Китай")
                    {
                        queryInsert = @"INSERT INTO application (UserID, Messenger, AddComents, Country, CarIDC, Status)
                                        VALUES (@UserID, @Messenger, @AddComents, @Country, @CarID, @Status);";
                    }

                    MySqlCommand commandInsert = new MySqlCommand(queryInsert, connection);
                    commandInsert.Parameters.AddWithValue("@UserID", textBoxUserID.Text);
                    commandInsert.Parameters.AddWithValue("@Messenger", MethodOfComm);
                    commandInsert.Parameters.AddWithValue("@AddComents", AdditionalComment);
                    commandInsert.Parameters.AddWithValue("@Country", Country);
                    commandInsert.Parameters.AddWithValue("@CarID", textBoxcarID.Text);
                    commandInsert.Parameters.AddWithValue("@Status", "Открыта");

                    int rowsAffected = commandInsert.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при добавлении данных в таблицу application", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обращении к базе данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


