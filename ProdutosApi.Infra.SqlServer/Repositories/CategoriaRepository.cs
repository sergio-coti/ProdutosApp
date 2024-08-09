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
    public class CategoriaRepository : BaseRepository<Categoria, Guid>, ICategoriaRepository
    {
        private readonly DataContext? _dataContext;

        public CategoriaRepository(DataContext? dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
