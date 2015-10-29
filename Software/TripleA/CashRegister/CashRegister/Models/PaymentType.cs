namespace CashRegister.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class PaymentType
    {
        
        public PaymentType()
        {
        
        }

        public long Id { get; set; }

        public long Description { get; set; }

    }
}
