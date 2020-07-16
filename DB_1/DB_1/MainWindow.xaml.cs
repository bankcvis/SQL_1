using System;
using System.Collections;
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

namespace DB_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataAccess.InitializeDatabase();


        }

        private void btnCreateDB_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.InitializeDatabase();
            MessageBox.Show("Create Database complete");
        }

        private void btnAddData_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.AddData(txtFirstName.Text, txtLastName.Text);
            txtFirstName.Clear();
            txtLastName.Clear();

        }

        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            string dataMessage = "" ;
            foreach (string i in DataAccess.GetData()) {
                dataMessage = dataMessage + i + "\n";
            }

            MessageBox.Show(dataMessage);
        }
    }
}
