using ProdutosApp.Infra.MongoDB.Collections;
using ProdutosApp.Infra.MongoDB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.MongoDB.Persistence
{
    public class HistoricoProdutoPresistence
    {
        private readonly MongoDBContext? _mongoDBContext;

        public HistoricoProdutoPresistence(MongoDBContext? mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public void Add(HistoricoProdutos historicoProdutos)
        {
            _mongoDBContext?.Produtos?.InsertOne(historicoProdutos);
        }
    }
}
