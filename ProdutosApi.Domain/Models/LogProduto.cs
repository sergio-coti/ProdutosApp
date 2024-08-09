using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Models
{
    public class LogProduto
    {
        public Guid? Id { get; set; }
        public DateTime? DataHora { get; set; }
        public TipoOperacao? TipoOperacao { get; set; }
        public string? DetalhesProduto { get; set; }
    }

    public enum TipoOperacao
    {
        CREATED = 1, UPDATED = 2, DELETED = 3
    }
}
