using ITMat.UI.WindowsApp.Pages;
using System.Windows;

namespace ITMat.UI.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ChangePage(new EmployeesPage());
        }

        public void ChangePage(AbstractPage p)
        {
            if (frmContent.Content != null)
                foreach (var menuItem in ((AbstractPage)frmContent.Content).Menu)
                    mnuMain.Items.Remove(menuItem);

            p.OnPageChangeRequest += (_, newPage) => ChangePage(newPage);

            frmContent.Navigate(p);

            foreach (var menuItem in p.Menu)
                mnuMain.Items.Add(menuItem);
        }

        private void MenuFileAbout(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFileExit(object sender, RoutedEventArgs e)
            => Close();

        private void MenuStartPage(object sender, RoutedEventArgs e)
            => ChangePage(new StartPage());

        private void MenuEmployees(object sender, RoutedEventArgs e)
            => ChangePage(new EmployeesPage());
    }
}