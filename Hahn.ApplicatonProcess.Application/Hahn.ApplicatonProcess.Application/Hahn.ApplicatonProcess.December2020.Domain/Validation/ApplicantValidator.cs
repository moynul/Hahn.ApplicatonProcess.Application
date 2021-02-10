using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validation
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the name").MinimumLength(5).WithMessage("Name – at least 5 Characters");
            RuleFor(x => x.FamilyName).NotEmpty().WithMessage("Please enter your family name").MinimumLength(5).WithMessage("FamilyName – at least 5 Characters");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Please enter the adress").MinimumLength(10).WithMessage("Adress – at least 10 Characters");
            RuleFor(x => x.EmailAdress).NotEmpty().WithMessage("enter the Email")
                .EmailAddress().WithMessage("EmailAdress – must be an valid email");
            RuleFor(x => x.Age).InclusiveBetween(20, 60).WithMessage("Age – must be between 20 and 60");
        }
    }

}
