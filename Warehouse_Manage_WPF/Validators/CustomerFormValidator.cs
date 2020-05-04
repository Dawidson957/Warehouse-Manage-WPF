using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.Validators
{
    public class CustomerFormValidator : AbstractValidator<CustomerModel>
    {
        public CustomerFormValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(x => x.Address)
                .MinimumLength(5)
                .MaximumLength(128);

            RuleFor(x => x.City)
                .MinimumLength(2)
                .MaximumLength(64);
        }
    }
}
