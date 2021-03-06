﻿using System.Data.Entity;
using System.Windows;
using CashRegister.Database;
using CashRegister.GUI.ViewModels;
using CashRegister.Log;
using CashRegister.Sales;

namespace CashRegister.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Startup method for the application.
        /// </summary>
        /// <param name="e">The arguments sent with the event.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            LogFactory.Configure("cash.log", true);
            IDatabaseInitializer<CashRegisterContext> seed;

            // Empty
            seed = null;

            // Kalle Seed
            // seed = new CashProductInitializer();

            // Lærke Seed
            seed = new FullProductInitializer();

            using (var contex = new CashRegisterContext(seed))
            {
                contex.Database.Initialize(true);
            }

            base.OnStartup(e);
            Window win = new MainWindow();
            var salesCtrl = SalesFactory.GuiSalesController;
            win.DataContext = new MainViewModel(salesCtrl);
            win.Show();
        }
    }
}
