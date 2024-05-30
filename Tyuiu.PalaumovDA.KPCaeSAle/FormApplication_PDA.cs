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
    public partial class FormApplication_PDA : Form
    {
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=kp_carsale_pda;Uid=root;Pwd=Dimap2634;Charset=utf8;";
        public string UserID { get; set; }
        public FormApplication_PDA()
        {
            InitializeComponent();

            richTextBoxInfoApplication_PDA.BackColor = Color.FromArgb(94, 34, 123);

            richTextBoxAplication_PDA.Enter += RichTextBoxAplication_PDA_Enter;
            richTextBoxAplication_PDA.Leave += RichTextBoxAplication_PDA_Leave;

            SetGrayText();
        }
        private void RichTextBoxAplication_PDA_Enter(object sender, EventArgs e)
        {
            if (richTextBoxAplication_PDA.Text == "Раскажите про машину которую вы хотите: Марка, модель, год, состояние, мощность, ваш бюджет ")
            {
                richTextBoxAplication_PDA.Text = "";
                richTextBoxAplication_PDA.ForeColor = Color.Black;
            }
        }

        private void RichTextBoxAplication_PDA_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBoxAplication_PDA.Text))
            {
                SetGrayText();
            }
        }

        private void SetGrayText()
        {
            richTextBoxAplication_PDA.Text = "Раскажите про машину которую вы хотите: Марка, модель, год, состояние, мощность, ваш бюджет ";
            richTextBoxAplication_PDA.ForeColor = Color.Gray;
        }
        private void FormApplication_PDA_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(94, 34, 123);
            textBoxUserID.Text = UserID;
        }

        private void rjButtonLeaveAplication_PDA_Click(object sender, EventArgs e)
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
                    string Text = richTextBoxAplication_PDA.Text;
                    string Status = "Открыта";

                    string insertQuestionQuery = "INSERT INTO application (userID, messenger, addcoments, status) " +
                                                 "VALUES (@UserID, @Messenger, @AddComents, @Status)";
                    using (MySqlCommand insertQuestionCommand = new MySqlCommand(insertQuestionQuery, connection))
                    {
                        insertQuestionCommand.Parameters.AddWithValue("@UserID", UserID);
                        insertQuestionCommand.Parameters.AddWithValue("@Messenger", selectedMessenger);
                        insertQuestionCommand.Parameters.AddWithValue("@AddComents", Text);
                        insertQuestionCommand.Parameters.AddWithValue("@Status", Status);

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
