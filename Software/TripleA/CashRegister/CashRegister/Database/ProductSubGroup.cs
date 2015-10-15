namespace CashRegister.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductSubGroup")]
    public partial class ProductSubGroup
    {
        public long ProductSubGroupId { get; set; }

        [Required]
        public string SubGroupName { get; set; }

        public long? ProductGroupId { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}
