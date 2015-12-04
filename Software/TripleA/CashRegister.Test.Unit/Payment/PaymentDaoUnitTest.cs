using CashRegister.Dal;
using CashRegister.Models;
using CashRegister.Payment;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    [TestFixture]
    public class PaymentDaoUnitTest
    {
        private int _id;
        private IPaymentDao _uut;
        private IDalFacade _fakeDalFacade;
        private Transaction _fakeTransaction;

        [SetUp]
        public void SetUp()
        {
            _id = 1;
            _fakeTransaction = Substitute.For<Transaction>();
            _fakeDalFacade = Substitute.For<IDalFacade>();
            _uut = new PaymentDao(_fakeDalFacade);
        }

        [Test]
        public void Delete_DeleteTransaktionFromDB_CallDalDelete()
        {
            _uut.Delete(_fakeTransaction);

            _fakeDalFacade.Received(1).UnitOfWork.TransactionRepository.Delete(_fakeTransaction);
        }

        [Test]
        public void Insert_InsertTransaktionFromDB_CallDalInsert()
        {
            _uut.Insert(_fakeTransaction);

            _fakeDalFacade.Received(1).UnitOfWork.TransactionRepository.Insert(_fakeTransaction);
        }

        [Test]
        public void SelectByTransactionId_SelectTransactionFromDbById_CallDallGetById()
        {
            _uut.SelectByTransactionId(_id);

            var temp = _fakeDalFacade.UnitOfWork.TransactionRepository.GetById(_id);
        }

        [Test]
        public void Update_UpdateTransactionFromDB_CallDalUpdate()
        {
            _uut.Update(_fakeTransaction);

            _fakeDalFacade.Received(1).UnitOfWork.TransactionRepository.Update(_fakeTransaction);
        }
    }
}