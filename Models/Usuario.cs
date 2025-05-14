using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjetoDB.Models
{
    public class Usuario
    {
        [Key]
        public int Id {get; set;}

        [Required (ErrorMessage ="Nome é um campo obrigatorio")]
        [MaxLength(150 ,ErrorMessage = "Nome pode ter no maximo 150 caracteres")]
        public string Nome {get;set;}

        [Required (ErrorMessage ="Email é um campo obrigatorio")]
        [MaxLength(40 ,ErrorMessage = "Email pode ter no maximo 40 caracteres")]
        public string Email {get;set;}
        
        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        public string Senha {get;set;}
        
    }
}