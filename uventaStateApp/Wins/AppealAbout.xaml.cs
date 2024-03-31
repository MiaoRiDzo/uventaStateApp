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
using System.Windows.Shapes;
using uventaStateApp.Resorces.Libs;

namespace uventaStateApp.Wins
{
    /// <summary>
    /// Логика взаимодействия для AppealAbout.xaml
    /// </summary>
    public partial class AppealAbout : Window
    {
        MainWindow win;
        Appeal _cuurent = new Appeal();
        public AppealAbout(MainWindow win, Appeal current)
        {
            InitializeComponent();
            this.win = win;
            _cuurent = current;
            DataContext = _cuurent;
            cbStatus.ItemsSource = stateUventaEntities.getContext().Status.ToList();
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status status = cbStatus.SelectedItem as Status;
            _cuurent.ID_Status = status.ID_Status;

            stateUventaEntities.getContext().Appeal.AddOrUpdate(_cuurent);
            try
            {
                stateUventaEntities.getContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }
    }
}
