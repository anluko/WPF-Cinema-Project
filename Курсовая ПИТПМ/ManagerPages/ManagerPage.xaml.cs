using Microsoft.Win32;
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
using System.Windows.Shapes;
using Курсовая_ПИТПМ.ManagerPages;

namespace Курсовая_ПИТПМ
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        Movie currentMovie = new Movie();
        int action = 0;
        public ManagerPage()
        {
            InitializeComponent();
            WelcomeLabel.Text = "Добавление нового фильма";
            AddMovieBTN.Content = "Добавить";
            action = 1;
            MoviePosterIMG.Source = new BitmapImage(new Uri(@"C:\Users\anluko\source\repos\Курсовая ПИТПМ\Курсовая ПИТПМ\Resources\picture.png"));
            MoviePreviewIMG.Source = new BitmapImage(new Uri(@"C:\Users\anluko\source\repos\Курсовая ПИТПМ\Курсовая ПИТПМ\Resources\picture.png"));
            using (var db = new CinemaEntities())
            {
                foreach (var poster in db.Poster)
                {
                    PosterCB.Items.Add(poster.Category);
                }
            }
        }
        public ManagerPage(Movie movie)
        {
            InitializeComponent();
            currentMovie = movie;
            WelcomeLabel.Text = String.Format("Изменение фильма {0}", currentMovie.MovieName);
            action = 2;
            AddMovieBTN.Content = "Изменить";
            using (var db = new CinemaEntities())
            {
                foreach (var poster in db.Poster)
                {
                    PosterCB.Items.Add(poster.Category);
                }
            }
            PosterCB.SelectedIndex = currentMovie.PosterId - 1;
            NameTB.Text = currentMovie.MovieName;
            CategoryTB.Text = currentMovie.MovieCategory;
            CountryTB.Text = currentMovie.MovieCountry;
            ProducerTB.Text = currentMovie.MovieProducer;
            UrlTB.Text = currentMovie.MovieURL;
            DateTB.Text = currentMovie.ReleaseDate.ToString();
            DescriptionTB.Text = currentMovie.Descriprion;
            MoviePosterIMG.Source = LoadImage(currentMovie.PosterPreview);
            MoviePreviewIMG.Source = LoadImage(currentMovie.Preview);
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
        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
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

        private void LoadPosterImgBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = ""; 
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";  
            bool? result = dlg.ShowDialog();
            if (result == true)
                MoviePosterIMG.Source = new BitmapImage(new Uri(dlg.FileName));
        }

        private void LoadPreviewImgBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            bool? result = dlg.ShowDialog();
            if (result == true)
                MoviePreviewIMG.Source = new BitmapImage(new Uri(dlg.FileName));
        }

        private void AddMovieBTN_Click(object sender, RoutedEventArgs e)
        {
            if (action == 1)
            {
                using (var db = new CinemaEntities())
                {
                    if (NameTB.Text != "" && PosterCB.SelectedIndex != -1)
                    {
                        foreach (var movie in db.Movie)
                        {
                            if (NameTB.Text == movie.MovieName && CategoryTB.Text == movie.MovieCategory && CountryTB.Text == movie.MovieCountry)
                            {
                                MessageBox.Show("Такой фильм уже существует. Пожалуйста заугрзите новый!");
                                return;
                            }
                        }
                        Movie newMovie = new Movie();
                        newMovie.MovieName = NameTB.Text;
                        newMovie.MovieCategory = CategoryTB.Text;
                        newMovie.MovieCountry = CountryTB.Text;
                        newMovie.MovieProducer = ProducerTB.Text;
                        newMovie.MovieURL = UrlTB.Text;
                        newMovie.ReleaseDate = Int32.Parse(DateTB.Text);
                        newMovie.Descriprion = DescriptionTB.Text;
                        newMovie.PosterPreview = ImageToByte(MoviePosterIMG.Source as BitmapImage);
                        newMovie.Preview = ImageToByte(MoviePreviewIMG.Source as BitmapImage);
                        if (PosterCB.SelectedIndex == 0)
                            newMovie.PosterId = 1;
                        if (PosterCB.SelectedIndex == 1)
                            newMovie.PosterId = 2;
                        if (PosterCB.SelectedIndex == 2)
                            newMovie.PosterId = 3;
                        db.Movie.Add(newMovie);
                        db.SaveChanges();
                        MessageBox.Show(String.Format("Фильм {0} успешно добавлен!", newMovie.MovieName));
                    }
                    else
                        MessageBox.Show("Пожалуйста заполните поля!");
                }
            }
            if (action == 2)
            {
                using (var db = new CinemaEntities())
                {
                    foreach (var movie in db.Movie)
                    {
                        if (movie.Id == currentMovie.Id)
                            currentMovie = movie;
                    }
                    currentMovie = db.Movie.Find(currentMovie.Id);
                    currentMovie.MovieName = NameTB.Text;
                    currentMovie.MovieCategory = CategoryTB.Text;
                    currentMovie.MovieCountry = CountryTB.Text;
                    currentMovie.MovieProducer = ProducerTB.Text;
                    currentMovie.MovieURL = UrlTB.Text;
                    currentMovie.ReleaseDate = Int32.Parse(DateTB.Text);
                    currentMovie.Descriprion = DescriptionTB.Text;
                    currentMovie.PosterPreview = ImageToByte(MoviePosterIMG.Source as BitmapImage);
                    currentMovie.Preview = ImageToByte(MoviePreviewIMG.Source as BitmapImage);
                    if (PosterCB.SelectedIndex == 0)
                        currentMovie.PosterId = 1;
                    if (PosterCB.SelectedIndex == 1)
                        currentMovie.PosterId = 2;
                    if (PosterCB.SelectedIndex == 2)
                        currentMovie.PosterId = 3;
                    db.SaveChanges();
                    MessageBox.Show(String.Format("Фильм {0} успешно изменён!", currentMovie.MovieName));
                    RedactMoviePage redactMoviePage = new RedactMoviePage(currentMovie);
                    redactMoviePage.Show();
                    this.Close();
                }
            }
        }
        public byte[] ImageToByte(BitmapImage image)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
    }
}
