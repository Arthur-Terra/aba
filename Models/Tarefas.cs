using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDB.Models
{
    public class Tarefas
    {
        [Key]
        public int IdTarefa {get; set;}

        public string Titulo {get; set;}

        public string Descricao {get; set;}

        public DateTime DataCriacao {get; set;} = DateTime.Now;

        public DateTime DataVencimento {get; set;}

        public string Status {get; set;}

        public string Prioridade {get; set;}


        [ForeignKey("Usuario")]
        public int IdUsuario {get; set;}

        public Usuario Usuario {get; set;}


    }
}