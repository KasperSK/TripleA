using System;

namespace CashRegister.WebApi.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// Auto generated
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public string Name { get; set; }
    }
}