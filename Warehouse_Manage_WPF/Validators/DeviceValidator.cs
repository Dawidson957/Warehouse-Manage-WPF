using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.Validators
{
    public class DeviceValidator : AbstractValidator<DeviceModel>
    {
        public DeviceValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(255);

            RuleFor(x => x.ArticleNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(64);

            RuleFor(x => x.Location)
                .MaximumLength(4);

            RuleFor(x => x.Quantity)
                .LessThan(1000000);

            RuleFor(x => x.ProducerName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(64);
        }
    }
}
