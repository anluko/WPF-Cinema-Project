using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Курсовая_ПИТПМ.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Window
    {
        Movie currentMovie = new Movie();
        Client currentClient = new Client();
        int starClick = 0;
        public MoviePage()
        {
            InitializeComponent();
        }
        public MoviePage(Movie movie, Client client)
        {
            InitializeComponent();
            currentMovie = movie;
            currentClient = client;

            using (var db = new CinemaEntities())
            {
                foreach (var sub in db.Subscription)
                {
                    if (currentClient.Id == sub.ClientId)
                    {
                        if (sub.IsBought == true)
                        {
                            StartWatchBTN.Visibility = Visibility.Visible;
                            BuyTicketBTN.Visibility = Visibility.Hidden;
                        }
                        if (sub.IsBought == false)
                        {
                            StartWatchBTN.Visibility = Visibility.Hidden;
                            BuyTicketBTN.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

            FirstRate.Background = setNullImage();
            SecondRate.Background = setNullImage();
            ThirdRate.Background = setNullImage();
            FourthRate.Background = setNullImage();
            FifthRate.Background = setNullImage();
            SixthRate.Background = setNullImage();
            SeventhRate.Background = setNullImage();
            EighthRate.Background = setNullImage();
            NinethRate.Background = setNullImage();
            TenthRate.Background = setNullImage();

            MovieNameTB.Text = currentMovie.MovieName;
            MovieImage.Source = LoadImage(currentMovie.Preview);
            CategoryTB.Text = currentMovie.MovieCategory;
            ProducerTB.Text = currentMovie.MovieProducer;
            CountryTB.Text = currentMovie.MovieCountry;
            DateTB.Text = Convert.ToString(currentMovie.ReleaseDate);
            DescriptionTB.Text = currentMovie.Descriprion;
            using (var db = new CinemaEntities())
            {
                Ticket ticket = new Ticket();
                ticket.MovieId = currentMovie.Id;
                ticket.IsBought = false;
                if (currentMovie.PosterId == 1){
                    PriceTB.Text = "699 Р";
                    ticket.Price = 699;
                }
                if (currentMovie.PosterId == 2) { 
                    PriceTB.Text = "599 Р";
                    ticket.Price = 599;
                }
                if (currentMovie.PosterId == 3){
                    PriceTB.Text = "499 Р";
                    ticket.Price = 499;
                }
                foreach (var tick in db.Ticket)
                {
                    if (tick.MovieId == ticket.MovieId)
                        return;
                }
                db.Ticket.Add(ticket);
                db.SaveChanges();
            }
        }
        private static ImageBrush setImage()
        {
            var image = new BitmapImage(new Uri(@"C:\Users\anluko\source\repos\Курсовая ПИТПМ\Курсовая ПИТПМ\Resources\OrangeStar.png"));
            var background = new ImageBrush(image);
            return background;
        }
        private static ImageBrush setNullImage()
        {
            var image = new BitmapImage(new Uri(@"C:\Users\anluko\source\repos\Курсовая ПИТПМ\Курсовая ПИТПМ\Resources\star.png"));
            var background = new ImageBrush(image);
            return background;
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
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

        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
        private void UpdateStars()
        {
            if (starClick == 1)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setNullImage();
                ThirdRate.Background = setNullImage();
                FourthRate.Background = setNullImage();
                FifthRate.Background = setNullImage();
                SixthRate.Background = setNullImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 2)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setNullImage();
                FourthRate.Background = setNullImage();
                FifthRate.Background = setNullImage();
                SixthRate.Background = setNullImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 3)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setNullImage();
                FifthRate.Background = setNullImage();
                SixthRate.Background = setNullImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 4)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setNullImage();
                SixthRate.Background = setNullImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 5)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setNullImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 6)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setImage();
                SeventhRate.Background = setNullImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 7)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setImage();
                SeventhRate.Background = setImage();
                EighthRate.Background = setNullImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 8)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setImage();
                SeventhRate.Background = setImage();
                EighthRate.Background = setImage();
                NinethRate.Background = setNullImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 9)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setImage();
                SeventhRate.Background = setImage();
                EighthRate.Background = setImage();
                NinethRate.Background = setImage();
                TenthRate.Background = setNullImage();
            }
            if (starClick == 10)
            {
                FirstRate.Background = setImage();
                SecondRate.Background = setImage();
                ThirdRate.Background = setImage();
                FourthRate.Background = setImage();
                FifthRate.Background = setImage();
                SixthRate.Background = setImage();
                SeventhRate.Background = setImage();
                EighthRate.Background = setImage();
                NinethRate.Background = setImage();
                TenthRate.Background = setImage();
            }
        }
        private void FirstRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 1;
            UpdateStars();
        }

        private void SecondRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 2;
            UpdateStars();
        }

        private void ThirdRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 3;
            UpdateStars();
        }

        private void FourthRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 4;
            UpdateStars();
        }

        private void FifthRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 5;
            UpdateStars();
        }

        private void SixthRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 6;
            UpdateStars();
        }

        private void SeventhRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 7;
            UpdateStars();
        }

        private void EighthRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 8;
            UpdateStars();
        }

        private void NinethRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 9;
            UpdateStars();
        }

        private void TenthRate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            starClick = 10;
            UpdateStars();
        }

        private void RateBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                Rating rating = new Rating();
                rating.MovieId = currentMovie.Id;
                rating.ClientId = currentClient.Id;
                if (starClick == 1)
                    rating.CountOfStars = 1;
                if (starClick == 2)
                    rating.CountOfStars = 2;
                if (starClick == 3)
                    rating.CountOfStars = 3;
                if (starClick == 4)
                    rating.CountOfStars = 4;
                if (starClick == 5)
                    rating.CountOfStars = 5;
                if (starClick == 6)
                    rating.CountOfStars = 6;
                if (starClick == 7)
                    rating.CountOfStars = 7;
                if (starClick == 8)
                    rating.CountOfStars = 8;
                if (starClick == 9)
                    rating.CountOfStars = 9;
                if (starClick == 10)
                    rating.CountOfStars = 10;
                db.Rating.Add(rating);
                db.SaveChanges();
                MessageBox.Show(String.Format("Вы оценили фильм на {0} баллов!", Convert.ToString(starClick)));
            }
        }

        private void ReviewTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ReviewTB.Text != "")
                {
                    using (var db = new CinemaEntities())
                    {
                        var currentRating = new Rating();
                        foreach (var rating in db.Rating)
                        {
                            if (rating.ClientId == currentClient.Id)
                                currentRating = rating;
                        }
                        currentRating = db.Rating.Find(currentRating.Id);
                        currentRating.Review = ReviewTB.Text;
                        db.SaveChanges();
                        MessageBox.Show("Спасибо за ваш отзыв!");
                    }
                }
                else
                    MessageBox.Show("Пожалуйста проверьте, чтобы поле не было пустым!");
            }
        }

        private void StartWatchBTN_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(currentMovie.MovieURL);
        }

        private void BuyTicketBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                Ticket curTicket = new Ticket();
                foreach (var tick in db.Ticket)
                {
                    if (tick.MovieId == currentMovie.Id)
                        curTicket = tick;
                }
                if (currentClient.Balance > curTicket.Price)
                {
                    curTicket = db.Ticket.Find(curTicket.Id);
                    curTicket.IsBought = true;
                    currentClient = db.Client.Find(currentClient.Id);
                    currentClient.Balance -= curTicket.Price;
                    db.SaveChanges();
                    MessageBox.Show("Билет успешно куплен. Хорошего просмотра!");
                    Process.Start(currentMovie.MovieURL);
                }
                else
                    MessageBox.Show("На вашем счету недостаточно средст для покупки билета!");
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
