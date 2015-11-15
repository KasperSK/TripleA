﻿using System.Windows;

namespace CashRegister.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TestWindow UserControlTest = new TestWindow();
            UserControlTest.Show();
        }
    }
}
