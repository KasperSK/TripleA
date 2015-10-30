using System.Windows.Input;

namespace CashRegister.GUI.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
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

        public string DataSource
        {
            get { return Properties.Settings.Default.DataSource; }
            set
            {
                if (DataSource == value) return;
                Properties.Settings.Default.City = value;
                OnPropertyChanged();
            }
        }

        public string InitialCatalog
        {
            get { return Properties.Settings.Default.InitialCatalog; }
            set
            {
                if (InitialCatalog == value) return;
                Properties.Settings.Default.InitialCatalog = value;
                OnPropertyChanged();
            }
        }


        public string UserId
        {
            get { return Properties.Settings.Default.UserId; }
            set
            {
                if (UserId == value) return;
                Properties.Settings.Default.UserId = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return Properties.Settings.Default.Password; }
            set
            {
                if (Password == value) return;
                Properties.Settings.Default.Password = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
            }
        }

        private ICommand _save;
        public ICommand Save => _save ?? (_save = new RelayCommand(SaveCommand));

        private void SaveCommand()
        {
            Properties.Settings.Default.Save();
        }

        private ICommand _discard;
        public ICommand Discard => _discard ?? (_discard = new RelayCommand(DiscardCommand));

        private void DiscardCommand()
        {
            Properties.Settings.Default.Reload();
        }
    }
}