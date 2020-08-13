using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace ITMat.UI.WPF.ServiceLayer
{
    public class EmployeeService
    {
        private readonly IRestClient client;

        public EmployeeService()
        {
            client = new RestClient("https://localhost:44377");
        }
    }
}