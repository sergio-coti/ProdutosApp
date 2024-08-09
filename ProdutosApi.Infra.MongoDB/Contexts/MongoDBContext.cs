using MongoDB.Driver;
using ProdutosApp.Infra.MongoDB.Collections;
using ProdutosApp.Infra.MongoDB.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.MongoDB.Contexts
{
    public class MongoDBContext
    {
        private readonly MongoDBSettings? _mongoDBSettings;
        private IMongoDatabase? _mongoDatabase;

        public MongoDBContext(MongoDBSettings? mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;

            #region Conexão com o banco de dados

            var mongoClientSettings = MongoClientSettings.FromUrl
                (new MongoUrl(_mongoDBSettings?.Host));

            var mongoClient = new MongoClient(mongoClientSettings);
            _mongoDatabase = mongoClient.GetDatabase(_mongoDBSettings?.Database);

            #endregion
        }

        #region Mapeamento da coleção

        public IMongoCollection<HistoricoProdutos>? Produtos
            => _mongoDatabase?.GetCollection<HistoricoProdutos>("HistoricoProdutos");

        #endregion
    }
}
