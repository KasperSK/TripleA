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
using System.Windows.Shapes;

namespace CashRegister.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for BalanceDialog.xaml
    /// </summary>
    public partial class BalanceDialog : Window
    {
        
        public BalanceDialog(string balance)
        {
            InitializeComponent();


            string[] splitted = balance.Split('\n');



            CashBlock.Text = splitted[0] + " kr.";
            TotalBlock.Text = splitted[1] + " kr.";    //There are two \n in the making of the string
            CashPayBlock.Text = splitted[3] + " kr.";
            CardPayBlock.Text = splitted[4] + " kr.";
            MobilePayBlock.Text = splitted[5] + " kr.";



        }
    }
}
