using System;
using System.ComponentModel;
using CashRegister.GUI.Annotations;
using CashRegister.GUI.ViewModels;
using CashRegister.Models;
using CashRegister.Sales;
using NSubstitute;
using NUnit.Framework;



namespace CashRegister.Test.Unit.ViewModels
{
    [TestFixture]
    public class SalesViewModelUnitTest
    {
        private SalesViewModel _uut;
        private ISalesController _fakeSalesController;


        [SetUp]
        public void Setup()
        {

            _fakeSalesController = Substitute.For<ISalesController>();

            _uut = new SalesViewModel(_fakeSalesController);

            _fakeSalesController.PropertyChanged += _uut.OnCurrentOrderChanged;


        }


        [Test]
        public void PaymentCommand_SalesControllerStart_StartPaymentCalled()
        {
            _fakeSalesController.MissingPaymentOnOrder().Returns(100);
            _uut.ViewProducts.Add(new SalesViewModel.ViewProduct("2", "Beer", "10"));
            var payment = _uut.PaytypeCommand;
            payment.Execute(PaymentType.Cash);


            _fakeSalesController.Received(1).StartPayment(100, Arg.Any<string>(), Arg.Any<PaymentType>());
        }

        [Test]
        public void AbortCommand_SalesController_ClearOrderCalled()
        {
            var abort = _uut.AbortCommand;
            abort.Execute(null);

            _fakeSalesController.Received(1).CancelOrder();
        }
/*
        [Test]
        publi c void OnCurrentOrderChanged_OnPropertyChangedEvent_OnCurrentOrderChangedCalled()
        {
           
            var orderlines = new SalesOrder();

            PropertyChangedEventHandler propertyChanged;

            

            orderlines.Lines.Add(new OrderLine() {Product = new Product("øl",10,true),Quantity = 1,UnitPrice = 10});
            _fakeSalesController.CurrentOrder.Returns(orderlines);

            _fakeSalesController.AddProductToOrder(new Product("øl",10,true),1, null);
            

            _uut.OnCurrentOrderChanged();
            

            CollectionAssert.Contains(_uut.ViewProducts,new SalesViewModel.ViewProduct("1","øl","10"));

    
        }
*/
        
    }
}