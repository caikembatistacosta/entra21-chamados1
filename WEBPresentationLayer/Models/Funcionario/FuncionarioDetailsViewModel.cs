using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioDetailsViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Genero Genero { get; set; }
        public NivelDeAcesso NivelDeAcesso { get; set; }
        public bool IsAtivo { get; set; }

    }


}
