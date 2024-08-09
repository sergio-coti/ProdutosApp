using AutoMapper;
using ProdutosApp.Domain.Dtos;
using ProdutosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProdutoRequest, Produto>()
                .AfterMap((src, dest) => 
                {
                    dest.Id = Guid.NewGuid();
                });

            CreateMap<Produto, ProdutoResponse>();

            CreateMap<Categoria, CategoriaResponse>();
        }
    }
}
