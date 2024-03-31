using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using uventaStateApp.Resorces.Libs;
using uventaStateApp.Wins;

namespace uventaStateApp.Resorces.Pages.UserPages.UserPage
{
    /// <summary>
    /// Логика взаимодействия для UserEditPage.xaml
    /// </summary>
    public partial class UserEditPage : Page
    {
        MainWindow win;
        User _current = new User();
        Boolean newIns = true;
        public UserEditPage(MainWindow _win, User current)
        {
            InitializeComponent();
            win = _win;
            if (current != null)
            {
                _current = current;
                newIns = false;
                tbHeader.Text = "Редактирование";
            }
            
            cbProvider.ItemsSource = stateUventaEntities.getContext().ServiceProvider.ToList();
            cbRole.ItemsSource = stateUventaEntities.getContext().Role.ToList();
            DataContext = _current;
            _current.Password = "";
        }

        private static int getNextId()
        {
            try
            {
                int id = stateUventaEntities.getContext().User.ToList().Last().ID_User;
                return id + 1;
            }
            catch { return 0; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в teextBox
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(tbUserName.Text)) { error.AppendLine("Введите имя пользователя!"); }
            if (string.IsNullOrEmpty(tbLogin.Text)) { error.AppendLine("Введите логин!"); }
            if (string.IsNullOrEmpty(tbPassword.Text)) { error.AppendLine("Введите пароль!"); }
            if (cbRole.SelectedItem == null) { error.AppendLine("Выберите роль!"); }

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_User >= 0)
            {
                _current.Password = md5.md5Hash(tbPassword.Text);
                if (newIns)
                {
                    _current.ID_User = getNextId();
                    if (cbProvider.SelectedItem != null)
                    {
                        ServiceProvider provider = cbProvider.SelectedItem as ServiceProvider;
                        _current.ID_ServiceProvider = provider.ID_ServiceProvider;
                    }
                    Role role = cbRole.SelectedItem as Role;
                    _current.ID_Role = role.ID_Role;
                }

                stateUventaEntities.getContext().User.AddOrUpdate(_current);
                try
                {
                    stateUventaEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные сохранены.");
                    win.mFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка\n" + ex.Message);
                }
            }
        }
    }
}
