using CRUD.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.INFRA.Data;

public class ApplicationContext : DbContext
{
	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{ }

	public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder builder)
	{
		builder.UseSqlServer("Data Source=DESKTOP-Q89RIRM\\SQLEXPRESS; Initial Catalog = CRUDAngular; Integrated Security = true; Trust Server Certificate = true;");
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
	}
}
