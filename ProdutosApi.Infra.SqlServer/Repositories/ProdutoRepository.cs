using Microsoft.EntityFrameworkCore;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.SqlServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.SqlServer.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto, Guid>, IProdutoRepository
    {
        private readonly DataContext? _dataContext;

        public ProdutoRepository(DataContext? dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public override List<Produto> GetAll()
        {
            return _dataContext?
                    .Set<Produto>()
                    .Include(p => p.Categoria)
                    .ToList();
        }

        public override List<Produto> GetAll(Func<Produto, bool> where)
        {
            return _dataContext?
                    .Set<Produto>()
                    .Include(p => p.Categoria)
                    .Where(where)
                    .ToList();
        }

        public override Produto? GetById(Guid id)
        {
            return _dataContext?
                    .Set<Produto>()
                    .Include(p => p.Categoria)
                    .FirstOrDefault(p => p.Id == id);
        }

        public override Produto? Get(Func<Produto, bool> where)
        {
            return _dataContext?
                    .Set<Produto>()
                    .Include(p => p.Categoria)
                    .FirstOrDefault(where);
        }
    }
}
