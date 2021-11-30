using HexaSamples.Commons.Entities;

namespace HexaSamples.Domain.SupportEntities;

public class Pessoa : Entity<Guid>
{
    public Pessoa(Guid id, string cpf, string nome)
    {
        Id = id;
        Cpf = cpf;
        Nome = nome;
    }

    protected Pessoa() { }
    
    public string Cpf { get; }

    public string Nome { get; }
}