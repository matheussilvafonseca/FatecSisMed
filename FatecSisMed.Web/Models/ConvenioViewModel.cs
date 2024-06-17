using System;
using System.ComponentModel.DataAnnotations;

namespace FatecSisMed.Web.Models
{
	public class ConvenioViewModel
	{
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
    }
}

