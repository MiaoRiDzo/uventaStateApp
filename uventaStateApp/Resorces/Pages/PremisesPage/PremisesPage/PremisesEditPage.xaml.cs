using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    public partial class PremisesEditPage : Page
    {
        MainWindow win;
        Premises _current = new Premises();
        Boolean newIns = true;
        public PremisesEditPage(MainWindow _win, Premises current)
        {
            InitializeComponent();
            win = _win;
            if(current != null)
            {
                _current = current;
                newIns = false;
                tbHeader.Text = "Редактирование";
            }
            cbState.ItemsSource = stateUventaEntities.getContext().State.ToList();
            cbBuild.ItemsSource = stateUventaEntities.getContext().Build.ToList();
            DataContext = _current;
        }

        private static int getNextId()
        {
            try
            {
                int id = stateUventaEntities.getContext().Premises.ToList().Last().ID_Premises;
                return id + 1;
            }
            catch { return 0; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на ошибки в teextBox
            StringBuilder error = new StringBuilder();
            if(string.IsNullOrEmpty(tbName.Text)) { error.AppendLine("Введите наименование!"); }
            if(cbBuild.SelectedItem == null) { error.AppendLine("Введите адрес!"); }
            if (cbState.SelectedItem == null) { error.AppendLine("Не выбрано состояние"); }
            try
            {
                float area = float.Parse(tbArea.Text);
            }
            catch(Exception) { error.AppendLine("Недопустимое число!"); }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_current.ID_Premises >= 0)
            {
                if (newIns)
                {
                    _current.ID_Premises = getNextId();
                    Build build = cbBuild.SelectedItem as Build;
                    _current.ID_Build = build.ID_Build;
                    State state = cbState.SelectedItem as State;
                    _current.ID_State = state.ID_State;
                }

                stateUventaEntities.getContext().Premises.AddOrUpdate(_current);
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
