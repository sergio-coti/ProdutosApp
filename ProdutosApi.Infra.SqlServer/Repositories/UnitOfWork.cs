using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.SqlServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.SqlServer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _dataContext;

        public UnitOfWork(DataContext? dataContext)
        {
            _dataContext = dataContext;
        }

        public ICategoriaRepository CategoriaRepository 
            => new CategoriaRepository(_dataContext);

        public IProdutoRepository ProdutoRepository => 
            new ProdutoRepository(_dataContext);

        public void SaveChanges()
        {
            _dataContext?.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
