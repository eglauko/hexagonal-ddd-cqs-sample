using HexaSamples.Domain.OrdemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaSamples.Infra.Persistence.Mappings;

public class ItemVendaMapping : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.ToTable("ItensVenda");

        builder.HasOne(i => i.Produto).WithMany().HasForeignKey("ProdutoId");
    }
}