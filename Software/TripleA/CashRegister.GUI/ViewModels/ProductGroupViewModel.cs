using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Database;

namespace CashRegister.GUI
{
    class ProductGroupViewModel
    {
        private ObservableCollection<ProductGroupModel> _itemList = new ObservableCollection<ProductGroupModel>();

        public ProductGroupViewModel()
        {
            var toAdd = new ProductGroupModel {GroupName = "Grupper"};
            var productAdd = new ProductModel();
            productAdd.ProductNameRowOne = "hash";
            productAdd.Column = 0;
            productAdd.Row = 0;
            toAdd.Products.Add(productAdd);
            productAdd = new ProductModel { ProductNameRowOne = "Peter"};
            productAdd.Column = 0;
            productAdd.Row = 0;
            toAdd.Products.Add(productAdd);
            productAdd = new ProductModel { ProductNameRowOne = "Lærke" };
            productAdd.Column = 1;
            productAdd.Row = 0;
            toAdd.Products.Add(productAdd);
            productAdd = new ProductModel { ProductNameRowOne = "Magnus" };
            productAdd.Column = 2;
            productAdd.Row = 0;
            toAdd.Products.Add(productAdd);
            for (var i = 0; i < 30; i++)
            {
                productAdd = new ProductModel { ProductNameRowOne = "Magnus" + i, ProductNameRowTwo = "Magnus" + i, ProductNameRowThree = "Magnus" + i, ProductNameRowFour = "Magnus" + i, ProductNameRowFive = "Magnus" + i, };
                productAdd.ProductNameRowTwo = "hat" + i;
                productAdd.Column = 2;
                productAdd.Row = 0;
                toAdd.Products.Add(productAdd);
            }
            for (var i = 0; i < 30; i++)
            {
                productAdd = new ProductModel { ProductNameRowTwo = "Magnus" + i };
                productAdd.Column = 2;
                productAdd.Row = 0;
                toAdd.Products.Add(productAdd);
            }
            _itemList.Add(toAdd);
            toAdd = new ProductGroupModel { GroupName = "Øl"};
            _itemList.Add(toAdd);
            toAdd = new ProductGroupModel { GroupName = "Vand" };
            _itemList.Add(toAdd);
        }

         public List<ProductModel> Products
        {
            get { return ItemList[0].Products; }
        }

        public ObservableCollection<ProductGroupModel> ItemList => _itemList;
    }

    class ProductGroupModel
    {
        public string GroupName { get; set; }
        public List<ProductModel> Products { get; set; }


        public ProductGroupModel()
        {
            Products = new List<ProductModel>();
        }

    }

    class ProductModel
    {
        public string ProductNameRowOne { get; set; }
        public string ProductNameRowTwo { get; set; }
        public string ProductNameRowThree { get; set; }
        public string ProductNameRowFour { get; set; }
        public string ProductNameRowFive { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
