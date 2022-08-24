using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Chamados
{
    internal class ChamadoUpdateValidator : ChamadoValidator
    {
        public ChamadoUpdateValidator()
        {
            base.ValidateNome();
            base.ValidateDescricaoCurta();
            base.ValidateDescricaoDetalhada();
        }
    }
}
