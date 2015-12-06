using System.Windows;
using System.Windows.Controls;

namespace CashRegister.GUI.Views
{
    /// <summary>
    /// Interaction logic for NumpadView.xaml
    /// </summary>
    public partial class NumpadView : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NumpadView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Changes the font size when window is resized.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments sent with the event.</param>
        private void NumpadView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = (ActualWidth/18);
        }
    }
}
