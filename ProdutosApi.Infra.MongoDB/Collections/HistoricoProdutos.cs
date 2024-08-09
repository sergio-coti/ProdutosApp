using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.MongoDB.Collections
{
    public class HistoricoProdutos
    {
        public Guid? Id { get; set; }
        public DateTime? DataHora { get; set; }
        public string? TipoOperacao { get; set; }
        public string? Produto { get; set; }
    }
}
