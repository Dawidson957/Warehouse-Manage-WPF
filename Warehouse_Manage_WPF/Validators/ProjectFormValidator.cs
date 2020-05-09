using FluentValidation;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.Validators
{
    public class ProjectFormValidator : AbstractValidator<ProjectModel>
    {
        public ProjectFormValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(x => x.Status)
                .MaximumLength(64);

            RuleFor(x => x.Comment)
                .MaximumLength(512);
        }
    }
}
