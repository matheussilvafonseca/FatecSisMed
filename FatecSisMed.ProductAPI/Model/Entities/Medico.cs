namespace FatecSisMed.MedicoAPI.Model.Entities;

public class Medico
{
	public int Id { get; set; }
	public string? Nome { get; set; }
	public int CRM  { get; set; }
	public string? Email { get; set; }
	public string? Telefone { get; set; }
	public string? Endereco { get; set; }

    public Convenio? Convenio { get; set; }
	public int ConvenioId { get; set; }

    public Especialidade? Especialidade { get; set; }
    public int EspecialidadeId { get; set; }
}

