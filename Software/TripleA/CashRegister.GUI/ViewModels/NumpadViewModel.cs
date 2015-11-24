using System.Globalization;
using System.Windows.Input;

namespace CashRegister.GUI.ViewModels
{
    public interface INumpad
    {
        void ClearNumpad();
        int Amount { get; }
    }

    public class NumpadViewModel : BaseViewModel, INumpad
    {
        private string _input;
        public string Input
        {
            get { return _input; }
            set
            {
                if (Input == value) return;
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        private ICommand _numpadClicked;
        public ICommand NumpadClicked => _numpadClicked ?? (_numpadClicked = new RelayCommand<string>(NumpadClicked_Command));

        private void NumpadClicked_Command(string num)
        {
            Input += num;
        }

        private ICommand _numpadClear;
        public ICommand NumpadClear => _numpadClear ?? (_numpadClear = new RelayCommand(NumpadClear_Command));

        private void NumpadClear_Command()
        {
            Input = "";
        }

        public void ClearNumpad()
        {
            NumpadClear_Command();
        }

        public int Amount
        {
            get
            {
                int returnvalue;
                int.TryParse(Input, out returnvalue);
                return returnvalue == 0 ? 1 : returnvalue;
            }
        }

        
    }
}