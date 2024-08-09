using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Domain.Dtos;
using ProdutosApp.Domain.Interfaces.Services;
using System.Net;

namespace ProdutosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoDomainService? _produtoDomainService;

        public ProdutosController(IProdutoDomainService? produtoDomainService)
        {
            _produtoDomainService = produtoDomainService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProdutoResponse), 201)]
        public IActionResult Post([FromBody] ProdutoRequest request)
        {
            var response = _produtoDomainService?.Add(request);
            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProdutoResponse), 200)]
        public IActionResult Put(Guid id, [FromBody] ProdutoRequest request)
        {
            var response = _produtoDomainService?.Update(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProdutoResponse), 200)]
        public IActionResult Delete(Guid id)
        {
            var response = _produtoDomainService?.Delete(id);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoResponse>), 200)]
        public IActionResult GetAll(int pageIndex = 0, int pageSize = 10) 
        {
            var response = _produtoDomainService?.GetAll(pageIndex, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoResponse), 200)]
        public IActionResult GetById(Guid id) 
        {
            var response = _produtoDomainService?.GetById(id);
            return Ok(response);
        }
    }
}
