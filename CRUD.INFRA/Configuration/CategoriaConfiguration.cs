using CRUD.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.INFRA.Configuration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categoria");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Descricao).HasColumnType("VARCHAR(80)");
        builder.HasMany(p => p.Produtos).WithOne(p => p.Categoria);
    }
}
