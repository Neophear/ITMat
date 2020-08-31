using ITMat.Core.DTO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ITMat.UI.WindowsApp.Controls
{
    /// <summary>
    /// Interaction logic for CommentControl.xaml
    /// </summary>
    public partial class CommentControl : UserControl
    {
        #region CommentsSource
        public IEnumerable<CommentDTO> CommentsSource
        {
            get { return (IEnumerable<CommentDTO>)GetValue(CommentsSourceProperty); }
            set { SetValue(CommentsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommentsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentsSourceProperty =
            DependencyProperty.Register("CommentsSource", typeof(IEnumerable<CommentDTO>), typeof(CommentControl), new PropertyMetadata(new CommentDTO[0]));
        #endregion

        #region Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CommentControl), new PropertyMetadata(""));
        #endregion

        public event EventHandler CreateClicked;

        public CommentControl()
        {
            InitializeComponent();
            grdRoot.DataContext = this;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
            => CreateClicked?.Invoke(this, EventArgs.Empty);
    }
}