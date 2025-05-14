using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDB.Models
{
    public class Graficos
    {
        public int idUsuario {get;set;}

        public int TarefasTotal {get;set;}

        public int TarefasPendentes {get;set;}

        public int TarefasConcluidas {get;set;}

        public int TarefasAtrasadas {get;set;}

        public UsuarioDTO Usuario {get;set;}
        
    }
}