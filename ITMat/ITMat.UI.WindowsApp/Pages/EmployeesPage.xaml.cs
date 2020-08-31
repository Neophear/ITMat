using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services;
using ITMat.UI.WindowsApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ITMat.UI.WindowsApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : AbstractPage
    {
        #region Employees
        public IEnumerable<EmployeeListedDTO> Employees
        {
            get { return (IEnumerable<EmployeeListedDTO>)GetValue(EmployeesProperty); }
            set { base.SetValue(EmployeesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for employees.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeesProperty =
            DependencyProperty.Register("Employees", typeof(IEnumerable<EmployeeListedDTO>), typeof(EmployeesPage), new PropertyMetadata(new EmployeeListedDTO[0]));
        #endregion

        #region Filter
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Filter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(EmployeesPage), new PropertyMetadata(""));
        #endregion

        private readonly IEmployeeService service;

        public EmployeesPage()
        {
            InitializeComponent();

            service = App.ServiceProvider.GetRequiredService<IEmployeeService>();

            Loaded += EmployeesPage_Loaded;
        }

        private async void EmployeesPage_Loaded(object sender, RoutedEventArgs e)
        {
            StatusMessage = "Henter ansatte...";

            try
            {
                Employees = await service.GetEmployeesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            StatusMessage = $"{Employees.Count()} medarbejdere hentet.";
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => PageChangeRequest(new EmployeePage((((DataGridRow)sender).Item as EmployeeListedDTO).Id));

        private void TxtFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dgEmployees.ItemsSource);
            view.Refresh();
            StatusMessage = $"{view.Cast<EmployeeListedDTO>().Count()} medarbejdere vist.";
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var filter = txtFilter.Text.ToLower();

            if (String.IsNullOrEmpty(filter))
                e.Accepted = true;
            else if (e.Item is EmployeeListedDTO employee)
            {
                if (Int32.TryParse(filter, out int id))
                    e.Accepted = employee.Id == id || employee.MANR.Contains(filter);
                else
                    e.Accepted = employee.Name.ToLower().Contains(filter);
            }
        }
    }
}