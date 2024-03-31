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
using uventaStateApp.Resorces.Libs;

namespace uventaStateApp.Wins
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public List<User> users;
        public LoginPage()
        {
            InitializeComponent();
            users = stateUventaEntities.getContext().User.ToList();
        }

        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            User currnetUser = new User();

            foreach(User user in users)
            {
                if(user.Login == tbLogin.Text)
                {
                    currnetUser = user;
                    if(currnetUser.Password == md5.md5Hash(tbPass.Text))
                    {
                        MainWindow win = new MainWindow(currnetUser);
                        win.Show();
                        this.Close();
                        break;
                    }
                    else{ MessageBox.Show("Ошибка пароля"); }
                }
            }
            if(currnetUser.Login == null) { MessageBox.Show("Неверный логин"); }
        }
    }
}
