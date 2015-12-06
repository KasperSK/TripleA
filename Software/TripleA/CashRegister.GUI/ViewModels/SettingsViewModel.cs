using System.Windows.Input;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// The ViewModel for the SettingsView.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Contains the name of the business.
        /// </summary>
        public string Name
        {
            get { return Properties.Settings.Default.Name; }
            set
            {
                if (Name == value) return;
                Properties.Settings.Default.Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the address of the business.
        /// </summary>
        public string Address
        {
            get { return Properties.Settings.Default.Address; }
            set
            {
                if (Address == value) return;
                Properties.Settings.Default.Address = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the postal code of the business.
        /// </summary>
        public string Postal
        {
            get { return Properties.Settings.Default.Postal; }
            set
            {
                if (Postal == value) return;
                Properties.Settings.Default.Postal = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the city of the business.
        /// </summary>
        public string City
        {
            get { return Properties.Settings.Default.City; }
            set
            {
                if (City == value) return;
                Properties.Settings.Default.City = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the ConnectionString for the Database.
        /// </summary>
        public string ConnectionString
        {
            get { return Properties.Settings.Default.ConnectionString; }
            set
            {
                if (ConnectionString == value) return;
                Properties.Settings.Default.ConnectionString = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsViewModel()
        {
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
            }
        }

        /// <summary>
        /// Contains an ICommand implementation for Save.
        /// </summary>
        private ICommand _save;
        
        /// <summary>
        /// Contains a Command to Invoke a Save of the settings.
        /// </summary>
        public ICommand Save => _save ?? (_save = new RelayCommand(SaveCommand));

        /// <summary>
        /// The logic for the ICommand Save.
        /// </summary>
        private void SaveCommand()
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Contains an ICommand implementation for Discard.
        /// </summary>
        private ICommand _discard;

        /// <summary>
        /// Contains an ICommand to Invoke a Discard of the settings.
        /// </summary>
        public ICommand Discard => _discard ?? (_discard = new RelayCommand(DiscardCommand));

        /// <summary>
        /// The logic for the ICommand Discard.
        /// </summary>
        private void DiscardCommand()
        {
            Properties.Settings.Default.Reload();
        }
    }
}