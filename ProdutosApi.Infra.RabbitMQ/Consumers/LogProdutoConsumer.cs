using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProdutosApp.Domain.Models;
using ProdutosApp.Infra.MongoDB.Collections;
using ProdutosApp.Infra.MongoDB.Contexts;
using ProdutosApp.Infra.MongoDB.Persistence;
using ProdutosApp.Infra.RabbitMQ.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.RabbitMQ.Consumers
{
    public class LogProdutoConsumer : BackgroundService
    {
        private readonly IConnectionFactory? _connectionFactory;
        private readonly RabbitMQSettings? _rabbitMQSettings;
        private readonly HistoricoProdutoPresistence? _historicoProdutoPresistence;
        private readonly IServiceProvider? _serviceProvider;

        public LogProdutoConsumer(IConnectionFactory? connectionFactory, RabbitMQSettings? rabbitMQSettings, HistoricoProdutoPresistence? historicoProdutoPresistence, IServiceProvider? serviceProvider)
        {
            _connectionFactory = connectionFactory;
            _rabbitMQSettings = rabbitMQSettings;
            _historicoProdutoPresistence = historicoProdutoPresistence;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = _connectionFactory?.CreateConnection();
            var model = connection?.CreateModel();

            model.QueueDeclare(
                queue: _rabbitMQSettings?.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (sender, args) =>
            {
                var content = Encoding.UTF8.GetString(args.Body.ToArray());
                var logProdutos = JsonConvert.DeserializeObject<LogProduto>(content);

                #region Gravar o log no MongoDB

                using (var scope = _serviceProvider?.CreateScope())
                {
                    var historicoProdutos = new HistoricoProdutos
                    {
                        Id = logProdutos.Id,
                        DataHora = logProdutos.DataHora,
                        TipoOperacao = logProdutos.TipoOperacao.ToString(),
                        Produto = logProdutos.DetalhesProduto
                    };

                    _historicoProdutoPresistence?.Add(historicoProdutos);                    
                }

                model.BasicAck(args.DeliveryTag, false);

                #endregion
            };

            model.BasicConsume(_rabbitMQSettings?.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}
