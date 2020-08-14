using ITMat.UI.WPF.Interfaces;
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

        public EmployeesViewModel(IEmployeeService service)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}