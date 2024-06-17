namespace FatecSisMed.MedicoAPI.Model.Entities;

public class Convenio
{
	public int Id { get; set; }
	public string? Nome { get; set; }

	public ICollection<Medico>? Medicos { get; set; }
}

