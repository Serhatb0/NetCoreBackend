using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; } // USERID
        public string CompanyName { get; set; }
       
    }
}
