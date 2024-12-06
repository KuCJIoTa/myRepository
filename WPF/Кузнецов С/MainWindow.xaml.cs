using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Кузнецов_С
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _attempts = 0; 
        private string _currentCaptcha; 

        Random _random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            UpdateCaptcha();
        }

        private void UpdateCaptcha()
        {
            SPanelSymbols.Children.Clear(); // очистка Стак Панела
            CanvasNoise.Children.Clear();//очистка Канвас

            _currentCaptcha = GenerateSymbols(4); // наша функция на создание 4 символов (можно сделать больше)
        GenerateNoise(30); // Функция на создание «Шума»
         }

        private string GenerateSymbols(int count) // создаем функцию и получаем число символов
        {
            string alphabet = "1234567890"; // задаем алфавит
            string generatedSymbols = ""; 
            for (int i = 0; i < count; i++) // запускаем цикл
            {
                string symbol = alphabet.ElementAt(_random.Next(0, alphabet.Length)).ToString(); // с помощью рандома выбираем случаный символ из алфавита
                generatedSymbols += symbol;
                TextBlock lbl = new TextBlock(); //создаем текст блок в который поместим наш символ

                lbl.Text = symbol;
                lbl.FontSize = _random.Next(40, 80); //задаем случайный размер шрифта в диапазоне от 40 до 80
                lbl.RenderTransform = new RotateTransform(_random.Next(-45, 45)); // задаем случайный угол
                lbl.Margin = new Thickness(20, 0, 20, 0); // задаем случайные отступы

                SPanelSymbols.Children.Add(lbl); // добавляем новый текст блок на наш стак панел 
            }
            return generatedSymbols;
        }

        private void GenerateNoise(int volumeNoise) // создаем функцию и получаем число элементов
        {
            for (int i = 0; i < volumeNoise; i++) // цикл создания элементов
            {
                Border border = new Border(); //задаем новую рамку
                border.Background = new SolidColorBrush(Color.FromArgb((byte)_random.Next(100, 200),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256))); // создаем случайный цвет для нашего элемента
                border.Height = _random.Next(2, 10); // создаем рандом высоты
                border.Width = _random.Next(10, 400);//создаем рандом ширины

                border.RenderTransform = new RotateTransform(_random.Next(0, 360));


                CanvasNoise.Children.Add(border); //добавляем наш элемент
                Canvas.SetLeft(border, _random.Next(0, 300));
                Canvas.SetTop(border, _random.Next(0, 150)); // добавляем отступы от точки создания





                Ellipse ellipse = new Ellipse(); // создаем кружок
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)_random.Next(100, 200),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256),
                                                                       (byte)_random.Next(0, 256))); // так же как и с Border создаем случайный цвет для элемента (кружка)
                ellipse.Height = ellipse.Width = _random.Next(20, 40); //задаем радиус

                CanvasNoise.Children.Add(ellipse); // добавляем
                Canvas.SetLeft(ellipse, _random.Next(0, 300));
                Canvas.SetTop(ellipse, _random.Next(0, 150));
            }
        }

        private void BtnUpdateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha(); //запускаем обновление капчи
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (text1.Text==_currentCaptcha)
            {

                MessageBox.Show("Успешный вход");
               
            }

            else
            {
                _attempts++;
                if (_attempts >= 3)
                {
                    // Выводим сообщение об ошибке
                    MessageBox.Show("Вы превысили количество попыток.");
                    btn1.IsEnabled = false;
                   
                    btn1.IsEnabled = true;
                }
                else
                {
                    // Сообщаем пользователю о неверном вводе
                    MessageBox.Show("Неверный ввод, попробуйте снова.");
                }
            }
        }

  

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            text2.Text = _currentCaptcha;
        }
    }
}
