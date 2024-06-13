using PP05Tretyakov.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private PP05TretyakovEntities _db;
        public MainWindow()
        {
            InitializeComponent();
            _db = new PP05TretyakovEntities();
            _db.Employee.Load();
            _db.Contract.Load();
            _db.Department.Load();
        }



        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var tableName = menuItem.Name.Replace("Refresh", "").Replace("Btn", "");

            switch (tableName)
            {
                case "Employee":
                    EmployeeDG.ItemsSource = _db.Employee.Local.ToBindingList();
                    DGComboBoxColumnDepartmentName.ItemsSource = _db.Department.ToList();
                    DGComboBoxColumnContractNumber.ItemsSource = _db.Contract.ToList();
                    foreach (var item in _db.Department.ToList())
                    {
                        if(!CBDepartment.Items.Equals(item))
                        {
                            CBDepartment.Items.Add(item.Name);
                        }
                    }
                    break;
                case "Contract": ContractDG.ItemsSource = _db.Contract.Local.ToBindingList(); break;
                case "Department": DepartmentDG.ItemsSource = _db.Department.Local.ToBindingList(); break;
                default: MessageBox.Show("No find any table", "Error"); break;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
        }


        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _db.Dispose();
        }

        private void CBDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t = CBDepartment.SelectedValue.ToString();
            if(t.Contains("All"))
            {
                EmployeeDG.ItemsSource = _db.Employee.Local.ToBindingList();
                decimal summary = 0;
                foreach (var item in _db.Contract.Local.ToList())
                {
                    summary += item.Amount_Contract;
                }
                TBSummary.Text = summary.ToString();
            }
            else
            {
                EmployeeDG.ItemsSource = _db.Employee.Local.Where(x => x.Department_Name == t).ToList();
            }
        }
    }
}
