using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Cliente
{
    internal class ClienteValidator : AbstractValidator<Entities.Cliente>
    {
        public void ValidateID()
        {
            RuleFor(c => c.ID).NotNull().WithMessage("ID deve ser informado.");
        }
        public void ValidateNome()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage("Nome deve ser informado.")
                                .MinimumLength(3).WithMessage("Pet deve conter ao menos 3 caracteres.")
                                .MaximumLength(30).WithMessage("Pet não pode conter mais de 30 caracteres.");
        }
        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).NotNull().WithMessage("CPF deve ser informado.")
                               .MinimumLength(3).WithMessage("CPF deve conter ao menos 3 caracteres.")
                               .MaximumLength(20).WithMessage("CPF não pode conter mais de 20 caracteres.")
                               .IsCpfValid().WithMessage("CPF inválido");
        }

        public void ValidateEmail()
        {
            RuleFor(c => c.Email).NotNull().WithMessage("Email deve ser informado")
                                 .MinimumLength(3).WithMessage("Email deve conter ao menos 3 caracteres.")
                                 .MaximumLength(20).WithMessage("Email não pode conter mais de 20 caracteres.")
                                 .EmailAddress().WithMessage("Email inválido");
        }
    }
}
