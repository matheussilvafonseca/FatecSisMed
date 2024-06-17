using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FatecSisMed.Web.Models
{
	public class MedicoViewModel
	{
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        public int CRM { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }

        [Required]
        [Display(Name = "Especialidade")]
        public int EspecialidadeId { get; set; }
        [Display(Name = "Especialidade")]
        public string? EspecialidadeNome { get; set; }

        [Required]
        [Display(Name = "Convenio")]
        public int ConvenioId { get; set; }
        [Display(Name = "Convenio")]
        public string? ConvenioNome { get; set; }

    }
}

