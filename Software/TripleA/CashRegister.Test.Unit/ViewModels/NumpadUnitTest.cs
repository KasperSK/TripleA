using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.GUI.Annotations;
using CashRegister.GUI.ViewModels;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;

public interface INotifyTest
{
    void TestINotify(object obj, PropertyChangedEventArgs e);
}

namespace CashRegister.Test.Unit.ViewModels
{
    [TestFixture]
    internal class NumpadUnitTest
    {
        private NumpadViewModel _uut;
        private INotifyTest _fakeNotifyTest;

        [SetUp]
        public void SetUp()
        {
            _uut = new NumpadViewModel();
            _fakeNotifyTest = Substitute.For<INotifyTest>();
        }

        [Test]
        public void ClearNumpad_CallNumpad__NumpadClear_CommandIsCalled__InputIsempty()
        {
            _uut.ClearNumpad();

            Assert.That(_uut.Input, Is.EqualTo(""));
        }

        [Test]
        public void numpadClear__CallWhen_NumpadClearIsNull__NumpadReturnIsNotNull()
        {
            var temp = _uut.NumpadClear;

            Assert.NotNull(temp);
        }


        [Test]
        public void NumpadClicked__numIs7_InputIs7()
        {
            var NumpadClicked = _uut.NumpadClicked;

            NumpadClicked.Execute("7");

            Assert.That(_uut.Input, Is.EqualTo("7"));
        }

        [Test]
        public void NumpadClicked__numIs6and7_InputIs67()
        {
            var NumpadClicked = _uut.NumpadClicked;

            NumpadClicked.Execute("6");
            NumpadClicked.Execute("7");

            Assert.That(_uut.Input, Is.EqualTo("67"));
        }

        [Test]
        public void Input_SetInput_OnPropertyChangedIsCalled()
        {
            _uut.PropertyChanged += _fakeNotifyTest.TestINotify;

            _uut.Input = "7";

            _fakeNotifyTest.Received().TestINotify(Arg.Any<object>(), Arg.Any<PropertyChangedEventArgs>());
        }

        [Test]
        public void Input_InputSetTo7Twice_InputIs7()
        {
            _uut.Input = "7";
            var temp = _uut.Input = "7";

            Assert.That(temp, Is.EqualTo("7"));
        }

        [Test]
        public void Amount_InputIsNotSet_AmountIs1()
        {
            Assert.That(_uut.Amount, Is.EqualTo(1));
        }


        [Test]
        public void Amount_InputIs7_AmountIs7()
        {
            _uut.Input = "7";
            Assert.That(_uut.Amount, Is.EqualTo(7));
        }
    }
}
