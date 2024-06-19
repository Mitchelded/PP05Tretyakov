using System;
using System.Collections;
using System.Collections.Generic;
using PP05Tretyakov.Model;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace PP05Tretyakov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly PP05TretyakovEntities _db;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _db = new PP05TretyakovEntities();
                try
                {
                    _db.Database.CreateIfNotExists();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Create Database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _db.Employee.Load();
                _db.Contract.Load();
                _db.Department.Load();
                CbDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is MenuItem menuItem)
                {
                    var tableName = menuItem.Name.Replace("Refresh", "").Replace("Btn", "");

                    switch (tableName)
                    {
                        case "Employee":
                            var t = CbDepartment.SelectedValue.ToString();
                            if (t.Contains("All"))
                            {
                                EmployeeDg.ItemsSource = _db.Employee.Local.ToBindingList();
                                var summary = CalculationOfContractAmounts();
                                TbSummary.Text = summary.ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                EmployeeDg.ItemsSource = _db.Employee.Local.Where(x => x.Department_Name == t).ToList();
                            }

                            DgComboBoxColumnDepartmentName.ItemsSource = _db.Department.ToList();
                            DgComboBoxColumnContractNumber.ItemsSource = _db.Contract.ToList();

                            var departments = _db.Department.ToList();
                            var existingItems = new HashSet<string>();

                            foreach (var item in CbDepartment.Items)
                            {
                                existingItems.Add(item.ToString());
                            }

                            foreach (var department in departments)
                            {
                                if (!existingItems.Contains(department.Name))
                                {
                                    CbDepartment.Items.Add(department.Name);
                                }
                            }

                            break;
                        case "Contract":
                            ContractDg.ItemsSource = _db.Contract.Local.ToBindingList();
                            break;
                        case "Department":
                            DepartmentDg.ItemsSource = _db.Department.Local.ToBindingList();
                            break;
                        default:
                            MessageBox.Show("No find any table", "Error");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _db.Dispose();
        }

        private void CBDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var t = CbDepartment.SelectedValue.ToString();
                if (t.Contains("All"))
                {
                    EmployeeDg.ItemsSource = _db.Employee.Local.ToBindingList();
                    var summary = CalculationOfContractAmounts();
                    TbSummary.Text = summary.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    EmployeeDg.ItemsSource = _db.Employee.Local.Where(x => x.Department_Name == t).ToList();
                    var summary = CalculationOfContractAmounts();
                    TbSummary.Text = summary.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal CalculationOfContractAmounts()
        {
            try
            {
                decimal summary = 0;
                foreach (var item in EmployeeDg.ItemsSource)
                {
                    if (item is Employee employee)
                    {
                        var contractAmount = employee.Amount_Employees_Contract;
                        summary += contractAmount;
                    }
                }

                return summary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        private void ExportDataGridToExcel(DataGrid dataGrid, List<string> columnsToExclude, decimal summary = 0)
        {
            try
            {
                // Открываем диалоговое окно "Сохранить как"
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = ".xlsx",
                    FileName = "Report.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Создаем новую книгу Excel
                    using (var workbook = new XLWorkbook())
                    {
                        // Создаем рабочий лист
                        var worksheet = workbook.Worksheets.Add("Sheet1");

                        // Получаем элементы DataGrid
                        if (!(dataGrid.ItemsSource is IEnumerable itemsSource)) return;

                        // Получаем тип данных элементов
                        var enumerable = itemsSource as object[] ?? itemsSource.Cast<object>().ToArray();
                        var itemType = enumerable.FirstOrDefault()?.GetType();
                        if (itemType == null) return;

                        // Получаем свойства типа данных
                        var properties = itemType.GetProperties()
                            .Where(p => !columnsToExclude.Contains(p.Name))
                            .ToArray();

                        // Записываем заголовки
                        int colIndex = 1;
                        foreach (var column in dataGrid.Columns)
                        {
                            var header = column.Header.ToString();
                            if (!columnsToExclude.Contains(header))
                            {
                                worksheet.Cell(1, colIndex).Value = header;
                                colIndex++;
                            }
                        }

                        // Записываем данные
                        int row = 2;
                        foreach (var item in enumerable)
                        {
                            for (int col = 0; col < properties.Length; col++)
                            {
                                var value = properties[col].GetValue(item);
                                worksheet.Cell(row, col + 1).Value = value?.ToString();
                            }

                            row++;
                        }

                        worksheet.Cell(row, 1).Value = "Summary";
                        worksheet.Cell(row, 2).Value = summary;
                        // Сохраняем файл
                        workbook.SaveAs(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnToExcel_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TabControlName.SelectedItem is TabItem tableName)
                    switch (tableName.Header)
                    {
                        case "Employee":
                            var columnsToEmployeeExclude = new List<string> { "Contract", "Department", "Id" };
                            ExportDataGridToExcel(EmployeeDg, columnsToEmployeeExclude, decimal.Parse(TbSummary.Text));
                            break;
                        case "Contract":
                            var columnsToContractExclude = new List<string> { "Id" };
                            ExportDataGridToExcel(ContractDg, columnsToContractExclude);
                            break;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}