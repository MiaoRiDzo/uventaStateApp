using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для ProviderEditPage.xaml
    /// </summary>
    public partial class ProviderEditPage : Page
    {
        MainWindow win;
        ServiceProvider _current = new ServiceProvider();
        bool newIns = true;
        public ProviderEditPage(MainWindow _win, ServiceProvider current)
        {
            InitializeComponent();
            win = _win;

            if (current != null)
            {
                _current = current;
                newIns = false;
                tbHeader.Text = "Редактирование";
            }
            DataContext = _current;
        }
        private static int getNextId()
        {
            try
            {
                int id = stateUventaEntities.getContext().ServiceProvider.ToList().Last().ID_ServiceProvider;
                return id + 1;
            }
            catch { return 0; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в teextBox
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(tbName.Text)) { error.AppendLine("Введите наименование!"); }
            if (string.IsNullOrEmpty(tbPhone.Text)) { error.AppendLine("Введите номер телефона!"); }
            if (string.IsNullOrEmpty(tbINN.Text)) { error.AppendLine("Введите ИНН!"); }
            if (string.IsNullOrEmpty(tbEmail.Text)) { error.AppendLine("Введите электронную почту!"); }

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_ServiceProvider >= 0)
            {
                if (newIns)
                {
                    _current.ID_ServiceProvider = getNextId();
                }

                stateUventaEntities.getContext().ServiceProvider.AddOrUpdate(_current);
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

