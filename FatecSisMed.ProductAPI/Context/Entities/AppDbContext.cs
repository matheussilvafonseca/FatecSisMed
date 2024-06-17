using FatecSisMed.MedicoAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Context.Entities;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	// fazemos o mapeamento do objeto relacional
	// do nosso BD

	public DbSet<Convenio> Convenios { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Remedio> Remedios { get; set; }
    public DbSet<Marca> Marcas { get; set; }

    // usamos a fluent API e nao os Data Annotations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Convenio>().HasKey(c => c.Id);
        modelBuilder.Entity<Convenio>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
        
        modelBuilder.Entity<Especialidade>().HasKey(e => e.Id);
        modelBuilder.Entity<Especialidade>().Property(e => e.Nome).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Medico>().HasKey(m => m.Id);
        modelBuilder.Entity<Medico>().Property(m => m.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Medico>().Property(m => m.Email).HasMaxLength(100);
        modelBuilder.Entity<Medico>().Property(m => m.Telefone).HasMaxLength(20);
        modelBuilder.Entity<Medico>().Property(m => m.Endereco).HasMaxLength(100);

        modelBuilder.Entity<Remedio>().HasKey(r => r.Id);
        modelBuilder.Entity<Remedio>().Property(r => r.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Remedio>().Property(r => r.Preco).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Marca>().HasKey(ma => ma.Id);
        modelBuilder.Entity<Marca>().Property(ma => ma.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Marca>().Property(ma => ma.Observacao).HasMaxLength(100).IsRequired();

        // relacionamento
        modelBuilder.Entity<Convenio>()
            .HasMany(c => c.Medicos).WithOne(m => m.Convenio)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Especialidade>()
            .HasMany(e => e.Medicos).WithOne(m => m.Especialidade)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Marca>()
            .HasMany(ma => ma.Remedios).WithOne(ma => ma.Marca)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        // Populando o BD com os primeiros
        // convenios, especialidades e medicos
        modelBuilder.Entity<Convenio>().HasData(
            new Convenio
            {
                Id = 1,
                Nome = "Ben Saude"
            },
            new Convenio
            {
                Id = 2,
                Nome = "Unimed"
            });

        modelBuilder.Entity<Especialidade>().HasData(
            new Especialidade
            {
                Id = 1,
                Nome = "Ortopedista"
            },
            new Especialidade
            {
                Id = 2,
                Nome = "Clínico Geral"
            });

        modelBuilder.Entity<Medico>().HasData(
           new Medico
           {
               Id = 1,
               Nome = "Carlos",
               CRM = 123456,
               Email = "carlos@medico.com",
               Telefone = "17-12345678",
               Endereco = "Av Joao Amadeu, 123, Jales",
               ConvenioId = 1,
               EspecialidadeId = 1
           });

        modelBuilder.Entity<Medico>().HasData(
           new Medico
           {
               Id = 2,
               Nome = "Filisbino",
               CRM = 234567,
               Email = "filisbino@medico.com",
               Telefone = "17-12345678",
               Endereco = "Av Joao Amadeu, 125, Jales",
               ConvenioId = 2,
               EspecialidadeId = 2
           });
        modelBuilder.Entity<Marca>().HasData(
           new Marca
           {
               Id = 1,
               Nome = "Germed",
               Observacao = "Essa marca é Germed de todas"
           },
           new Marca
           {
               Id = 2,
               Nome = "GeoLab",
               Observacao = "Essa marca é mais Geolab de todas"
           },
           new Marca
           {
               Id = 3,
               Nome = "Solaris",
               Observacao = "Essa marca é a mais Solaris de todas"
           });

        modelBuilder.Entity<Remedio>().HasData(
           new Remedio
           {
               Id = 1,
               Nome = "Alprazolam",
               Preco = 15.00,
               MarcaId = 1,
           },
           new Remedio
           {
            Id = 2,
               Nome = "Rivotril",
               Preco = 20.00,
               MarcaId = 2,
           },
           new Remedio
           {
               Id = 3,
               Nome = "Carbamazepina",
               Preco = 40.00,
               MarcaId = 3
           });

    }
}

