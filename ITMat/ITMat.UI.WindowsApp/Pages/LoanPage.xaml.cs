using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
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

namespace ITMat.UI.WindowsApp.Pages
{
    /// <summary>
    /// Interaction logic for LoanPage.xaml
    /// </summary>
    public partial class LoanPage : AbstractPage
    {
        #region Id
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(MainWindow), new PropertyMetadata(0));
        #endregion



        public EmployeeDTO Employee
        {
            get { return (EmployeeDTO)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Employee.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeProperty =
            DependencyProperty.Register("Employee", typeof(EmployeeDTO), typeof(MainWindow), new PropertyMetadata(null));



        private LoanDTO originalLoan;

        private readonly ILoanService loanService;
        private readonly IEmployeeService employeeService;
        private readonly ICommentService commentService;

        public LoanPage(int id)
        {
            InitializeComponent();

            Mode = PageMode.Read;

            loanService = App.ServiceProvider.GetRequiredService<ILoanService>();
            employeeService = App.ServiceProvider.GetRequiredService<IEmployeeService>();
            commentService = App.ServiceProvider.GetRequiredService<ICommentService>();

            Loaded += (_, __) => LoadData(id);
        }

        private async void LoadData(int id)
        {
            Mode = PageMode.BusyLoading;

            StatusMessage = "Henter udlån...";

            try
            {
                var loan = await loanService.GetLoanAsync(id);
                var employee = await employeeService.GetEmployeeAsync(loan.EmployeeId);

                SetValues(loan, employee);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void SetValues(LoanDTO loan, EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}