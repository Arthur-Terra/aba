using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDB.Models.DTOs
{
    public class TarefaCreateDTO
    {
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public string nomeUsuario { get; set; }
    }
}