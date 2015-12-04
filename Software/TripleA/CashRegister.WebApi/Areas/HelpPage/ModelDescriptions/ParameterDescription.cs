using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CashRegister.WebApi.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Auto generated
    /// </summary>
    public class ParameterDescription
    {
        /// <summary>
        /// Auto generated
        /// </summary>
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// <summary>
        /// Auto generated
        /// </summary>
        public Collection<ParameterAnnotation> Annotations { get; private set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public ModelDescription TypeDescription { get; set; }
    }
}