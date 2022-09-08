﻿using System.ComponentModel.DataAnnotations;

namespace WEBApi.Models.Chamado
{
    public class ChamadoDetailsViewModel
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        public string Nome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }
    }
}
