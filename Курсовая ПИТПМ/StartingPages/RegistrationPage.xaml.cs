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
using System.Windows.Shapes;

namespace Курсовая_ПИТПМ
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void AutorizationBTN_Click(object sender, RoutedEventArgs e)
        {
            AutorizationPage autorizationPage = new AutorizationPage();
            autorizationPage.Show();
            this.Close();
        }

        private void REgistrationBTN_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();
            this.Close();
        }

        private void MainPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void RegistrationBTN_Click_1(object sender, RoutedEventArgs e)
        {
            using (var bd = new CinemaEntities())
            {
                foreach (User usr in bd.User)
                {
                    if (usr.Login == LoginTB.Text || usr.Password == PasswordTB.Text)
                    {
                        MessageBox.Show("Такой пользователь уже существует!");
                        return;
                    }
                }

                if (LoginTB.Text == "" || PasswordTB.Text == "" || EmailTB.Text == "")
                    MessageBox.Show("Заполните все поля для регистрации!");
                else if (LoginTB.Text == "admin")
                    MessageBox.Show("Недопустимое имя пользователя!");

                else
                {
                    User user = new User();
                    Client client = new Client();
                    user.Login = LoginTB.Text;
                    user.Password = PasswordTB.Text;
                    user.Email = EmailTB.Text;
                    user.FullName = NameTB.Text;
                    client.UserID = user.Id;
                    foreach (var role in bd.Role)
                    {
                        if (role.Id == 1)
                            user.RoleId = role.Id;
                    }
                    bd.User.Add(user);
                    bd.Client.Add(client);
                    bd.SaveChanges();
                    MessageBox.Show(String.Format("Регистрация пользователя '{0}' прошла успешно.", user.Login));
                    AutorizationPage autorizationPage = new AutorizationPage();
                    autorizationPage.Show();
                    this.Hide();
                }
            }
        }
    }
}
