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

namespace Курсовая_ПИТПМ.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Window
    {
        Client currentClient = new Client();
        User currentUser = new User();
        public PersonalPage()
        {
            InitializeComponent();
        }
        public PersonalPage(Client client)
        {
            InitializeComponent();
            currentClient = client;
            WelcomeLabel.Text = String.Format("Личный кабинет {0}", currentClient.User.FullName);
            LoginTB.Text = currentClient.User.Login;
            PasswordTB.Text = currentClient.User.Password;
            EmailTB.Text = currentClient.User.Email;
            NameTB.Text = currentClient.User.FullName;
            PutBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
            TakeBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
        }

        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void PosterPageBTN_Click(object sender, RoutedEventArgs e)
        {
            ClientPage clientPage = new ClientPage(currentClient);
            clientPage.Show();
            this.Close();
        }

        private void PersonalPageBTN_Click(object sender, RoutedEventArgs e)
        {
            PersonalPage personalPage = new PersonalPage();
            personalPage.Show();
            this.Close();
        }

        private void ApplyChangesBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                foreach (var user in db.User)
                {
                    if (user.Id == currentClient.UserID)
                        currentUser = user;
                }
                currentUser.Login = LoginTB.Text;
                currentUser.Password = PasswordTB.Text;
                currentUser.FullName = NameTB.Text;
                currentUser.Email = EmailTB.Text;
                db.SaveChanges();
                MessageBox.Show(string.Format("Данные {0} успешно изменились!", currentUser.Login));
            }
        }

        private void PersonalDataBTN_Click(object sender, RoutedEventArgs e)
        {
            PersonalDataWP.Visibility = Visibility.Visible;
            PutBalanceWP.Visibility = Visibility.Hidden;
            TakeBalanceWP.Visibility = Visibility.Hidden;
        }

        private void PutOnBalanceBTN_Click(object sender, RoutedEventArgs e)
        {
            PutBalanceWP.Visibility = Visibility.Visible;
            PersonalDataWP.Visibility = Visibility.Hidden;
            TakeBalanceWP.Visibility = Visibility.Hidden;
            PutBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
        }

        private void TakeFromBalanceBTN_Click(object sender, RoutedEventArgs e)
        {
            TakeBalanceWP.Visibility = Visibility.Visible;
            PutBalanceWP.Visibility = Visibility.Hidden;
            PersonalDataWP.Visibility = Visibility.Hidden;
            TakeBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
        }
        private double money;
        private void PutBalanceBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                foreach (var client in db.Client)
                {
                    if (currentUser.Id == client.UserID)
                        currentClient = client;
                }
                if (PutBalanceTB.Text != null)
                {
                    money = Convert.ToDouble(PutBalanceTB.Text);
                    currentClient = db.Client.Find(currentClient.Id);
                    if (currentClient.Balance == 0 || currentClient.Balance == null)
                        currentClient.Balance = money;
                    else if (currentClient.Balance > 0)
                        currentClient.Balance += money;
                    db.SaveChanges();
                    MessageBox.Show(String.Format("Баланс {0} успешно пополнен", currentClient.User.Login));
                    PutBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
                }
                else
                    MessageBox.Show("Пожалуйста, введите значение для внесения!");

            }
        }

        private void TakeBalanceBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                foreach (var client in db.Client)
                {
                    if (currentUser.Id == client.UserID)
                        currentClient = client;
                }
                if (TakeBalanceTB.Text != null)
                {
                    money = Convert.ToInt32(TakeBalanceTB.Text);
                    if (money < currentClient.Balance)
                    {
                        currentClient = db.Client.Find(currentClient.Id);
                        currentClient.Balance -= money;
                        db.SaveChanges();
                        MessageBox.Show("Деньги успешно сняты!");
                        TakeBalanceLabel.Text = String.Format("Ваш баланс: {0} руб.", currentClient.Balance);
                    }
                    else
                        MessageBox.Show("На вашем счёте недостаточно средств!");
                }
                else
                    MessageBox.Show("Пожалуйста, введите значение для внесения!");

            }
        }

        private void SubscriprionPageBTN_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionPage subscriptionPage = new SubscriptionPage(currentClient);
            subscriptionPage.Show();
            this.Close();
        }
    }
}
