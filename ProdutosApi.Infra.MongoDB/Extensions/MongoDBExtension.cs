using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProdutosApp.Infra.MongoDB.Contexts;
using ProdutosApp.Infra.MongoDB.Persistence;
using ProdutosApp.Infra.MongoDB.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.MongoDB.Extensions
{
    public static class MongoDBExtension
    {
        public static IServiceCollection AddMongoDBConfig
            (this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSettings = new MongoDBSettings();
            new ConfigureFromConfigurationOptions<MongoDBSettings>
                (configuration.GetSection("MongoDB"))
                .Configure(mongoDBSettings);

            services.AddSingleton(mongoDBSettings);
            services.AddTransient<MongoDBContext>();
            services.AddTransient<HistoricoProdutoPresistence>();

            return services;
        }
    }
}
