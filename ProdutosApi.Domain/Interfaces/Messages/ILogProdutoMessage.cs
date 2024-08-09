using ProdutosApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Interfaces.Messages
{
    public interface ILogProdutoMessage
    {
        void SendMessage(LogProduto logProduto);
    }
}
