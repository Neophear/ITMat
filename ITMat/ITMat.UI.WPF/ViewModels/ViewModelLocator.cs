using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITMat.UI.WPF.ViewModels
{
    //https://marcominerva.wordpress.com/2020/01/07/using-the-mvvm-pattern-in-wpf-applications-running-on-net-core/
    public class ViewModelLocator
    {
        public EmployeesViewModel EmployeesViewModel
            => App.ServiceProvider.GetRequiredService<EmployeesViewModel>();
    }
}