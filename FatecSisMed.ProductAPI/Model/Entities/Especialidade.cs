
namespace FatecSisMed.MedicoAPI.Model.Entities;

public class Especialidade
{
	public int Id { get; set; }
	public string? Nome { get; set; }

	public ICollection<Medico>? Medicos { get; set; }
}

