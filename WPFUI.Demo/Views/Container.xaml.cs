﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFUI.Appearance;
using WPFUI.Common;
using WPFUI.Controls;

namespace WPFUI.Demo.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Container()
        {
            //WPFUI.Appearance.Background.Apply(this, WPFUI.Appearance.BackgroundType.Mica);

            InitializeComponent();

            RootGrid.Visibility = Visibility.Collapsed;
            RootWelcomeGrid.Visibility = Visibility.Visible;

            //Closing += (sender, args) =>
            //{
            //    args.Cancel = true;
            //    System.Diagnostics.Debug.WriteLine($"DEBUG | Closing canceled", "WPFUI.Demo");
            //};

            Loaded += (sender, args) =>
            {
                WPFUI.Appearance.Watcher.Watch(this, Appearance.BackgroundType.Mica, true, true);

#if DEBUG
                RootNavigation.Items.Add(new NavigationItem { Page = typeof(Pages.Debug), Content = "Debug", Tag = "debug", Icon = SymbolRegular.Warning24, IconForeground = Brushes.Red, IconFilled = true });
#endif
            };

            WPFUI.Appearance.Theme.Changed += ThemeOnChanged;

            Task.Run(async () =>
            {
                await Task.Delay(4000);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    RootWelcomeGrid.Visibility = Visibility.Hidden;
                    RootGrid.Visibility = Visibility.Visible;
                });
            });

            //RootTitleBar.NotifyIconMenu = new ContextMenu();
            //RootTitleBar.NotifyIconMenu.Items.Add(new MenuItem() { Header = "Test #1" });
        }

        private void ThemeOnChanged(WPFUI.Appearance.ThemeType currentTheme, Color systemAccent)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG | {typeof(Container)} was informed that the theme has been changed to {currentTheme}", "WPFUI.Demo");
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG | Navigation was loaded", "WPFUI.Demo");
        }

        private void RootDialog_LeftButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Root dialog action button was clicked!", "WPFUI.Demo");
        }

        private void RootDialog_RightButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Root dialog custom right button was clicked!", "WPFUI.Demo");

            RootDialog.Hide();
        }

        private void RootNavigation_OnNavigated(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Page now is: " + (sender as NavigationFluent)?.PageNow, "WPFUI.Demo");
        }

        private void TitleBar_OnMinimizeClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Minimize button clicked", "WPFUI.Demo");
        }

        private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menuItem) return;

            string tag = menuItem.Tag as string ?? String.Empty;

            System.Diagnostics.Debug.WriteLine("DEBUG | Menu item clicked: " + tag, "WPFUI.Demo");
        }

        private void RootTitleBar_OnNotifyIconClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Notify SymbolRegular clicked", "WPFUI.Demo");
        }

        private void RootNavigation_OnNavigatedForward(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Navigated forward", "WPFUI.Demo");
        }

        private void RootNavigation_OnNavigatedBackward(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Navigated backward", "WPFUI.Demo");
        }

        private void RootSnackbar_OnClosed(Snackbar sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Snackbar closed", "WPFUI.Demo");
        }

        private void RootSnackbar_OnOpened(Snackbar sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Snackbar opened", "WPFUI.Demo");
        }

        private void RootDialog_OnOpened(Dialog sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Dialog opened", "WPFUI.Demo");
        }

        private void RootDialog_OnClosed(Dialog sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG | Dialog closed", "WPFUI.Demo");
        }

        private void ButtonToggleTheme_OnClick(object sender, RoutedEventArgs e)
        {
            WPFUI.Appearance.Theme.Set(
                WPFUI.Appearance.Theme.GetAppTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark,
                BackgroundType.Mica, true, false);
        }
    }
}
