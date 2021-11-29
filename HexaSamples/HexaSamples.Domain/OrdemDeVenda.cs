using HexaSamples.Commons.Entities;

namespace HexaSamples.Domain
{
    public class OrdemDeVenda : AggregateRoot<Guid>
    {

    }

    public class ItemVenda : Entity<long>
    {
        public Produto Produto { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }
    }

    public class Produto : Entity<Guid, string>
    {
        public string Descricao { get; private set; }

        public virtual ICollection<PrecoVenda> PrecoVenda { get; private set; }


    }

    public class Loja : Entity<Guid, int>
    {
        public string RazaoSocial { get; private set; }

        public string NomeFantasia { get; private set; }

        public string Cnpj { get; private set; }
    }

    public class PrecoVenda : Entity<Guid>
    {
        public Loja Loja { get; private set; }

        public decimal Valor { get; private set; }
    }

    public class Pessoa : Entity<Guid>
    {

    }
}