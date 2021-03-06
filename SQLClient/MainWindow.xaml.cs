﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SQLClient.DBInteraction;

namespace SQLClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void HandleConnect(object sender, RoutedEventArgs e) {
            ConnectDialog dialog = new ConnectDialog();
            if (dialog.ShowDialog().GetValueOrDefault(false)) {
                ConnectionInfo info = dialog.ConnectionInfo;
                if (info != null) {
                    // get rid of the 'Welcome' tab -- has no effect if we've already removed it
                    _tabs.Items.Remove(_welcomeTab);

                    TabItem tab = new TabItem();
                    TextBlock headerText = new TextBlock();
                    headerText.Text = dialog.ConnectionName;
                    tab.Header = headerText;

                    QueryControl queryCtrl = new QueryControl();
                    queryCtrl.Controller = new QueryController(info);
                    tab.Content = queryCtrl;

                    _tabs.Items.Add(tab);
                    _tabs.SelectedItem = tab;
                }
            }
        }

        private void HandleTabClosing(object sender, RoutedEventArgs e) {
            DependencyObject clickedButton = (DependencyObject)sender;

            TabItem itemToRemove = null;
            foreach(TabItem item in _tabs.Items) {
                if( item.IsAncestorOf(clickedButton)) {
                    itemToRemove = item;
                    break;
                }
            }
            if( itemToRemove != null ) {
                _tabs.Items.Remove(itemToRemove);
            }
            
        }

        private void HandleLoad(object sender, RoutedEventArgs e)
        {
        }
    }
}