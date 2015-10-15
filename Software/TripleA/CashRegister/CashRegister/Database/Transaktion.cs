namespace CashRegister.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaktion")]
    public partial class Transaktion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TransaktionId { get; set; }

        public DateTime TransaktionDate { get; set; }

        public float TransaktionPrice { get; set; }

        public long OrderId { get; set; }

        public long PaymentTypeId { get; set; }

        public virtual OrderList OrderList { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
