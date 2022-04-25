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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        User selectedUser = new User();
        public AdminPage()
        {
            InitializeComponent();
            using (var db = new CinemaEntities())
            {
                foreach (var user in db.User)
                {
                    UsersLV.Items.Add(user.Login);
                }
            }
        }

        private void MainPageBTN_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void BackPageBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void UsersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                foreach (var item in db.User)
                {
                    if (UsersLV.SelectedItem.ToString() == item.Login)
                    {
                        selectedUser = item;
                        LoginTB.Text = item.Login;
                        PasswordTB.Text = item.Password;
                        FullNameTB.Text = item.FullName;
                        EmailTB.Text = item.Email;
                        if (item.RoleId == 1)
                            RoleTB.Text = "Клиент";
                        if (item.RoleId == 2)
                            RoleTB.Text = "Менеджер";
                        if (item.RoleId == 3)
                            RoleTB.Text = "Администратор";
                    }
                }
            }
        }

        private void ApplyBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CinemaEntities())
            {
                foreach (var user in db.User)
                {
                    if (selectedUser.Id == user.Id)
                        selectedUser = user;
                }
                selectedUser = db.User.Find(selectedUser.Id);
                selectedUser.Login = LoginTB.Text;
                selectedUser.Password = PasswordTB.Text;
                selectedUser.FullName = FullNameTB.Text;
                if (RoleTB.Text == "Клиент")
                    selectedUser.RoleId = 1;
                if (RoleTB.Text == "Менеджер")
                    selectedUser.RoleId = 2;
                if (RoleTB.Text == "Администратор")
                    selectedUser.RoleId = 3;
                db.SaveChanges();
                MessageBox.Show(String.Format("Данные пользователя {0} успешно изменены", selectedUser.FullName));
            }
        }
    }
}
