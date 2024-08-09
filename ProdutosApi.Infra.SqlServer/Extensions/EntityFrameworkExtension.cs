using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.SqlServer.Contexts;
using ProdutosApp.Infra.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.SqlServer.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>
                (cfg => cfg.UseSqlServer(configuration.GetConnectionString("ProdutosApp")));

            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
