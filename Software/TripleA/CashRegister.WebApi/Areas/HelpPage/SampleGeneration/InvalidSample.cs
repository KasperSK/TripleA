using System;

namespace CashRegister.WebApi.Areas.HelpPage
{
    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
    /// </summary>
    public class InvalidSample
    {
        /// <summary>
        /// Auto generated
        /// </summary>
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Auto generated
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Auto generated
        /// </summary>
        public override bool Equals(object obj)
        {
            InvalidSample other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <summary>
        /// Auto generated
        /// </summary>
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <summary>
        /// Auto generated
        /// </summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}