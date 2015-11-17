﻿using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CashRegister.Printer
{
    /// <summary>
    /// Implementation of a receipt printer
    /// </summary>
    public class ReceiptPrinter : IPrinter
    {
        private FlowDocument _flowDocument;
        private Paragraph _paragraph;

        public ReceiptPrinter()
        {
            _flowDocument = new FlowDocument();
            _paragraph = new Paragraph();

            // FlowDocument settings
            _flowDocument.MaxPageWidth = 384;
            _flowDocument.FontFamily = new FontFamily("Courier New");
        }

        /// <summary>
        /// Adds a string to a print document
        /// </summary>
        /// <param name="str">str is added to the print document</param>
        public virtual void AddTo(string str)
        {
            _paragraph.Inlines.Add(new Run(str));
        }

        /// <summary>
        /// Initiates a print to the reciept printer from the default Windows printer dialog
        /// </summary>
        public virtual void Print()
        {
            var printDlg = new PrintDialog();

            IDocumentPaginatorSource idpSource = _flowDocument;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Reciept");
        }
    }
}