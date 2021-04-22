using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailDto>> GetDetailCarDto();
        IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max);
        IResult Add(Car car);
        IResult Update(Car car);

        IDataResult<List<Car>> GetById(int BrandId);

        IResult AddTransactionalTest(Car car);

    }
}
