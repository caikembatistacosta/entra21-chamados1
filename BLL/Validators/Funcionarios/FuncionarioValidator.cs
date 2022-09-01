using Entities;
using BLL.Validators.ComonsValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Constants;

namespace BLL.Validators.Funcionarios
{
    internal class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public void ValidateID()
        {
            RuleFor(c => c.Id).NotNull().WithMessage(GenericConstants.MENSAGEM_ERRO_ID_VAZIO);
        }
        public void ValdiateNome()
        {

        }
        public void ValidateUsername()
        {

        }
        public void ValidateSenha()
        {

        }
        public void ValidateEmail()
        {

        }
        public void ValidateDataNascimento()
        {

        }
        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).IsCpfValid();
        }
        
    }
}
