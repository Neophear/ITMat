using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ITMat.UI.WindowsApp.Pages
{
    public abstract class AbstractPage : Page
    {
        public List<MenuItem> Menu { get; } = new List<MenuItem>();

        public delegate void PageChangeRequestEventHandler(object sender, AbstractPage newPage);

        public event PageChangeRequestEventHandler OnPageChangeRequest;

        #region IsBusy
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(AbstractPage), new PropertyMetadata(false));
        #endregion

        #region BusyMessage
        public string BusyMessage
        {
            get { return (string)GetValue(BusyMessageProperty); }
            set { SetValue(BusyMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BusyMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusyMessageProperty =
            DependencyProperty.Register("BusyMessage", typeof(string), typeof(AbstractPage), new PropertyMetadata(""));
        #endregion

        #region Status
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(AbstractPage), new PropertyMetadata(""));
        #endregion

        protected void PageChangeRequest(AbstractPage newPage)
        {
            OnPageChangeRequest?.Invoke(this, newPage);
        }

        protected void AddMenuItem(string header, Action clickEvent)
        {
            MenuItem menuItem = new MenuItem { Header = header };
            menuItem.Click += (_, __) => clickEvent();
            Menu.Add(menuItem);
        }
    }
}