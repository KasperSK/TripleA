using System.Windows.Input;

namespace CashRegister.GUI.ViewModels
{
    public class NumpadViewModel : BaseViewModel
    {
        public NumpadViewModel()
        {
        }

        private string _input;
        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                if (Input == value) return;
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        private ICommand _numpadClicked;
        public ICommand NumpadClicked => _numpadClicked ?? (_numpadClicked = new RelayCommand<string>(NumpadClicked_Command));

        public void NumpadClicked_Command(string num)
        {
            Input += num;
        }
    }
}