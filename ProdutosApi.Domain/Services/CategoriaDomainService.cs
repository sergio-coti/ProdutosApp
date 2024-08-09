using AutoMapper;
using ProdutosApp.Domain.Dtos;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Services
{
    public class CategoriaDomainService : ICategoriaDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;

        public CategoriaDomainService(IUnitOfWork? unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<CategoriaResponse> GetAll()
        {
            return _mapper?.Map<List<CategoriaResponse>>(_unitOfWork?.CategoriaRepository.GetAll());
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
