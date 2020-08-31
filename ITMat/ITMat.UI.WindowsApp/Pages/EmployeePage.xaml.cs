using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Controls;
using ITMat.UI.WindowsApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        #region Status
        public EmployeeStatusDTO Status
        {
            get { return (EmployeeStatusDTO)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(EmployeeStatusDTO), typeof(EmployeePage), new PropertyMetadata(null));
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

        #region Loans
        public IEnumerable<LoanListedDTO> Loans
        {
            get { return (IEnumerable<LoanListedDTO>)GetValue(LoansProperty); }
            set { SetValue(LoansProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Loans.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoansProperty =
            DependencyProperty.Register("Loans", typeof(IEnumerable<LoanListedDTO>), typeof(EmployeePage), new PropertyMetadata(new LoanListedDTO[0]));
        #endregion

        #region Comments
        public IEnumerable<CommentDTO> Comments
        {
            get { return (IEnumerable<CommentDTO>)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Comments.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register("Comments", typeof(IEnumerable<CommentDTO>), typeof(EmployeePage), new PropertyMetadata(null));
        #endregion

        private EmployeeDTO originalEmployee;

        private readonly IEmployeeService employeeService;
        private readonly ICommentService commentService;
        private readonly ILoanService loanService;

        public EmployeePage(int id)
        {
            InitializeComponent();

            Mode = PageMode.Read;

            employeeService = App.ServiceProvider.GetRequiredService<IEmployeeService>();
            commentService = App.ServiceProvider.GetRequiredService<ICommentService>();
            loanService = App.ServiceProvider.GetRequiredService<ILoanService>();

            Loaded += (_, __) => LoadData(id);
        }

        private async void LoadData(int id)
        {
            Mode = PageMode.BusyLoading;

            StatusMessage = "Henter medarbejder...";

            try
            {
                Statuses = await employeeService.GetStatusesAsync();

                var employee = await employeeService.GetEmployeeAsync(id);
                SetValues(employee);
                Comments = await commentService.GetEmployeeCommentsAsync(id);
                Loans = await loanService.GetEmployeeLoansAsync(id);

                StatusMessage = $"{Name} hentet.";
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }

            Mode = PageMode.Read;
        }

        private void SetValues(EmployeeDTO employee)
        {
            Id = employee.Id;
            MANR = employee.MANR;
            Name = employee.Name;
            Status = Statuses.FirstOrDefault(s => s.Id == employee.Status);
        }

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Mode = PageMode.BusyCreating;
            StatusMessage = "Opretter medarbejder...";

            try
            {
                var newId = await employeeService.CreateEmployeeAsync(new EmployeeDTO
                {
                    MANR = MANR,
                    Name = Name,
                    Status = Status?.Id ?? 0
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
                Status = Status?.Id ?? 0
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
            StatusMessage = "Gemmer medarbejder...";
            Mode = PageMode.BusyUpdating;

            try
            {
                await employeeService.UpdateEmployeeAsync(Id, new EmployeeDTO { Id = Id, MANR = MANR, Name = Name, Status = Status?.Id ?? 0 });
                var updatedEmployee = await employeeService.GetEmployeeAsync(Id);
                SetValues(updatedEmployee);

                StatusMessage = "Gemt!";
                Mode = PageMode.Read;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mode = PageMode.Edit;
            }
        }

        private async void CommentControl_CreateClicked(object sender, EventArgs e)
        {
            if (!(sender is CommentControl commentCtrl))
                return;

            commentCtrl.IsEnabled = false;
            StatusMessage = "Opretter kommentar...";

            try
            {
                await commentService.CreateEmployeeCommentAsync(Id, commentCtrl.Text);
                Comments = await commentService.GetEmployeeCommentsAsync(Id);
                commentCtrl.Text = "";
                StatusMessage = "Kommentar oprettet!";
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message; 
            }
            finally
            {
                commentCtrl.IsEnabled = true;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => PageChangeRequest(new LoanPage((((DataGridRow)sender).Item as LoanListedDTO).Id));
    }
}