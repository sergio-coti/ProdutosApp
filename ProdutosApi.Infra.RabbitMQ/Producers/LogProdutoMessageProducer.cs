using Newtonsoft.Json;
using ProdutosApp.Domain.Interfaces.Messages;
using ProdutosApp.Domain.Models;
using ProdutosApp.Infra.RabbitMQ.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.RabbitMQ.Producers
{
    public class LogProdutoMessageProducer : ILogProdutoMessage
    {
        private readonly IConnectionFactory? _connectionFactory;
        private readonly RabbitMQSettings? _settings;

        public LogProdutoMessageProducer(IConnectionFactory? connectionFactory, RabbitMQSettings? settings)
        {
            _connectionFactory = connectionFactory;
            _settings = settings;
        }

        public void SendMessage(LogProduto logProduto)
        {
            using var connection = _connectionFactory?.CreateConnection();
            using var channel = connection?.CreateModel();

            channel?.QueueDeclare(
                queue: _settings?.Queue, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logProduto));

            channel.BasicPublish(
                exchange: string.Empty, 
                routingKey: _settings?.Queue, 
                basicProperties: null, 
                body: body);

        }
    }
}
