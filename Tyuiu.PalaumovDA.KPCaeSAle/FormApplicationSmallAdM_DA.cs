//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
//using MySql.Data.MySqlClient;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

//namespace Tyuiu.PalaumovDA.KPCaeSAle
//{
//    public partial class FormApplicationSmallAdM_DA : Form
//    {
//        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
//        public string applicationID { get; set; }

//        public FormApplicationSmallAdM_DA()
//        {
//            InitializeComponent();
//        }



//        private void LoadApplicationData()
//        {
//            try
//            {
//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    connection.Open();
//                    string query = "SELECT * FROM application WHERE applicationID = @applicationID";
//                    using (MySqlCommand command = new MySqlCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@applicationID", applicationID);
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                labelNumberapplAdm_PDA.Text = reader["applicationID"].ToString();
//                                textBoxUserID.Text = reader["userID"].ToString();
//                                textBoxName1Adm_PDA.Text = reader["Name1"].ToString();
//                                textBoxSurnameAdm_PDA.Text = reader["Surname"].ToString();
//                                textBoxmailAdm_PDA.Text = reader["Mail"].ToString();
//                                textBoxphonenumberAdm_PDA.Text = reader["PhoneNumber"].ToString();
//                                textBoxCityAdm_PDA.Text = reader["City"].ToString();
//                                textBoxmessengerAdm_PDA.Text = reader["Messenger"].ToString();
//                                richTextBoxCommentsAdm_PDA.Text = reader["AddComents"].ToString();
//                            }
//                            else
//                            {
//                                MessageBox.Show("Данные не найдены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void FormApplicationSmallAdM_DA_Load(object sender, EventArgs e)
//        {
//            LoadApplicationData();
//            this.BackColor = Color.FromArgb(94, 34, 123);
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
    public partial class FormApplicationSmallAdM_DA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string applicationID { get; set; }

        public FormApplicationSmallAdM_DA()
        {
            InitializeComponent();
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
                            user1.Name1, user1.Surname, user1.Mail, user1.PhoneNumber, user1.City
                        FROM 
                            application
                        INNER JOIN 
                            user1 ON application.userID = user1.userID
                        WHERE 
                            application.applicationID = @applicationID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@applicationID", applicationID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                labelNumberapplAdm_PDA.Text = reader["applicationID"].ToString();
                                textBoxUserID.Text = reader["userID"].ToString();
                                textBoxName1Adm_PDA.Text = reader["Name1"].ToString();
                                textBoxSurnameAdm_PDA.Text = reader["Surname"].ToString();
                                textBoxmailAdm_PDA.Text = reader["Mail"].ToString();
                                textBoxphonenumberAdm_PDA.Text = reader["PhoneNumber"].ToString();
                                textBoxCityAdm_PDA.Text = reader["City"].ToString();
                                textBoxmessengerAdm_PDA.Text = reader["Messenger"].ToString();
                                richTextBoxCommentsAdm_PDA.Text = reader["AddComents"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Данные не найдены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormApplicationSmallAdM_DA_Load(object sender, EventArgs e)
        {
            LoadApplicationData();
            this.BackColor = Color.FromArgb(94, 34, 123);
        }
    }
}
