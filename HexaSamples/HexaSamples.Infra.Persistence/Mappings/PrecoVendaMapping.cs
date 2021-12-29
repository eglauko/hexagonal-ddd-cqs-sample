using HexaSamples.Domain.SupportEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaSamples.Infra.Persistence.Mappings;

public class PrecoVendaMapping : IEntityTypeConfiguration<PrecoVenda>
{
    public void Configure(EntityTypeBuilder<PrecoVenda> builder)
    {
        builder.ToTable("PrecosVenda");
        builder.HasOne(p => p.Loja).WithMany().HasForeignKey("LojaId");
    }
}