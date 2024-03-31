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

namespace uventaStateApp.Resorces.Pages.PremisesPage.PremisesPage
{
    /// <summary>
    /// Логика взаимодействия для PremisesTablePage.xaml
    /// </summary>
    public partial class PremisesTablePage : Page
    {
        MainWindow win;
        public PremisesTablePage(MainWindow _win)
        {
            InitializeComponent();
            win = _win;
            dgPremises.ItemsSource = stateUventaEntities.getContext().Premises.ToList();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.PremisesPage.PremisesEditPage(win, null));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.PremisesPage.PremisesEditPage(win, ((sender as Button).DataContext as Premises)));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          
                stateUventaEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dgPremises.ItemsSource = stateUventaEntities.getContext().Premises.ToList();
            
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var removes = dgPremises.SelectedItems.Cast<Premises>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {removes.Count()} элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    stateUventaEntities.getContext().Premises.RemoveRange(removes);
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgPremises.ItemsSource = stateUventaEntities.getContext().Premises.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void btn_builds_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.BuildPage.BuildTablePage(win));

        }

        private void btn_state_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.PremisesPage.StatePage.StateTablePage(win));
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSearch.Text))
            {
                // Предположим, что у вас есть DataGridView с именем dgUsers
                List<Premises> list = dgPremises.ItemsSource as List<Premises>; // Получаем List<User> из DataSource
                if (list != null) // Проверяем, что он не null
                {
                    string filter = tbSearch.Text; // Получаем текст из TextBox
                    List<Premises> filteredList = list.FindAll(premises => premises.PremisesName.Contains(filter)); // Создаем отфильтрованный список по лямбда-выражению
                    dgPremises.ItemsSource = filteredList; // Привязываем отфильтрованный список к DataGridView
                }
            }
            else
            {
                dgPremises.ItemsSource = stateUventaEntities.getContext().Premises.ToList();
            }
        }
    }
}
