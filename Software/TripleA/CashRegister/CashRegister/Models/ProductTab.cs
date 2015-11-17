using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to determine which products goes on which tab
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductTab
	{
		/// <summary>
		/// Uniqe Id
		/// </summary>
		public virtual int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Name of the Tab
		/// </summary>
		public virtual string Name
		{
			get;
			set;
		}

		/// <summary>
		/// In what order is this displayed
		/// </summary>
		public virtual int Priority
		{
			get;
			set;
		}

        /// <summary>
		/// Is it currently active
		/// </summary>
		public virtual bool Active
        {
            get;
            set;
        }

        /// <summary>
		/// Color of the tab
		/// </summary>
		public virtual string Color
        {
            get;
            set;
        }


        public virtual ICollection<ProductGroup> ProductGroups
		{
			get;
			set;
		}

	}
}

