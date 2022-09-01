using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PessoaFisica
    {
        public string CPF { get; set; }
        public string RG { get; set; }
        public PessoaFisica(string cPF, string rG)
        {
            CPF = cPF;
            RG = rG;
        }
        public PessoaFisica()
        {

        }
    }
}
