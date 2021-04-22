﻿using Core.Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Rental : IEntity
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
