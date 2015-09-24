namespace ProjektGUI
{
    public class Product
    {
        public string Name_ { get;}

        public uint Price_ { get; }

        public Product(string name, uint price)
        {
            Name_ = name;

            Price_ = price;
        }
    }
}