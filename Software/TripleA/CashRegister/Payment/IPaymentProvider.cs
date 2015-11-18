namespace CashRegister.Payment
{
    /// <summary>
	/// Interface for the different paymenttypes
	/// </summary>
	public interface IPaymentProvider  : IPaymentProviderDescriptor
	{
        /// <summary>
        /// Initialiser for the payment types
        /// </summary>
        void Init();

        /// <summary>
        /// Transfor the price amount
        /// </summary>
        bool TransferAmount(int amount, string description);

        /// <summary>
        /// Writes the transaktionstatus
        /// </summary>
        bool TransactionStatus();

        /// <summary>
        /// Restart the payment system
        /// </summary>
        void Restart();

        /// <summary>
        /// Shuts down the payment system
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Balance the payment
        /// </summary>
        int Tally();
    }
}