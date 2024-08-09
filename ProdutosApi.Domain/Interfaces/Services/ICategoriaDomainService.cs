using ProdutosApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Services
{
    public interface ICategoriaDomainService : IDisposable
    {
        List<CategoriaResponse> GetAll();
    }
}
