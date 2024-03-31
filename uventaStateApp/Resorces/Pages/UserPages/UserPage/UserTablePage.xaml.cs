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

namespace uventaStateApp.Resorces.Pages.UserPages.UserPage
{
    /// <summary>
    /// Логика взаимодействия для UserTablePage.xaml
    /// </summary>
    public partial class UserTablePage : Page
    {
        MainWindow win;
        public UserTablePage(MainWindow _win)
        {
            InitializeComponent();
            win = _win;
            dgUsers.ItemsSource = stateUventaEntities.getContext().User.ToList();
        }

        private void btn_role_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.UserPages.UserPage.UserEditPage(win, null));
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var removes = dgUsers.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {removes.Count()} элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    stateUventaEntities.getContext().User.RemoveRange(removes);
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgUsers.ItemsSource = stateUventaEntities.getContext().User.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.UserPages.UserPage.UserEditPage(win,(sender as Button).DataContext as User));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            stateUventaEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dgUsers.ItemsSource = stateUventaEntities.getContext().User.ToList();

        }
    }
}
