using HexaSamples.Domain.OrdemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaSamples.Infra.Persistence.Mappings;

public class OrdemDeVendaMapping : IEntityTypeConfiguration<OrdemDeVenda>
{
    public void Configure(EntityTypeBuilder<OrdemDeVenda> builder)
    {
        builder.ToTable("OrdensDeVenda");

        builder.HasMany(o => o.Itens).WithOne().HasForeignKey("OrdemDeVendaId");
        builder.HasOne(o => o.Loja).WithMany().HasForeignKey("LojaId");
        builder.HasOne(o => o.Cliente).WithMany().HasForeignKey("ClienteId");
    }
}