using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Entities
{
    public class Categoria
    {
        #region Propriedades

        public Guid? Id { get; set; }
        public string? Nome { get; set; }

        #endregion

        #region Relacionamentos

        public List<Produto>? Produtos { get; set; }

        #endregion
    }
}
