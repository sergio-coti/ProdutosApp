using ProdutosApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Logs
{
    public interface ILogProdutoPersistence
    {
        void Add(LogProduto logProduto);
        void Update(LogProduto logProduto);
        void Delete(Guid id);

        List<LogProduto> GetAll(DateTime dataMin, DateTime dataMax);
        LogProduto? GetById(Guid id);
    }
}
