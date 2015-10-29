namespace CashRegister.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class OrderStatus
    {
        public OrderStatus()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
