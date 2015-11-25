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
        private ISalesController _salesController;


        [SetUp]
        public void Setup()
        {
           
            _salesController = Substitute.For<ISalesController>();

            _uut = new SalesViewModel(_salesController);

          
        }


        [Test]
        public void PaymentCommand_SalesControllerStart_StartPaymentCalled()
        {
            _salesController.MissingPaymentOnOrder().Returns(100);
            _uut.ViewProducts.Add(new SalesViewModel.ViewProduct("2", "Beer", "10"));
            var payment = _uut.PaytypeCommand;
            payment.Execute(PaymentType.Cash);
            
            
            _salesController.Received(1).StartPayment(100, Arg.Any<string>(), Arg.Any<PaymentType>());
        }

        [Test]
        public void AbortCommand_SalesController_ClearOrderCalled()
        {
            var abort = _uut.AbortCommand;
            abort.Execute(null);

            _salesController.Received(1).CancelOrder();
        }

        
    }
}