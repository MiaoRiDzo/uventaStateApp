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

namespace uventaStateApp.Resorces.Pages.PremisesPage.BuildPage
{
    /// <summary>
    /// Логика взаимодействия для BuildTablePage.xaml
    /// </summary>
    public partial class BuildTablePage : Page
    {
        MainWindow win;
        public BuildTablePage(MainWindow _win)
        {
            InitializeComponent();
            win = _win;
            dgBuild.ItemsSource = stateUventaEntities.getContext().Build.ToList();
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var removes = dgBuild.SelectedItems.Cast<Build>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {removes.Count()} элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    stateUventaEntities.getContext().Build.RemoveRange(removes);
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgBuild.ItemsSource = stateUventaEntities.getContext().Build.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.BuildPage.BuildEditPage(win, null));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.BuildPage.BuildEditPage(win, (sender as Button).DataContext as Build));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            stateUventaEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dgBuild.ItemsSource = stateUventaEntities.getContext().Build.ToList();
        }
    }
}
