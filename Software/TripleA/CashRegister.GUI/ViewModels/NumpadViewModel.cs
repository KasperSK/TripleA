using System.Windows.Input;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// An interface to the NumpadViewModel
    /// </summary>
    public interface INumpad
    {
        void ClearNumpad();
        int Amount { get; }
    }

    /// <summary>
    /// The ViewModel for the NumpadView
    /// </summary>
    public class NumpadViewModel : BaseViewModel, INumpad
    {
        /// <summary>
        /// Contains a string that represents the Input.
        /// </summary>
        private string _input;

        /// <summary>
        /// A Input property that notifies when changes are made.
        /// </summary>
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

        /// <summary>
        /// Contains an ICommand implementation for NumpadClicked.
        /// </summary>
        private ICommand _numpadClicked;

        /// <summary>
        /// An ICommand property that returns an implementation to Invoke a NumpadClicked.
        /// </summary>
        public ICommand NumpadClicked => _numpadClicked ?? (_numpadClicked = new RelayCommand<string>(NumpadClicked_Command));


        /// <summary>
        /// The logic for NumpadClicked.
        /// </summary>
        /// <param name="num"></param>
        private void NumpadClicked_Command(string num)
        {
            if (num == "-" && Input == "-")
            {
                Input = "-";
            }
            else
            {
                Input += num;
            }
        }

        /// <summary>
        /// Contains an ICommand implementation for NumpadClear.
        /// </summary>
        private ICommand _numpadClear;

        /// <summary>
        /// An ICommand property that returns an implementation to Invoke a NumpadClear.
        /// </summary>
        public ICommand NumpadClear => _numpadClear ?? (_numpadClear = new RelayCommand(NumpadClear_Command));

        /// <summary>
        /// The logic for NumpadClear.
        /// </summary>
        private void NumpadClear_Command()
        {
            Input = "";
        }

        /// <summary>
        /// Clears the numpad.
        /// </summary>
        public void ClearNumpad()
        {
            NumpadClear_Command();
        }
        
        /// <summary>
        /// Contains the input amount as an integer.
        /// </summary>
        public int Amount
        {
            get
            {
                if (Input == "-")
                {
                    Input = "-1";
                }

                int returnvalue;
                int.TryParse(Input, out returnvalue);
                return returnvalue == 0 ? 1 : returnvalue;
            }
        }
    }
}