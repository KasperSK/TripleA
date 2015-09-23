namespace CashRegister.Products
{
    public interface IProductDao
    {
        int Create(Product product);
        Product Read(int pk);
        void Update(Product product);
        void Delete(Product product);
    }
}