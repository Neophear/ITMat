using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ITMat.UI.WindowsApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : AbstractPage
    {
        #region Employee
        public EmployeeDTO Employee
        {
            get { return (EmployeeDTO)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeProperty =
            DependencyProperty.Register("Employee", typeof(EmployeeDTO), typeof(EmployeePage), new PropertyMetadata(null));
        #endregion

        #region Loans
        public IEnumerable<LoanDTO> Loans
        {
            get { return (IEnumerable<LoanDTO>)GetValue(LoansProperty); }
            set { SetValue(LoansProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Loans.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoansProperty =
            DependencyProperty.Register("Loans", typeof(IEnumerable<LoanDTO>), typeof(EmployeePage), new PropertyMetadata(new LoanDTO[0]));
        #endregion

        #region Editing
        public bool Editing
        {
            get { return (bool)GetValue(EditingProperty); }
            set { SetValue(EditingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Editing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingProperty =
            DependencyProperty.Register("Editing", typeof(bool), typeof(EmployeePage), new PropertyMetadata(false));
        #endregion

        #region Statuses
        public IEnumerable<EmployeeStatusDTO> Statuses
        {
            get { return (IEnumerable<EmployeeStatusDTO>)GetValue(StatusesProperty); }
            set { SetValue(StatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeStatuses.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register("Statuses", typeof(IEnumerable<EmployeeStatusDTO>), typeof(EmployeePage), new PropertyMetadata(new EmployeeStatusDTO[0]));
        #endregion


        private readonly IEmployeeService employeeService;
        private readonly ILoanService loanService;

        public EmployeePage(int id)
        {
            InitializeComponent();

            employeeService = App.ServiceProvider.GetRequiredService<IEmployeeService>();
            loanService = App.ServiceProvider.GetRequiredService<ILoanService>();

            Loaded += (_, __) => LoadData(id);
        }

        private async void LoadData(int id)
        {
            Status = "Henter medarbejder...";

            try
            {
                Statuses = await employeeService.GetStatusesAsync();
                Employee = await employeeService.GetEmployeeAsync(id);
                Loans = await loanService.GetEmployeeLoansAsync(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Status = $"{Employee.Name} hentet.";
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Editing = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Editing = false;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Editing = false;
        }
    }
}