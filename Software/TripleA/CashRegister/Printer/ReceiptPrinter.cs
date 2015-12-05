using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CashRegister.Printer
{
    /// <summary>
    /// Implementation of a receipt printer.
    /// Takes lines of string and prints the strings.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ReceiptPrinter : IPrinter
    {
        /// <summary>
        /// A formatting helper for the FlowDocument.
        /// </summary>
        private Paragraph _paragraph;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReceiptPrinter()
        {
            _paragraph = new Paragraph();
        }

        /// <summary>
        /// Adds a string to the print document.
        /// </summary>
        /// <param name="line">line is added to the print document.</param>
        public void AddLine(string line)
        {
            _paragraph.Inlines.Add(new Run(line));
        }

        /// <summary>
        /// Initiates a print to the default printer from Windows printer dialog.
        /// </summary>
        public void Print()
        {
            var flowDocument = new FlowDocument
            {
                MaxPageWidth = 384,
                FontFamily = new FontFamily("Courier New")
            };

            flowDocument.Blocks.Add(_paragraph);

            var printDlg = new PrintDialog();
            IDocumentPaginatorSource idpSource = flowDocument;

            printDlg.PrintDocument(idpSource.DocumentPaginator, "Reciept");

            _paragraph = new Paragraph();
        }
    }
}