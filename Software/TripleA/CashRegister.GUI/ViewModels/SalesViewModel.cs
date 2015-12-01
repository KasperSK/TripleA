﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CashRegister.GUI.Dialogs;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly ISalesController _salesController;

        public SalesViewModel(ISalesController salesController)
        {
            _salesController = salesController;
            _salesController.PropertyChanged += OnCurrentOrderChanged;
        }

        public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();
        //Collection the salesView will bind to

        public string Total => _salesController.CurrentOrder.Total.ToString() + " kr.";

        private int _currentIndex = -1;

        

        

        public void OnCurrentOrderChanged(object sender, PropertyChangedEventArgs e) //Happening when receiving event from SalesController
        {
            var currentOrderLines = _salesController.CurrentOrderLines; //Retrieving currentorder via SAlesController

            ViewProducts.Clear();
            CurrentIndex = 0;

            foreach (var lineElement in currentOrderLines) //Itterating through all orderlines in currentorder
            {
                var price = (lineElement.UnitPrice*lineElement.Quantity).ToString(); //Making Total price for Orderline

                ViewProducts
                    .Add(new ViewProduct(lineElement.Quantity.ToString(), //Adding new Viewproducts to be displayed in SalesView
                        lineElement.Product.Name,
                        price));
                CurrentIndex++;
                OnPropertyChanged();
            }
            OnPropertyChanged(nameof(ViewProducts));
            OnPropertyChanged(nameof(Total));

        }

        public int CurrentIndex
        {
            get { return _currentIndex; }

            set
            {
                if (_currentIndex == value) return;
                else
                {
                    _currentIndex = value;
                    OnPropertyChanged(nameof(CurrentIndex));
                }
            }

        }

        public string GetTally()
        {
            return _salesController.Tally();
        }

        

        public class ViewProduct
        {
            public ViewProduct(string count, string name, string price)
            {
                Antal = count;

                Navn = name;

                Pris = price +" kr.";
            }

            public string Antal { get; set; }

            public string Navn { get; set; }

            public string Pris { get; set; }
        }

        //RelayCommands
        private RelayCommand<PaymentType> _paytypeCommand;
        public ICommand PaytypeCommand => _paytypeCommand ?? (_paytypeCommand = new RelayCommand<PaymentType>(PaytypeCommandExecute));

        private void PaytypeCommandExecute(PaymentType paymentType)
        {
            var amount = _salesController.MissingPaymentOnOrder();
            _salesController.StartPayment((int)amount, "", paymentType);
        }



        private RelayCommand _paymentCommand;
        public ICommand PaymentCommand => _paymentCommand ?? (_paymentCommand = new RelayCommand(PaymentCommandExecute, PaymentCommandCanExecute));

        private void PaymentCommandExecute()
        {
        }

        private bool PaymentCommandCanExecute()
        {
            return ViewProducts.Count > 0;
        }

        private RelayCommand _abortCommand;
        public ICommand AbortCommand => _abortCommand ?? (_abortCommand = new RelayCommand(AbortCommandExecute));

        private void AbortCommandExecute()
        {
            ViewProducts.Clear();
            _salesController.CancelOrder();
            OnPropertyChanged(nameof(Total));
        }

        
    }
}