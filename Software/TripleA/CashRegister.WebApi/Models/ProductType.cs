﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Model for ProductTypes in Entity Framework
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductType
    {
        /// <summary>
        /// Initializes list
        /// </summary>
        public ProductType()
        {
            ProductGroups = new List<ProductGroup>();
        }

        /// <summary>
        ///     Uniqe Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Name of the Type
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Price of the Type
        /// </summary>
        public virtual int Price { get; set; }

        /// <summary>
        ///     Color associated with the group
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        ///     Groups of products to match this
        /// </summary>
        public virtual ICollection<ProductGroup> ProductGroups { get; }
    }

    /// <summary>
    /// Data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductTypeDto
    {
        /// <summary>
        ///     Uniqe Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Name of the Type
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Detailed data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductTypeDetailsDto
    {
        /// <summary>
        ///     Uniqe Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Name of the Type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Price of the Type
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        ///     Color associated with the group
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///     List of ProductGroup Ids
        /// </summary>
        public List<long> ProductGroups { get; set; }
    }
}