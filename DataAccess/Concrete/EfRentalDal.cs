using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RecapProjectContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RecapProjectContext context = new RecapProjectContext())
            {
                var result = from R in context.Rentals
                             join C in context.Cars
                                 on R.CarId equals C.CarId
                             join U in context.Users
                                 on R.UserId equals U.Id
                             select new RentalDetailDto
                             {
                                 RentalId = R.RentalId,
                                 CarName = C.CarName,
                                 FirstName = U.FirstName,
                                 LastName = U.LastName,
                                 RentDate = R.RentDate,
                                 ReturnDate = R.ReturnDate
                             };
                return result.ToList();

            }


        }





    }
}
