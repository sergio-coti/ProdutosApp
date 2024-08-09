using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProdutosApp.Domain.Interfaces.Messages;
using ProdutosApp.Infra.RabbitMQ.Producers;
using ProdutosApp.Infra.RabbitMQ.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.RabbitMQ.Extensions
{
    public static class RabbitMQExtension
    {
        public static IServiceCollection AddRabbitMQ
            (this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = new RabbitMQSettings();
            new ConfigureFromConfigurationOptions<RabbitMQSettings>
                (configuration.GetSection("RabbitMQ")).Configure(rabbitMQSettings);

            services.AddSingleton(rabbitMQSettings);

            services.AddSingleton<IConnectionFactory>(cf =>
            {
                var config = cf.GetRequiredService<RabbitMQSettings>();
                return new ConnectionFactory
                {
                    HostName = config.Hostname,
                    Port = config.Port.Value,
                    UserName = config.Username,
                    Password = config.Password,
                    VirtualHost = config.Username
                };
            });

            services.AddTransient<ILogProdutoMessage, LogProdutoMessageProducer>();

            return services;
        }
    }
}
