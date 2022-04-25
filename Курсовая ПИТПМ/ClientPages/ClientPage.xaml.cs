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
using Курсовая_ПИТПМ.ClientPages;

namespace Курсовая_ПИТПМ
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Window
    {
        public int click = 0;
        Client currentClient = new Client();
        Movie currentMovie = new Movie();
        public ClientPage()
        {
            InitializeComponent();
            CategoriesCB.SelectedIndex = 0;
            CountriesCB.SelectedIndex = 0;
            UpdateMovies();
        }
        public ClientPage(Client client)
        {
            InitializeComponent();
            currentClient = client;
            CategoriesCB.SelectedIndex = 0;
            CountriesCB.SelectedIndex = 0;
            UpdateMovies();
        }
        private void UpdateMovies()
        {
            var currentMovies = CinemaEntities.GetContext().Movie.ToList();
            if (click == 0)
            {
                MoviesLV.ItemsSource = currentMovies;
                using (var db = new CinemaEntities())
                {
                    foreach (var movies in db.Movie)
                    {
                        if (!CategoriesCB.Items.Contains(movies.MovieCategory))
                        {
                            CategoriesCB.Items.Add(movies.MovieCategory);
                        }
                        if (!CountriesCB.Items.Contains(movies.MovieCountry))
                        {
                            CountriesCB.Items.Add(movies.MovieCountry);
                        }
                    }
                }
            }
            if (click == 1)
            {
                currentMovies = currentMovies.Where(p => p.PosterId.Equals(1)).ToList();
                using (var db = new CinemaEntities())
                {
                    foreach (var movies in db.Movie)
                    {
                        if (movies.PosterId == 1)
                        {
                            if (!CategoriesCB.Items.Contains(movies.MovieCategory))
                            {
                                CategoriesCB.Items.Add(movies.MovieCategory);
                            }
                            if (!CountriesCB.Items.Contains(movies.MovieCountry))
                            {
                                CountriesCB.Items.Add(movies.MovieCountry);
                            }
                        }
                    }
                }
            }
            if (click == 2)
            {
                currentMovies = currentMovies.Where(p => p.PosterId.Equals(2)).ToList();
                using (var db = new CinemaEntities())
                {
                    foreach (var movies in db.Movie)
                    {
                        if (movies.PosterId == 2)
                        {
                            if (!CategoriesCB.Items.Contains(movies.MovieCategory))
                            {
                                CategoriesCB.Items.Add(movies.MovieCategory);
                            }
                            if (!CountriesCB.Items.Contains(movies.MovieCountry))
                            {
                                CountriesCB.Items.Add(movies.MovieCountry);
                            }
                        }
                    }
                }
            }
            if (click == 3)
            {
                currentMovies = currentMovies.Where(p => p.PosterId.Equals(3)).ToList();
                using (var db = new CinemaEntities())
                {
                    foreach (var movies in db.Movie)
                    {
                        if (movies.PosterId == 3)
                        {
                            if (!CategoriesCB.Items.Contains(movies.MovieCategory))
                            {
                                CategoriesCB.Items.Add(movies.MovieCategory);
                            }
                            if (!CountriesCB.Items.Contains(movies.MovieCountry))
                            {
                                CountriesCB.Items.Add(movies.MovieCountry);
                            }
                        }
                    }
                }
            }
            if (CategoriesCB.SelectedIndex > 0)
                currentMovies = currentMovies.Where(p => p.MovieCategory.Contains(CategoriesCB.SelectedItem.ToString())).ToList(); 
            if (CountriesCB.SelectedIndex > 0)
                currentMovies = currentMovies.Where(p => p.MovieCountry.Contains(CountriesCB.SelectedItem.ToString())).ToList();
            if (click == 4)
                currentMovies = currentMovies.OrderByDescending(p => p.ReleaseDate).ToList();
            if (click == 5)
            {
                //var currentRating = CinemaEntities.GetContext().Rating.ToList();
                //currentRating = currentRating.OrderByDescending(p => p.CountOfStars).ToList();
                //currentMovies = currentMovies.OrderByDescending(p => p.Rating == currentRating).ToList();
            }
            if (click == 6)
            {
                var selectedmovie = currentMovies;
                selectedmovie = selectedmovie.Where(p => p.Equals(MoviesLV.SelectedItem)).ToList();
                using (var db = new CinemaEntities())
                {
                    foreach (var item in db.Movie)
                    {
                        if (selectedmovie.Any(p => p.MovieName.Contains(item.MovieName)))
                        {
                            currentMovie = item;
                        }
                    }
                }
                Window form = new Window();
                using (var bd = new CinemaEntities())
                {
                    Subscription subscription = new Subscription();
                    subscription.ClientId = currentClient.Id;
                    subscription.PosterId = currentMovie.PosterId;
                    subscription.IsBought = false;
                    foreach (var sub in bd.Subscription)
                    {
                        if (currentMovie.PosterId == sub.PosterId)
                        {
                            form = new MoviePage(currentMovie, currentClient);
                            form.Show();
                            this.Close();
                            return;
                        }
                    }
                    bd.Subscription.Add(subscription);
                    bd.SaveChanges();
                    form = new MoviePage(currentMovie, currentClient);
                    form.Show();
                    this.Close();
                }
            }
            MoviesLV.ItemsSource = currentMovies;
        }
        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void PosterPageBTN_Click(object sender, RoutedEventArgs e)
        {
            ClientPage clientPage = new ClientPage();
            clientPage.Show();
            this.Close();
        }

        private void PersonalPageBTN_Click(object sender, RoutedEventArgs e)
        {
            PersonalPage personalPage = new PersonalPage(currentClient);
            personalPage.Show();
            this.Close();
        }

        private void MoviesBTN_Click(object sender, RoutedEventArgs e)
        {
            CountriesCB.Items.Clear();
            CategoriesCB.Items.Clear();
            CategoriesCB.Items.Insert(0, "Все жанры");
            CategoriesCB.SelectedIndex = 0;
            CountriesCB.Items.Insert(0, "Все страны");
            CountriesCB.SelectedIndex = 0;
            click = 1;
            UpdateMovies();
        }

        private void SerialsBTN_Click(object sender, RoutedEventArgs e)
        {
            CountriesCB.Items.Clear();
            CategoriesCB.Items.Clear();
            CategoriesCB.Items.Insert(0, "Все жанры");
            CategoriesCB.SelectedIndex = 0;
            CountriesCB.Items.Insert(0, "Все страны");
            CountriesCB.SelectedIndex = 0;
            click = 2;
            UpdateMovies();
        }

        private void AnimesBTN_Click(object sender, RoutedEventArgs e)
        {
            CountriesCB.Items.Clear();
            CategoriesCB.Items.Clear();
            CategoriesCB.Items.Insert(0, "Все жанры");
            CategoriesCB.SelectedIndex = 0;
            CountriesCB.Items.Insert(0, "Все страны");
            CountriesCB.SelectedIndex = 0;
            click = 3;
            UpdateMovies();
        }

        private void CategoriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMovies();
        }

        private void CountriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMovies();
        }

        private void NewestSortBTN_Click(object sender, RoutedEventArgs e)
        {
            click = 4;
            UpdateMovies();
        }

        private void RatingSortBTN_Click(object sender, RoutedEventArgs e)
        {
            click = 5;
            UpdateMovies();
        }

        private void AllPosterBTN_Click(object sender, RoutedEventArgs e)
        {
            click = 0;
            UpdateMovies();
        }

        private void MoviesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            click = 6;
            UpdateMovies();
        }

        private void SubscriprionPageBTN_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionPage subscriptionPage = new SubscriptionPage(currentClient);
            subscriptionPage.Show();
            this.Close();
        }
    }
}
