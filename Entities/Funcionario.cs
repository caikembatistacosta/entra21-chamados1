using Entities.Enums;

namespace Entities
{
    public class Funcionario : PessoaFisica
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public NivelDeAcesso NivelDeAcesso { get; set; }
    }
}