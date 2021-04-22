using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    class RentManager : IRentService
    {
        IRentDal _rentDal;

        public RentManager(IRentDal rentDal)
        {
            _rentDal = rentDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Rent rent)
        {
            _rentDal.Add(rent);
            return new SuccessResult("Araba Kiralandı");
        }

        public IDataResult<List<Rent>> GetAll()
        {
            return new SuccessDataResult<List<Rent>>(_rentDal.GetAll());
        }
    }
}
