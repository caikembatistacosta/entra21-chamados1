using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Cliente
{
    internal class ClienteInsertValidator:ClienteValidator
    {
        public ClienteInsertValidator()
        {
            base.ValidateCPF();
            base.ValidateEmail();
            base.ValidateNome();
        }
    }
}
