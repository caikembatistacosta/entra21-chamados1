using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Chamados
{
    internal class ChamadoInsertValidator : ChamadoValidator
    {
        public ChamadoInsertValidator()
        {
            base.ValidateNome();
            base.ValidateDescricaoCurta();
            base.ValidateDescricaoDetalhada();
        }
    }
}
