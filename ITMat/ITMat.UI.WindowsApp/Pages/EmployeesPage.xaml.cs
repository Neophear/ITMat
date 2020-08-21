using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ITMat.UI.WindowsApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : AbstractPage
    {
        #region Employees
        public IEnumerable<EmployeeDTO> Employees
        {
            get { return (IEnumerable<EmployeeDTO>)GetValue(EmployeesProperty); }
            set { base.SetValue(EmployeesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for employees.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeesProperty =
            DependencyProperty.Register("Employees", typeof(IEnumerable<EmployeeDTO>), typeof(EmployeesPage), new PropertyMetadata(new EmployeeDTO[0]));
        #endregion

        #region Statuses
        public IEnumerable<EmployeeStatusDTO> Statuses
        {
            get { return (IEnumerable<EmployeeStatusDTO>)GetValue(StatusesProperty); }
            set { SetValue(StatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeStatuses.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register("Statuses", typeof(IEnumerable<EmployeeStatusDTO>), typeof(EmployeesPage), new PropertyMetadata(new EmployeeStatusDTO[0]));
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
            Status = "Henter ansatte...";

            try
            {
                Statuses = await service.GetStatusesAsync();
                Employees = await service.GetEmployeesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Status = $"{Employees.Count()} medarbejdere hentet.";
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => PageChangeRequest(new EmployeePage((((DataGridRow)sender).Item as EmployeeDTO).Id));
    }
}