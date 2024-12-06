using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Test1_2025_Chumakov
{
    /// <summary>
    /// Interaction logic for Registracion.xaml
    /// </summary>
    public partial class Registracion : Window
    {
            private string connectionString = "Server=stud-mssql.sttec.yar.ru,38325;Database=user245_db;User Id=user245_db;Password=user245;";

            public Registracion()
            {
                InitializeComponent();
            }


            private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
            {
                // Логика показа/скрытия пароля
            }
        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedCountry = selectedItem.Content.ToString();

                // Очистка списка регионов перед добавлением новых значений
                RegionComboBox.Items.Clear();

                // Пример: добавление регионов в зависимости от выбранной страны
                if (selectedCountry == "Россия")
                {
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Московская область" });
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Мурманская область" });
                }
                else if (selectedCountry == "США")
                {
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Флорида" });
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Техас" });
                }
                else if (selectedCountry == "Армения")
                {
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Арагацотн" });
                    RegionComboBox.Items.Add(new ComboBoxItem { Content = "Арарат" });
                }

                // Выбор первого элемента по умолчанию после изменения страны
                if (RegionComboBox.Items.Count > 0)
                    RegionComboBox.SelectedIndex = 0;
            }
        }
        private int InsertCountry()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Strani (nazvanie) VALUES (@nazvanie); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nazvanie", ((ComboBoxItem)CountryComboBox.SelectedItem).Content.ToString());
                    return Convert.ToInt32(command.ExecuteScalar()); // Возвращаем ID вставленной страны
                }
            }
        }
        private int InsertRegion(int countryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Region (id_strani) VALUES (@id_strani); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_strani", countryId);
                    return Convert.ToInt32(command.ExecuteScalar()); // Возвращаем ID вставленного региона
                }
            }
        }


        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Логика обработки выбора региона (если необходимо)
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                // Логика регистрации
                if (ValidateInput())
                {
                    int countryId = InsertCountry(); // Вставка страны и получение ее ID
                    int regionId = InsertRegion(countryId); // Вставка региона и получение его ID
                    InsertUser(countryId, regionId); // Вставка пользователя

                    MessageBox.Show("Регистрация успешна!");

                    MainWindow loginWindow = new MainWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка в введенных данных.");
                }
            }

            private bool ValidateInput()
            {
                // Валидация входных данных
                string namePattern = @"^[а-яА-ЯёЁa-zA-Z]+$"; // Проверка на русские или латинские буквы
                return Regex.IsMatch(LastNameTextBox.Text.Trim(), namePattern) &&
                       Regex.IsMatch(FirstNameTextBox.Text.Trim(), namePattern) &&
                       Regex.IsMatch(MiddleNameTextBox.Text.Trim(), namePattern) &&
                       BirthDatePicker.SelectedDate.HasValue &&
                       EmailIsValid(EmailTextBox.Text.Trim()) &&
                       PasswordBox.Password.Length >= 8;
            }

            private bool EmailIsValid(string email)
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email.Trim(), emailPattern);
            }

        private void InsertUser(int countryId, int regionId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Zapisi (Familiya, Imya, Otchestvo, data_rozhdenie, id_regiona, id_strani, id_role, Email, Nomer_telephona, password) " +
                               "VALUES (@Familiya, @Imya, @Otchestvo, @data_rozhdenie, @id_regiona, @id_strani, @id_role, @Email, @Nomer_telephona, @password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Familiya", LastNameTextBox.Text);
                    command.Parameters.AddWithValue("@Imya", FirstNameTextBox.Text);
                    command.Parameters.AddWithValue("@Otchestvo", MiddleNameTextBox.Text);
                    command.Parameters.AddWithValue("@data_rozhdenie", BirthDatePicker.SelectedDate.Value);
                    command.Parameters.AddWithValue("@id_regiona", regionId);
                    command.Parameters.AddWithValue("@id_strani", countryId);
                    command.Parameters.AddWithValue("@id_role", 1); // Предположим роль по умолчанию
                    command.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                    command.Parameters.AddWithValue("@Nomer_telephona", PhoneTextBox.Visibility == Visibility.Visible && !string.IsNullOrEmpty(PhoneTextBox.Text)
                        ? PhoneTextBox.Text
                        : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@password", PasswordBox.Password);

                    command.ExecuteNonQuery(); // Выполнение запроса
                }
            }

        }
        private void UsePhoneCheckBox_Checked(object sender, RoutedEventArgs e)
            {
                PhoneLabel.Visibility = Visibility.Visible;
                PhoneTextBox.Visibility = Visibility.Visible;
            }
        private bool RoleExists(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM roole WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", roleId);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
        private void InsertRole(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO roole (id, role_name) VALUES (@id, 'Новая роль')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", roleId);
                    command.ExecuteNonQuery();
                }


            }

        }

        private void UsePhoneCheckBox_Unchecked(object sender, RoutedEventArgs e)
            {
                PhoneLabel.Visibility = Visibility.Collapsed;
                PhoneTextBox.Visibility = Visibility.Collapsed;
            }

            private void GoToLoginButton_Click(object sender, RoutedEventArgs e)
            {
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
    