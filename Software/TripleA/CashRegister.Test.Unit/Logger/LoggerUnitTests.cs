using CashRegister.Log;
using log4net;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Logger
{
    [TestFixture]
    public class LoggerUnitTests
    {
        ILog _fakeBackend;
        ILogger _uut;
        string _logline = "Hola";

        [SetUp]
        public void SetUp()
        {
            _fakeBackend = Substitute.For<ILog>();
            _uut = new Log.Logger(_fakeBackend);

        }

        [Test]
        public void Fatal_BackendFatels_GetsCalled()
        {
            _uut.Fatal(_logline);
            _fakeBackend.Received(1).Fatal(_logline);
        }

        [Test]
        public void Err_BackendError_GetsCalled()
        {
            _uut.Err(_logline);
            _fakeBackend.Received(1).Error(_logline);
        }

        [Test]
        public void Warn_BackendWarns_GetsCalled()
        {
            _uut.Warn(_logline);
            _fakeBackend.Received(1).Warn(_logline);
        }

        [Test]
        public void Info_BackendInfo_GetsCalled()
        {
            _uut.Info(_logline);
            _fakeBackend.Received(1).Info(_logline);
        }

        [Test]
        public void Debug_BackendDebug_GetsCalled()
        {
            _uut.Debug(_logline);
            _fakeBackend.Received(1).Debug(_logline);
        }
    }
}