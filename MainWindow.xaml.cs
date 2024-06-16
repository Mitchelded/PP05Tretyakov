using PP05Tretyakov.Model;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            CBDepartment.SelectedIndex = 0;
        }



        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var tableName = menuItem.Name.Replace("Refresh", "").Replace("Btn", "");

            switch (tableName)
            {
                case "Employee":
                    var t = CBDepartment.SelectedValue.ToString();
                    if(t.Contains("All"))
                    {
                        EmployeeDG.ItemsSource = _db.Employee.Local.ToBindingList();
                        var summary = CalculationOfContractAmounts();
                        TBSummary.Text = summary.ToString();
                    }
                    else
                    {
                        EmployeeDG.ItemsSource = _db.Employee.Local.Where(x => x.Department_Name == t).ToList();
                    }
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
                var summary = CalculationOfContractAmounts();
                TBSummary.Text = summary.ToString();
            }
            else
            {
                EmployeeDG.ItemsSource = _db.Employee.Local.Where(x => x.Department_Name == t).ToList();
            }
        }

        private decimal CalculationOfContractAmounts()
        {
            decimal summary = 0;
            foreach (var item in EmployeeDG.ItemsSource)
            {
                // var contractAmount = _db.Contract
                //     .Where(x => x.Number == employee.Contract_Number)
                //     .Sum(x => x.Amount_Contract);
                if (item is Employee employee)
                {
                    var contractAmount = employee.Amount_Employees_Contract;
                    summary += contractAmount;
                }
            }
            return summary;
        }
    }
}
