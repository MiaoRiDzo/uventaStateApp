using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using uventaStateApp.Wins;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uventaStateApp.Resorces.Libs;
using MahApps.Metro.Controls;

namespace uventaStateApp.Wins
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {

        User authUser = new User();
        
        public MainWindow(User user)
        {
            InitializeComponent();
            authUser = user;
            //Навигация в Frame
            mFrame.Navigate(new Resorces.Pages.mainMenu(authUser, this));
            //отображение информации провиля
            tbName.Text = user.UserName;
            tbRole.Text = user.Role.RoleName;
            if(user.ServiceProvider != null)
            {
                tbProvider.Visibility = Visibility.Visible;
                tbProvider.Text = user.ServiceProvider.ServiceProviderName;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mFrame.GoBack();
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Сменить пользователя?", authUser.UserName, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                LoginPage loginWindow = new LoginPage();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
