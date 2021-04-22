using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.CarName).NotEmpty();
            //RuleFor(p => p.CarName).Must(StarwithA).WithMessage("üRUNLER A Harfi İle Başlamalı");
        }

        private bool StarwithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
