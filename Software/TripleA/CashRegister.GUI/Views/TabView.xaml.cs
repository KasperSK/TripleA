using System.Windows;
using System.Windows.Controls;

namespace CashRegister.GUI.Views
{
    /// <summary>
    /// Interaction logic for TabView.xaml
    /// </summary>
    public partial class TabView : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TabView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Changes the font size when window is resized.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments sent with the event.</param>        
        private void TabView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = (ActualWidth/40);
        }
    }
}
