using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.products.commands.insertProductCommand
{
    public class InsertProductCommandValidator :  AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            
            RuleFor(p => p.StatusName)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(8).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Description)
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
