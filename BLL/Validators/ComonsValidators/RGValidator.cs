using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.ComonsValidators
{
    internal static class RGValidator
    {
        public static IRuleBuilderOptions<PessoaFisica, string> IsRgValid<PessoaFisica>(this IRuleBuilder<PessoaFisica, string> param)
        {
            return param.Must(c => ValidateRg(c));
        }
    }
}
