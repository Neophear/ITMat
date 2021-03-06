﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ITMat.UI.WindowsApp.Pages
{
    public abstract class AbstractPage : Page
    {
        #region StatusMessage
        public string StatusMessage
        {
            get { return (string)GetValue(StatusMessageProperty); }
            set { SetValue(StatusMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusMessageProperty =
            DependencyProperty.Register("StatusMessage", typeof(String), typeof(AbstractPage), new PropertyMetadata(""));
        #endregion

        #region Mode
        public PageMode Mode
        {
            get { return (PageMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(PageMode), typeof(AbstractPage), new PropertyMetadata(PageMode.Read));
        #endregion

        public List<MenuItem> Menu { get; } = new List<MenuItem>();

        public delegate void PageChangeRequestEventHandler(object sender, AbstractPage newPage);

        public event PageChangeRequestEventHandler OnPageChangeRequest;

        protected void PageChangeRequest(AbstractPage newPage)
            => OnPageChangeRequest?.Invoke(this, newPage);

        protected void AddMenuItem(string header, Action clickEvent)
        {
            MenuItem menuItem = new MenuItem { Header = header };
            menuItem.Click += (_, __) => clickEvent();
            Menu.Add(menuItem);
        }
    }
}