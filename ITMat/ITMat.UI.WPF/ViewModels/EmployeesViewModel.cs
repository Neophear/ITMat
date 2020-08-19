using ITMat.UI.WPF.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ITMat.UI.WPF.ViewModels
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private IEnumerable<Employee> employees;
        public IEnumerable<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        public EmployeesViewModel()
        {
            Employees = new List<Employee>
            {
                new Employee { Id = 1, MANR = "370929", Name = "Stiig Gade", Status = new EmployeeStatus { Name = "Aktiv" } },
                new Employee { Id = 2, MANR = "123456", Name = "Peter Petersen", Status = new EmployeeStatus { Name = "Aktiv" } },
                new Employee { Id = 3, MANR = "654321", Name = "Jørgen Jørgensen", Status = new EmployeeStatus { Name = "Aktiv" } },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}