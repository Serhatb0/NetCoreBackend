using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICustomerService 
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<Customer>> GetAllByUserId(int id);

        IResult Add(Customer customer);

        IResult Update(Customer customer);

        IResult Delete(Customer customer);
    }
}
