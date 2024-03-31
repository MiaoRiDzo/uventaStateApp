using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для BuildEditPage.xaml
    /// </summary>
    public partial class BuildEditPage : Page
    {
        MainWindow win;
        Build _current = new Build();
        Boolean newIns = true;
        public BuildEditPage(MainWindow _win, Build current)
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
                int id = stateUventaEntities.getContext().Build.ToList().Last().ID_Build;
                return id + 1;
            }
            catch { return 0; }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в teextBox
            StringBuilder error = new StringBuilder();
            
            if(string.IsNullOrEmpty(tbAdress.Text)) { error.AppendLine("Введите адрес здания"); }
            try
            {
                float area = int.Parse(tbFloor.Text);
            }
            catch (Exception) { error.AppendLine("Недопустимое число!"); }
            
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_Build >= 0)
            {
                if (newIns)
                {
                    _current.ID_Build = getNextId();
                }
                stateUventaEntities.getContext().Build.AddOrUpdate(_current);
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
