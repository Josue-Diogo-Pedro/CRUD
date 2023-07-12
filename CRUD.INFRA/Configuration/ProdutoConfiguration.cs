using CRUD.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.INFRA.Configuration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Titulo).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.Descricao).HasColumnType("VARCHAR(80)");
        builder.Property(p => p.Imagem).HasColumnType("VARCHAR(1024)");
    }
}
