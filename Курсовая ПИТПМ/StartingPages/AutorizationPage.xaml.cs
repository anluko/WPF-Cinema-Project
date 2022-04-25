using System.Windows;


namespace Курсовая_ПИТПМ
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Window
    {
        public AutorizationPage()
        {
            InitializeComponent();
            LoginTB.Text = "user";
            PasswordTB.Text = "user123";
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

        private void AutorizBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                Window form;
                User loggedUser = null;
                foreach (User user in db.User)
                {
                    if (LoginTB.Text == user.Login && PasswordTB.Text == user.Password)
                    {
                        loggedUser = user;
                        break;
                    }
                }

                if (loggedUser == null)
                {
                    MessageBox.Show("Логин или пароль указан неверно!");
                    return;
                }

                Client currentClient = null;
                foreach (var client in db.Client)
                {
                    if (loggedUser.Id == client.UserID)
                        currentClient = client;
                }

                switch (loggedUser.RoleId)
                {
                    case 1:
                        form = new ClientPage(currentClient);
                        break;
                    case 2:
                        Manager manager = new Manager();
                        manager.UserId = loggedUser.Id;
                        foreach (var manag in db.Manager)
                        {
                            if (loggedUser.Id == manag.UserId)
                            {
                                form = new ManagerPage();
                                break;
                            }
                            else
                                db.Manager.Add(manager);
                        }
                        db.SaveChanges();
                        form = new ManagerPage();
                        break;
                    case 3:
                        Administrator administrator = new Administrator();
                        administrator.UserID = loggedUser.Id;
                        foreach (var admin in db.Administrator)
                        {
                            if (loggedUser.Id == admin.UserID)
                            {
                                form = new ManagerPage();
                                break;
                            }
                            else
                                db.Administrator.Add(administrator);
                        }
                        db.SaveChanges();
                        form = new AdminPage();
                        break;
                    default:
                        MessageBox.Show(string.Format("Ошибка: неизвестная роль пользователя! ({0})", loggedUser.Role));
                        return;
                }

                form.Show();
                this.Close();
                return;
            }
        }
    }
}
