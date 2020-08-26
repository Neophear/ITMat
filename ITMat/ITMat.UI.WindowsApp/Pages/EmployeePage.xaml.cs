using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ITMat.UI.WindowsApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : AbstractPage
    {
        #region Id
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(EmployeePage), new PropertyMetadata(0));
        #endregion

        #region MANR
        public string MANR
        {
            get { return (string)GetValue(MANRProperty); }
            set { SetValue(MANRProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MANR.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MANRProperty =
            DependencyProperty.Register("MANR", typeof(string), typeof(EmployeePage), new PropertyMetadata(""));
        #endregion

        #region Name
        public new string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(EmployeePage), new PropertyMetadata(""));
        #endregion

        #region StatusId
        public int StatusId
        {
            get { return (int)GetValue(StatusIdProperty); }
            set { SetValue(StatusIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusIdProperty =
            DependencyProperty.Register("StatusId", typeof(int), typeof(EmployeePage), new PropertyMetadata(0));
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

        private EmployeeDTO originalEmployee;
        private readonly IEmployeeService employeeService;
        private readonly ILoanService loanService;

        public EmployeePage(int id)
        {
            InitializeComponent();

            Mode = PageMode.Read;

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

                var employee = await employeeService.GetEmployeeAsync(id);
                SetValues(employee);

                Loans = await loanService.GetEmployeeLoansAsync(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Status = $"{Name} hentet.";
        }

        private void SetValues(EmployeeDTO employee)
        {
            Id = employee.Id;
            MANR = employee.MANR;
            Name = employee.Name;
            StatusId = employee.Status;
        }

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Mode = PageMode.BusyCreating;
            Status = "Opretter medarbejder...";

            try
            {
                var newId = await employeeService.CreateEmployeeAsync(new EmployeeDTO
                {
                    MANR = MANR,
                    Name = Name,
                    Status = StatusId
                });

                PageChangeRequest(new EmployeePage(newId));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mode = PageMode.Create;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            originalEmployee = new EmployeeDTO
            {
                Id = Id,
                MANR = MANR,
                Name = Name,
                Status = StatusId
            };

            Mode = PageMode.Edit;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetValues(originalEmployee);
            Mode = PageMode.Read;
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Status = "Gemmer medarbejder...";
            Mode = PageMode.BusyUpdating;
            try
            {

                await employeeService.UpdateEmployeeAsync(Id, new EmployeeDTO { Id = Id, MANR = MANR, Name = Name, Status = StatusId });
                var updatedEmployee = await employeeService.GetEmployeeAsync(Id);
                SetValues(updatedEmployee);

                Status = "Gemt!";
                Mode = PageMode.Read;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mode = PageMode.Edit;
            }
        }
    }
}