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

namespace Курсовая_ПИТПМ.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для RedactMoviePage.xaml
    /// </summary>
    public partial class RedactMoviePage : Window
    {
        List<Movie> currentMovies = new List<Movie>();
        Movie clickedMovie = new Movie();
        Movie currentMovie = new Movie();
        public RedactMoviePage()
        {
            InitializeComponent();
            currentMovies = CinemaEntities.GetContext().Movie.ToList();
            MoviesLV.ItemsSource = currentMovies;
        }
        public RedactMoviePage(Movie movie)
        {
            InitializeComponent();
            currentMovie = movie;
            currentMovies = CinemaEntities.GetContext().Movie.ToList();
            foreach (var mov in currentMovies)
            {
                if (mov.Id == currentMovie.Id)
                {
                    mov.MovieName = currentMovie.MovieName;
                    mov.MovieCategory = currentMovie.MovieCategory;
                    mov.MovieCountry = currentMovie.MovieCountry;
                    mov.MovieProducer = currentMovie.MovieProducer;
                    mov.MovieURL = currentMovie.MovieURL;
                    mov.ReleaseDate = currentMovie.ReleaseDate;
                    mov.Descriprion = currentMovie.Descriprion;
                    mov.PosterPreview = currentMovie.PosterPreview;
                    mov.Preview = currentMovie.Preview;
                    mov.PosterId = currentMovie.PosterId;
                }
            }
            MoviesLV.ItemsSource = currentMovies;
        }
        private void AddMoviePageBTN_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage managerPage = new ManagerPage();
            managerPage.Show();
            this.Close();
        }

        private void RedactMoviePageBTN_Click(object sender, RoutedEventArgs e)
        {
            RedactMoviePage redactMoviePage = new RedactMoviePage();
            redactMoviePage.Show();
            this.Close();
        }

        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void RedactMovieBTN_Click(object sender, RoutedEventArgs e)
        {
            var send = (FrameworkElement)sender;
            var mouseItem = send.DataContext;
            var selectedmovie = currentMovies;

            selectedmovie = selectedmovie.Where(p => p.Equals(mouseItem)).ToList();
            using (var db = new CinemaEntities())
            {
                foreach (var item in db.Movie)
                {
                    if (selectedmovie.Any(p => p.MovieName.Contains(item.MovieName)))
                    {
                        clickedMovie = item;
                    }
                }
            }

            ManagerPage managerPage = new ManagerPage(clickedMovie);
            managerPage.Show();
            this.Close();
        }

        private void DeleteMovieBTN_Click(object sender, RoutedEventArgs e)
        {
            var send = (FrameworkElement)sender;
            var mouseItem = send.DataContext;
            var selectedmovie = currentMovies;

            selectedmovie = selectedmovie.Where(p => p.Equals(mouseItem)).ToList();
            using (var db = new CinemaEntities())
            {
                foreach (var item in db.Movie)
                {
                    if (selectedmovie.Any(p => p.MovieName.Contains(item.MovieName)))
                    {
                        clickedMovie = item;
                    }
                }
                if (MessageBox.Show(String.Format("Вы точно хотите удалить фильм {0}?", clickedMovie.MovieName), "Удаление фильма", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clickedMovie = db.Movie.Find(clickedMovie.Id);
                    db.Movie.Remove(clickedMovie);
                    db.SaveChanges();
                    currentMovies = currentMovies.Where(p => !p.Id.Equals(clickedMovie.Id)).ToList();
                    MoviesLV.ItemsSource = currentMovies;
                    MoviesLV.Items.Refresh();
                }
            }
        }
    }
}
