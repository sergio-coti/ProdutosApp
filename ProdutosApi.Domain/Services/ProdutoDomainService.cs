using AutoMapper;
using Newtonsoft.Json;
using ProdutosApp.Domain.Dtos;
using ProdutosApp.Domain.Entities;
using ProdutosApp.Domain.Interfaces.Messages;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Services
{
    public class ProdutoDomainService : IProdutoDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;
        private readonly ILogProdutoMessage? _logProdutoMessage;

        private string? _notFound = "Produto não encontrado. Verifique o ID informado.";

        public ProdutoDomainService(IUnitOfWork? unitOfWork, IMapper? mapper, ILogProdutoMessage? logProdutoMessage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logProdutoMessage = logProdutoMessage;
        }

        public ProdutoResponse Add(ProdutoRequest request)
        {
            var produto = _mapper?.Map<Produto>(request);

            _unitOfWork?.ProdutoRepository.Add(produto);
            _unitOfWork?.SaveChanges();

            _logProdutoMessage?.SendMessage(new LogProduto 
            { 
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now,
                TipoOperacao = TipoOperacao.CREATED,
                DetalhesProduto = JsonConvert.SerializeObject(produto)
            });

            return _mapper?.Map<ProdutoResponse>(_unitOfWork?.ProdutoRepository.GetById(produto.Id.Value));
        }

        public ProdutoResponse Update(Guid? id, ProdutoRequest request)
        {
            var produto = _unitOfWork?.ProdutoRepository.GetById(id.Value);
            if (produto == null)
                throw new ApplicationException(_notFound);

            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.Quantidade = request.Quantidade;
            produto.CategoriaId = request.CategoriaId;

            _unitOfWork?.ProdutoRepository.Update(produto);
            _unitOfWork?.SaveChanges();

            _logProdutoMessage?.SendMessage(new LogProduto
            {
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now,
                TipoOperacao = TipoOperacao.UPDATED,
                DetalhesProduto = JsonConvert.SerializeObject(produto)
            });

            return _mapper?.Map<ProdutoResponse>(_unitOfWork?.ProdutoRepository.GetById(produto.Id.Value));
        }

        public ProdutoResponse Delete(Guid? id)
        {
            var produto = _unitOfWork?.ProdutoRepository.GetById(id.Value);
            if (produto == null)
                throw new ApplicationException(_notFound);

            _unitOfWork?.ProdutoRepository.Delete(produto);
            _unitOfWork?.SaveChanges();

            _logProdutoMessage?.SendMessage(new LogProduto
            {
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now,
                TipoOperacao = TipoOperacao.DELETED,
                DetalhesProduto = JsonConvert.SerializeObject(produto)
            });

            return _mapper?.Map<ProdutoResponse>(_unitOfWork?.ProdutoRepository.GetById(produto.Id.Value));
        }

        public List<ProdutoResponse> GetAll(int pageIndex, int pageSize)
        {
            var produtos = _unitOfWork?.ProdutoRepository.GetAll(); //TODO Implementar a paginação no repositório
            return _mapper?.Map<List<ProdutoResponse>>(produtos);
        }

        public ProdutoResponse? GetById(Guid? id)
        {
            var produto = _unitOfWork?.ProdutoRepository.GetById(id.Value);
            if (produto == null)
                throw new ApplicationException(_notFound);

            return _mapper?.Map<ProdutoResponse>(produto);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
