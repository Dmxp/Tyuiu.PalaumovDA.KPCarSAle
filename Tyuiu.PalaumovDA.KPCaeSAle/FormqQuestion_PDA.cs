using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tyuiu.PalaumovDA.KPCaeSAle
{
    public partial class FormqQuestion_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserID { get; set; }

        public FormqQuestion_PDA()
        {
            InitializeComponent();
            richTextBoxInfoQuestion_PDA.BackColor = Color.FromArgb(94, 34, 123);
        }

        private void FormqQuestion_PDA_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(94, 34, 123);
            textBoxUserID.Text = UserID;
        }

        private void rjButtonLeaveQuestion_PDA_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID))
                {
                    MessageBox.Show("UserID не определен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string selectedMessenger = comboBoxMessengerApl_PDA.SelectedItem?.ToString();
                    string questionText = richTextBoxQuestion_PDA.Text;

                    string insertQuestionQuery = "INSERT INTO questions (userID, messenger, question, Status) " +
                        "VALUES (@UserID, @Messenger, @Question, @Status)";
                    using (MySqlCommand insertQuestionCommand = new MySqlCommand(insertQuestionQuery, connection))
                    {
                        insertQuestionCommand.Parameters.AddWithValue("@UserID", UserID);
                        insertQuestionCommand.Parameters.AddWithValue("@Messenger", selectedMessenger);
                        insertQuestionCommand.Parameters.AddWithValue("@Question", questionText);
                        insertQuestionCommand.Parameters.AddWithValue("@Status", "Не рассмотрен");

                        int rowsAffected = insertQuestionCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось сохранить вопрос", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении вопроса: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
