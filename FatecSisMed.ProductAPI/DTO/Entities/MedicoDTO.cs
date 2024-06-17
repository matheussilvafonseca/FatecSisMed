using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FatecSisMed.MedicoAPI.DTO.Entities;

	public class MedicoDTO
	{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O CRM é obrigatório!")]
    public int CRM { get; set; }
    public string? Email { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório!")]
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }

    [JsonIgnore]
    public EspecialidadeDTO? EspecialidadeDTO { get; set; }
    public int EspecialidadeId { get; set; }

    [JsonIgnore]
    public ConvenioDTO? ConvenioDTO { get; set; }
    public int ConvenioId { get; set; }

}

