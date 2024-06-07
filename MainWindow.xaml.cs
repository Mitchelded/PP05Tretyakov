using PP05Tretyakov.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PP05Tretyakov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PP05TretyakovEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new PP05TretyakovEntities();
            db.Employee.Load();
            db.Contract.Load();
            db.Department.Load();

        }



        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var tableName = menuItem.Name.Replace("Refresh", "").Replace("Btn", "");

            switch (tableName)
            {
                case "Employee": EmployeeDG.ItemsSource = db.Employee.Local.ToBindingList(); break;
                case "Contract": ContractDG.ItemsSource = db.Contract.Local.ToBindingList(); break;
                case "Department": DepartmentDG.ItemsSource = db.Department.Local.ToBindingList(); break;
                default: MessageBox.Show("No find any table", "Error"); break;
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            db.SaveChanges();

        }

        private void TabItem_Unloaded(object sender, RoutedEventArgs e)
        {
            //var menuItem = sender as MenuItem;
            //var tableName = menuItem.Name;
            //using (var db = new PP05TretyakovEntities())
            //{
            //    switch (tableName)
            //    {
            //        case "Employee": db.Employee.; break;
            //        case "Contract": ContractDG.ItemsSource = db.Contract.ToList(); break;
            //        case "Department": DepartmentDG.ItemsSource = db.Department.ToList(); break;
            //        default: MessageBox.Show("No find any table", "Error"); break;
            //    }
            //}
        }
    }
}
