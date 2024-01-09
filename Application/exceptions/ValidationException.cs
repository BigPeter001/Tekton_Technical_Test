using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get;}
        public ValidationException() : base("Se han producido uno mas erores de validación")
        { 
               Errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures) 
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
