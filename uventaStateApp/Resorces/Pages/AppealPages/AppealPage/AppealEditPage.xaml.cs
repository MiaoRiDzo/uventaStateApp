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
using System.Xml.Linq;
using uventaStateApp.Resorces.Libs;
using uventaStateApp.Wins;

namespace uventaStateApp.Resorces.Pages.AppealPages.AppealPage
{
    /// <summary>
    /// Логика взаимодействия для AppealEditPage.xaml
    /// </summary>
    public partial class AppealEditPage : Page
    {
        MainWindow win;
        Appeal _current = new Appeal();
        Boolean newIns = true;
        public AppealEditPage(MainWindow _win, Appeal current)
        {
            InitializeComponent();
            win = _win;
            if (current != null)
            {
                _current = current;
                newIns = false;
                tbHeader.Text = "Редактирование";
            }
            cbBuild.ItemsSource = stateUventaEntities.getContext().Premises.ToList();
            cbProvider.ItemsSource = stateUventaEntities.getContext().ServiceProvider.ToList();
            DataContext = _current;
        }

        private static int getNextId()
        {
            try
            {
                int id = stateUventaEntities.getContext().Appeal.ToList().Last().ID_Appeal;
                return id + 1;
            }
            catch { return 0; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в teextBox
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(tbMessage.Text)) { error.AppendLine("Введите сообщение!"); }
            if (cbBuild.SelectedItem == null) { error.AppendLine("Выберите здание!"); }
            if (cbProvider.SelectedItem == null) { error.AppendLine("Выберите поставщика"); }

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_Appeal >= 0)
            {
                if (newIns)
                {
                    _current.ID_Appeal = getNextId();
                    Premises premises= cbBuild.SelectedItem as Premises;
                    _current.ID_Premises = premises.ID_Premises;
                    ServiceProvider service= cbProvider.SelectedItem as ServiceProvider;
                    _current.ID_ServiceProvider= service.ID_ServiceProvider;
                }

                stateUventaEntities.getContext().Appeal.AddOrUpdate(_current);
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
