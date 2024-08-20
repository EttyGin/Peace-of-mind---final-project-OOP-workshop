﻿using loginDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace loginDb.View
{
    /// <summary>
    /// Interaction logic for AddOrEditClientView.xaml
    /// </summary>
    public partial class AddOrEditClientView : Window
    {
        public AddOrEditClientView(ClientsViewModel.EditMode mode, Client client)
        {
            InitializeComponent();
            AddOrEditClientViewModel acvm = new AddOrEditClientViewModel (mode, client);
            this.DataContext = acvm;

        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
