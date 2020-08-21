using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ITMat.UI.WPF.Interfaces;
using ITMat.UI.WPF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WPF.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        private IEnumerable<Employee> employees;
        public IEnumerable<Employee> Employees
        {
            get => employees;
            set => Set(ref employees, value);
        }

        private readonly IEmployeeService service;

        public RelayCommand RefreshCommand { get; }

        public EmployeesViewModel(IEmployeeService service)
        {
            this.service = service;
            RefreshCommand = new RelayCommand(async () => await RefreshAsync());
        }

        public async Task LoadData()
        {
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            Employees = await service.GetEmployeesAsync();
        }
    }
}