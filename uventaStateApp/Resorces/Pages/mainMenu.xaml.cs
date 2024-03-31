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
using System.Windows.Navigation;
using System.Windows.Shapes;
using uventaStateApp.Resorces.Libs;
using uventaStateApp.Wins;

namespace uventaStateApp.Resorces.Pages
{
    /// <summary>
    /// Логика взаимодействия для mainMenu.xaml
    /// </summary>
    public partial class mainMenu : Page
    {
        User currentUser = new User();
        MainWindow win;
        public mainMenu(User user, MainWindow _win)
        {
            InitializeComponent();
            currentUser = user;
            tbHead.Text += user.UserName;
            win = _win;
           //initial ui elements
           switch(currentUser.Role.RoleName)
            {
                case "Менеджер":
                    btnUser.Visibility = Visibility.Collapsed;
                    btnProvider.Visibility = Visibility.Collapsed;
                    break;
                case "Администратор":
                    btnMessage.Visibility = Visibility.Collapsed;
                    break;
                case "Управляющий":
                    btnUser.Visibility= Visibility.Collapsed;
                    break;
            }
        }

        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.PremisesPage.PremisesTablePage(win));
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.UserPages.UserPage.UserTablePage(win));
        }

        private void btnProvider_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.ProviderPages.ProviderPage.ProviderTablePage(win));
        }

        private void btnMessage_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.AppealPages.AppealPage.AppealTablePage(win, currentUser));
        }
    }
}
