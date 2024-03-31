using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using uventaStateApp.Resorces.Libs;
using uventaStateApp.Wins;

namespace uventaStateApp.Resorces.Pages.AppealPages.AppealPage
{
    /// <summary>
    /// Логика взаимодействия для AppealTablePage.xaml
    /// </summary>
    public partial class AppealTablePage : Page
    {
        private readonly MainWindow win;
        private readonly User user;
        private readonly List<Appeal> appeals;
        private readonly List<Appeal> filterAppeals;

        public AppealTablePage(MainWindow _win, User _user)
        {
            InitializeComponent();
            win = _win;
            user = _user;
            appeals = stateUventaEntities.getContext().Appeal.ToList();
            filterAppeals = FilterAppeals();
            dgAppeal.ItemsSource = filterAppeals;
            if(user.Role.RoleName == "Координатор")
            {
                btn_add.Visibility = Visibility.Visible;
                btn_del.Visibility = Visibility.Visible;
            }
        }

        private List<Appeal> FilterAppeals()
        {
            if (user.ServiceProvider != null)
            {
                return appeals.Where(appeal => appeal.ServiceProvider == user.ServiceProvider).ToList();
            }
            return appeals;
        }

        private void btnMore_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnMore = sender as Button;
            if (btnMore != null)
            {
                // Установить видимость кнопки в зависимости от роли пользователя
                if (user.Role.RoleName != "Менеджер")
                {
                    btnMore.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnMore.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnEdit_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnMore = sender as Button;
            if (btnMore != null)
            {
                // Установить видимость кнопки в зависимости от роли пользователя
                if (user.Role.RoleName != "Координатор")
                {
                    btnMore.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnMore.Visibility = Visibility.Visible;
                }
            }
        }
        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            AppealAbout appealAbout = new AppealAbout(win, (sender as Button)?.DataContext as Appeal);
            appealAbout.Show();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.AppealPages.AppealPage.AppealEditPage(win, null));
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var removes = dgAppeal.SelectedItems.Cast<Appeal>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {removes.Count()} элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    stateUventaEntities.getContext().Appeal.RemoveRange(removes);
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgAppeal.ItemsSource = stateUventaEntities.getContext().Appeal.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            win.mFrame.Navigate(new Pages.AppealPages.AppealPage.AppealEditPage(win, (sender as Button).DataContext as Appeal));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            stateUventaEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dgAppeal.ItemsSource = stateUventaEntities.getContext().Appeal.ToList();
        }
    }
}
