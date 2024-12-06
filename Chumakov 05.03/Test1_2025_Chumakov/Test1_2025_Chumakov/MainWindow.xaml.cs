using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Windows.Shapes;

namespace Test1_2025_Chumakov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=stud-mssql.sttec.yar.ru,38325;Database=user245_db;User Id=user245_db;Password=user245;"; // Замените your_password на фактический пароль

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                ShowPasswordButton.Content = "Показать";
            }
            else
            {
                PasswordBox.Visibility = Visibility.Visible;
                ShowPasswordButton.Content = "Скрыть";
            }
        }

        private void UsePhoneCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PhoneLabel.Visibility = Visibility.Visible;
            PhoneTextBox.Visibility = Visibility.Visible;
            EmailTextBox.Visibility = Visibility.Collapsed;
        }

        private void UsePhoneCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneLabel.Visibility = Visibility.Collapsed;
            PhoneTextBox.Visibility = Visibility.Collapsed;
            EmailTextBox.Visibility = Visibility.Visible;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^\+(\d{1,3})\s?\((\d{3})\)\s?\d{3}-\d{4}$";

            if (UsePhoneCheckBox.IsChecked == true)
            {
                if (!Regex.IsMatch(PhoneTextBox.Text, phonePattern))
                {
                    MessageBox.Show("Некорректный номер телефона.");
                    return;
                }
            }
            else
            {
                if (EmailTextBox.Text.Length < 5 || !Regex.IsMatch(EmailTextBox.Text, emailPattern))
                {
                    MessageBox.Show("Некорректный адрес электронной почты.");
                    return;
                }
            }

            if (PasswordBox.Password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов.");
                return;
            }

      

            MessageBox.Show("Авторизация успешна!");
            // Здесь можно добавить логику для перехода на другое окно
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Registracion registracion = new Registracion();
            registracion.Show();
            this.Close();
        }
        private void InsertUserIntoDatabase(string familiya, string imya, string otchestvo, DateTime dateOfBirth, string email, string strana, string region, string naselpunkt, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Userrs (Familiya, Imya, Otchestvo, date_of_birth, Mail, strana, region, naselpunkt, Pass) VALUES (@Familiya, @Imya, @Otchestvo, @date_of_birth, @Mail, @strana, @region, @naselpunkt, @Pass)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Familiya", familiya);
                    command.Parameters.AddWithValue("@Imya", imya);
                    command.Parameters.AddWithValue("@Otchestvo", otchestvo);
                    command.Parameters.AddWithValue("@date_of_birth", dateOfBirth);
                    command.Parameters.AddWithValue("@Mail", email);
                    command.Parameters.AddWithValue("@strana", strana);
                    command.Parameters.AddWithValue("@region", region);
                    command.Parameters.AddWithValue("@naselpunkt", naselpunkt);
                    command.Parameters.AddWithValue("@Pass", password);

                    command.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

    }
}


