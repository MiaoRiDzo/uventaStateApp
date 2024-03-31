using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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

namespace uventaStateApp.Resorces.Pages.PremisesPage.StatePage
{
    /// <summary>
    /// Логика взаимодействия для StateEditPage.xaml
    /// </summary>
    public partial class StateEditPage : Page
    {
        MainWindow win;
        Boolean newIns = true;
        State _current = new State();
        public StateEditPage(MainWindow _win, State current)
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

            if (string.IsNullOrEmpty(tbName.Text)) { error.AppendLine("Введите наименование"); }


            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_State >= 0)
            {
                if (newIns)
                {
                    _current.ID_State = getNextId();
                }

                stateUventaEntities.getContext().State.AddOrUpdate(_current);
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
