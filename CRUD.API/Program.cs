using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CRUD.API.Areas.Identity.Data;
using CRUD.INFRA.Data;
using CRUD.INFRA.Repositories;
using CRUD.DOMAIN.Interfaces;
using CRUD.DOMAIN.Models;
using CRUD.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CRUD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("CRUDAPIContextConnection") ?? throw new InvalidOperationException("Connection string 'CRUDAPIContextConnection' not found.");

            builder.Services.AddDbContext<CRUDAPIContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CRUDAPIContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeExcluir")));
                options.AddPolicy("PodeAtualizar", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeAtualizar")));
            });

            builder.Services.AddTransient<IRepository<Produto>, Repository<Produto>>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddTransient<ApplicationContext, ApplicationContext>();
            builder.Services.AddTransient<IAuthorizationHandler, PermissaoNecessariaHandler>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}