using System.ComponentModel;
using System.Runtime.CompilerServices;
using CashRegister.GUI.Annotations;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// An abstract class that implements a PropertyChangedEventHandler
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Contains a PropertyChangedEventHandler 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A method to invoke an PropertyChangedEventHandler.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}