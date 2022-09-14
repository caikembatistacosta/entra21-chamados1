﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Demandas
{
    internal class DemandaUpdateValidator : DemandaValidator
    {
        public DemandaUpdateValidator()
        {
            base.ValidateNome();
            base.ValidateDescricaoCurta();
            base.ValidateDescricaoDetalhada();
        }
    }
}