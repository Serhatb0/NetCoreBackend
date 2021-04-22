using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RecapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetRentalDetails()
        {
            using (RecapProjectContext context = new RecapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on
                                 c.BrandId equals b.BrandId
                             join color in context.Colors
                                 on c.ColorId equals color.ColorId
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 BrandName = b.BrandName,
                                 ColorName = color.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 CarName = c.CarName,
                             };
                return result.ToList();
            }
        }
    }

}
