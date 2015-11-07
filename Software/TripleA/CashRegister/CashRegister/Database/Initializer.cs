using System.Data.Entity;

namespace CashRegister.Database
{
    public class CashRegisterInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        protected override void Seed(CashRegisterContext context)
        {
            // seed database here
        }
    }
}
