﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CashRegister.Orders
{
    using CashRegister.Database;
    // using CashRegister.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OrderDao : IOrderDao
    {

        /// <summary>
        /// Being able to delete an order from the database
        /// </summary>
        public virtual void Delete(Order order)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update an order in the database
        /// </summary>
        public virtual void Update(Order order)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Being able to insert an order to the database
        /// </summary>
        public virtual void Insert(Order order)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Selecting an order by ID in the database
        /// </summary>
        public virtual Order SelectByID(int id)
        {
            throw new NotImplementedException();

        }

        // Get the last N IDs
        public virtual List<int> GetNLastID(int id)
        {
            throw new NotImplementedException();
        }
        //Get the lastest ID
        public virtual int GetLastestID()
        {
            throw new NotImplementedException();
        }

        //Gets a new ID for creating a new order

        public virtual int GetNewID()
        {
            throw new NotImplementedException();
        }

    }
}

