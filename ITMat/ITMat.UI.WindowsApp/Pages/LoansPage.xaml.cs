﻿using ITMat.Core.DTO;
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
    /// Interaction logic for LoansPage.xaml
    /// </summary>
    public partial class LoansPage : AbstractPage
    {
        #region Loans
        public IEnumerable<LoanEmployeeListedDTO> Loans
        {
            get { return (IEnumerable<LoanEmployeeListedDTO>)GetValue(LoansProperty); }
            set { base.SetValue(LoansProperty, value); }
        }

        // Using a DependencyProperty as the backing store for loans.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoansProperty =
            DependencyProperty.Register("Loans", typeof(IEnumerable<LoanEmployeeListedDTO>), typeof(LoansPage), new PropertyMetadata(new LoanEmployeeListedDTO[0]));
        #endregion

        private readonly ILoanService service;

        public LoansPage()
        {
            InitializeComponent();

            service = App.ServiceProvider.GetRequiredService<ILoanService>();

            Loaded += LoansPage_Loaded;
        }

        private async void LoansPage_Loaded(object sender, RoutedEventArgs e)
        {
            StatusMessage = "Henter udlån...";

            try
            {
                Loans = (await service.GetLoansAsync()).OrderByDescending(l => l.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            StatusMessage = $"{Loans.Count()} udlån hentet.";
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => PageChangeRequest(new LoanPage((((DataGridRow)sender).Item as LoanEmployeeListedDTO).Id));
    }
}