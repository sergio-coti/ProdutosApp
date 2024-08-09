using ProdutosApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Services
{
    public interface IProdutoDomainService : IDisposable
    {
        ProdutoResponse Add(ProdutoRequest request);
        ProdutoResponse Update(Guid? id, ProdutoRequest request);
        ProdutoResponse Delete(Guid? id);

        List<ProdutoResponse> GetAll(int pageIndex, int pageSize);
        ProdutoResponse? GetById(Guid? id);
    }
}
