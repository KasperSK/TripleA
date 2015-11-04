namespace CashRegister.Payment
{
    /// <summary>
	/// Contians Description of the Paymentprovidor and is the interface used outside this namespace
	/// </summary>
	public interface IPaymentProvidorDescriptor 
	{
		/// <summary>
		/// The PaymentProviders unique ID
		/// </summary>
		int ID { get; }

		/// <summary>
		/// The PaymentProviders Name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// A text discription of the PaymentProvider
		/// </summary>
		string Description { get; }
	}
}

