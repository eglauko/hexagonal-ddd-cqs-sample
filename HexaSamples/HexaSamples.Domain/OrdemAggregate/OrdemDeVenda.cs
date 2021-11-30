using HexaSamples.Commons.Entities;
using HexaSamples.Commons.Results;

namespace HexaSamples.Domain.OrdemAggregate
{
    public class OrdemDeVenda : AggregateRoot<Guid>
    {
        private ICollection<ItemVenda> _itens;

        public OrdemDeVenda(Loja loja, Pessoa cliente)
        {
            Loja = loja ?? throw new ArgumentNullException(nameof(loja));
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            DataCriacao = DateTime.Now;
            Situacao = OrdemSituacao.EmEdicao;
            _itens = new List<ItemVenda>();
        }

        public virtual Loja Loja { get; private set; }

        public virtual Pessoa Cliente { get; private set; }

        public DateTimeOffset DataCriacao { get; private set; }

        public OrdemSituacao Situacao { get; private set; }

        public virtual IEnumerable<ItemVenda> Itens => _itens ??= new List<ItemVenda>();

        private ICollection<ItemVenda> InternalItens
        {
            get
            {
                if (_itens is null) 
                    _ = Itens;
                return _itens!;
            }
        }

        public IResult AdicionarProduto(Produto produto, int quantidade = 1)
        {
            if (quantidade <= 0)
                return BaseResult.InvalidParameters("Quantidade deve ser maior que zero (0)", nameof(quantidade));

            var item = InternalItens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (item is not null)
            {
                item.AdicionarQuantidade(quantidade);
            }
            else
            {
                item = new ItemVenda(Loja, produto, quantidade);
                if (item.ValorUnitario == 0)
                    return BaseResult.ValidationError("Produto sem preço de venda!");

                InternalItens.Add(item);
            }

            return BaseResult.ImmutableFailure;
        }
    }

    public enum OrdemSituacao
    {
        EmEdicao,
        Fechada,
        Cancelada,

        // as situações daqui em diante seriam opcionais,
        // é uma questão de design para atender as regras de negócio.
        // se poderia deixar as opções adiante para outras entidades,
        // como de processo de finalização da ordem e processo de entrega.
        // Estas outras entidades, que são de contexto diferentes,
        // poderiam ter suas situações, mas ainda poderia ser replicada a
        // situação para a ordem.
        // É uma decisão de design que seria tomada dependendo das regras de negócio.

        Reservada,
        Paga,
        Despachada,
        Entregue,
        Finalizada
    }


    public class ItemVenda : Entity<long>
    {
        public ItemVenda(Loja loja, Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = produto.ObterPreçoVenda(loja);
        }

        public Produto Produto { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        internal void AdicionarQuantidade(int quantidade)
            => Quantidade += quantidade;
    }

    public class Produto : Entity<Guid, string>
    {
        public string Descricao { get; private set; }

        public virtual ICollection<PrecoVenda> PrecoVenda { get; private set; }

        internal decimal ObterPreçoVenda(Loja loja)
        {
            var preco = PrecoVenda.FirstOrDefault(p => p.Loja.Id == loja.Id)
                ?? PrecoVenda.FirstOrDefault();

            return preco?.Valor ?? 0;
        }
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