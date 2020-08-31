using ITMat.UI.WindowsApp.Pages;
using System.Windows;
using System.Windows.Controls;

namespace ITMat.UI.WindowsApp.Controls
{
    /// <summary>
    /// Interaction logic for EditableTextBox.xaml
    /// </summary>
    public partial class EditableTextBox : TextBox
    {
        public PageMode Mode
        {
            get { return (PageMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(PageMode), typeof(EditableTextBox), new PropertyMetadata(PageMode.Read));

        public EditableTextBox()
            => InitializeComponent();
    }
}