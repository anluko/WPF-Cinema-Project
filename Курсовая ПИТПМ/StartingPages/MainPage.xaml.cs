using System;
using System.Collections.Generic;
using System.IO;
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

namespace Курсовая_ПИТПМ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            //ImportLogo()
        }
        //public void ImportLogo()
        //{
        //    var photos = Directory.GetFiles(@"C:\Users\anluko\Desktop\videos");
        //    using (var db = new CinemaEntities())
        //    {
        //        foreach (var movies in db.Movie)
        //        {
        //            try
        //            {
        //                movies.Video = File.ReadAllBytes(photos.FirstOrDefault(p => p.Contains(movies.MovieName)));
        //            }
        //            catch
        //            {
        //                movies.Video = null;
        //            }
        //        }
        //        db.SaveChanges();
        //    }
        //}
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

        private void OutBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
