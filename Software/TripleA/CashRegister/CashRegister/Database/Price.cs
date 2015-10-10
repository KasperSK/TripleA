namespace CashRegister.CashRegister.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        public long PriceId { get; set; }

        [Column("Price")]
        public double Price1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
