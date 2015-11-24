using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CashRegister.GUI.Views
{
    /// <summary>
    /// Interaction logic for NumpadView.xaml
    /// </summary>
    public partial class NumpadView : UserControl
    {
        public NumpadView()
        {
            InitializeComponent();
        }

        private void NumpadView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = (ActualWidth/18);
        }
    }
}
