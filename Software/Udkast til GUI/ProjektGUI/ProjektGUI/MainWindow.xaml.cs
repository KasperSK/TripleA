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

namespace ProjektGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> BeerProducts = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();

            // opretter dummy øl
            Product beer1 = new Product("Ceres top", 15);
            BeerProducts.Add(beer1);
            Product beer2 = new Product("Carlsberg", 15);
            BeerProducts.Add(beer2);
            Product beer3 = new Product("Grimbergen", 25);
            BeerProducts.Add(beer3);



            //Laver wrappanel knapper som skal ind under øl tab item
            WrapPanel ØlPanel = new WrapPanel();




            foreach (Product product in BeerProducts)
            {
                Button button = new Button
                {
                    Content = product.Name_,
                    MinHeight = 40,
                    MinWidth = 50


                };

                ØlPanel.Children.Add(button);
            }

            

            

            


            // Denne linje ødelægger det!
            Øl.Content = ØlPanel;

            

            



        }
    }
}
