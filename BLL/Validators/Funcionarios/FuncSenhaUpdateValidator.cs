﻿using BLL.Validators.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Funcionarios
{
    internal class FuncSenhaUpdateValidator : FuncionarioValidator
    {
        public FuncSenhaUpdateValidator()
        {
            base.ValidateSenha();

        }
    }
}