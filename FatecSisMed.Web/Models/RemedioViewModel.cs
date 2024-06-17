using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FatecSisMed.Web.Models
{
    public class RemedioViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public double? Preco { get; set; }
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }
        [Display(Name = "Marca")]
        public string? NomeMarca { get; set; }
    }
}
