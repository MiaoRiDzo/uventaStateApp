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

namespace uventaStateApp.Resorces.Pages.ProviderPages.ProviderPage
{
    /// <summary>
    /// Логика взаимодействия для ProviderTablePage.xaml
    /// </summary>
    public partial class ProviderTablePage : Page
    {
        MainWindow win;
        public ProviderTablePage(MainWindow _win)
        {
            InitializeComponent();
            win = _win;
            dgProvider.ItemsSource = stateUventaEntities.getContext().ServiceProvider.ToList();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.ProviderPages.ProviderPage.ProviderEditPage(win, null));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.ProviderPages.ProviderPage.ProviderEditPage(win, ((sender as Button).DataContext as ServiceProvider)));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            stateUventaEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dgProvider.ItemsSource = stateUventaEntities.getContext().ServiceProvider.ToList();

        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var removes = dgProvider.SelectedItems.Cast<ServiceProvider>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {removes.Count()} элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    stateUventaEntities.getContext().ServiceProvider.RemoveRange(removes);
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgProvider.ItemsSource = stateUventaEntities.getContext().ServiceProvider.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

    }
}

