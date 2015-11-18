using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
	/// Contians Description of the Paymentprovider and is the interface used outside this namespace
	/// </summary>
	public interface IPaymentProviderDescriptor 
	{
        /// <summary>
        /// The PaymentProviders unique Type
        /// </summary>
        PaymentType Type { get; }

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

