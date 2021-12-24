using HexaSamples.SeedWork.Entities;

namespace HexaSamples.Domain.SupportEntities;

public class Loja : Entity<Guid, int>
{
    public Loja(Guid id, int codigo, string razaoSocial, string nomeFantasia, string cnpj)
    {
        Id = id;
        Codigo = codigo;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
    }

    protected Loja() { }
    
    public string RazaoSocial { get;  }

    public string NomeFantasia { get; }

    public string Cnpj { get; }
}