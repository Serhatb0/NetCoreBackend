using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService 
    {
        IDataResult<List<Rental>> GetAll();
        IResult GetAllByCarId(int carId);
        IDataResult<List<Rental>> GetAllByCustomerId(int customerId);
        IDataResult<List<RentalDetailDto>> GetAllRentalDto();
        IResult Add(Rental rental);

        IResult Update(Rental rental);

        IResult Delete(Rental rental);
    }
}
