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
    /// Логика взаимодействия для SubscriptionPage.xaml
    /// </summary>
    public partial class SubscriptionPage : Window
    {
        Client currentClient = new Client();
        public SubscriptionPage()
        {
            InitializeComponent();
        }
        public SubscriptionPage(Client client)
        {
            InitializeComponent();
            currentClient = client;
            MonthPriceTB.Visibility = Visibility.Hidden;
            YearPriceTB.Visibility = Visibility.Hidden;
            MonthSubBTN.Visibility = Visibility.Hidden;
            YearSubBTN.Visibility = Visibility.Hidden;
            using (var db = new CinemaEntities())
            {
                foreach (var item in db.Poster)
                {
                    CategoriesCB.Items.Add(item.Category);
                }
            }
        }

        private void PosterPageBTN_Click(object sender, RoutedEventArgs e)
        {
            ClientPage clientPage = new ClientPage(currentClient);
            clientPage.Show();
            this.Close();
        }

        private void PersonalPageBTN_Click(object sender, RoutedEventArgs e)
        {
            PersonalPage personalPage = new PersonalPage(currentClient);
            personalPage.Show();
            this.Close();
        }

        private void SubscriprionPageBTN_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionPage subscriptionPage = new SubscriptionPage();
            subscriptionPage.Show();
            this.Close();
        }

        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void CategoriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MonthPriceTB.Visibility = Visibility.Visible;
            YearPriceTB.Visibility = Visibility.Visible;
            MonthSubBTN.Visibility = Visibility.Visible;
            YearSubBTN.Visibility = Visibility.Visible;
            if (CategoriesCB.SelectedIndex == 0){
                MonthPriceTB.Text = " Подписка на месяц: 199 руб.";
                YearPriceTB.Text = "Подписка на год: 2500 руб.";
            }
            if (CategoriesCB.SelectedIndex == 1){
                MonthPriceTB.Text = " Подписка на месяц: 149 руб.";
                YearPriceTB.Text = "Подписка на год: 2000 руб.";
            }
            if (CategoriesCB.SelectedIndex == 2){
                MonthPriceTB.Text = " Подписка на месяц: 119 руб.";
                YearPriceTB.Text = "Подписка на год: 1500 руб.";
            }
        }

        private void MonthSubBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var bd = new CinemaEntities())
            {
                Subscription subscription = new Subscription();
                foreach (var sub in bd.Subscription)
                {
                    if (currentClient.Id == sub.ClientId && sub.PosterId == (CategoriesCB.SelectedIndex + 1))
                        subscription = sub;
                }
                subscription = bd.Subscription.Find(subscription.Id);
                subscription.IsBought = true;
                if (CategoriesCB.SelectedIndex == 0)
                    subscription.Price = 199;
                if (CategoriesCB.SelectedIndex == 1)
                    subscription.Price = 149;
                if (CategoriesCB.SelectedIndex == 2)
                    subscription.Price = 119;
                bd.SaveChanges();
                MessageBox.Show("Месячная подписка успешно оформлена!");
            }
        }

        private void YearSubBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var bd = new CinemaEntities())
            {
                Subscription subscription = new Subscription();
                foreach (var sub in bd.Subscription)
                {
                    if (currentClient.Id == sub.ClientId && sub.PosterId == (CategoriesCB.SelectedIndex + 1))
                        subscription = sub;
                }
                subscription = bd.Subscription.Find(subscription.Id);
                subscription.IsBought = true;
                if (CategoriesCB.SelectedIndex == 0)
                    subscription.Price = 2500;
                if (CategoriesCB.SelectedIndex == 1)
                    subscription.Price = 2000;
                if (CategoriesCB.SelectedIndex == 2)
                    subscription.Price = 1500;
                bd.SaveChanges();
                MessageBox.Show("Подписка на год успешно оформлена!");
            }
        }
    }
}
