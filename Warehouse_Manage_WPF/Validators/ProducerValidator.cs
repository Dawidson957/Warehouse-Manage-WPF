using FluentValidation;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.Validators
{
    public class ProducerValidator : AbstractValidator<ProducerModel>
    {
        public ProducerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(64);

            RuleFor(x => x.URL)
                .MaximumLength(512);
        }
    }
}
