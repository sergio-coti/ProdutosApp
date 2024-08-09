using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApp.Domain.Dtos;
using ProdutosApp.Domain.Interfaces.Services;

namespace ProdutosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaDomainService? _categoriaDomainService;

        public CategoriasController(ICategoriaDomainService? categoriaDomainService)
        {
            _categoriaDomainService = categoriaDomainService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaResponse>), 200)]
        public IActionResult GetAll()
        {
            var response = _categoriaDomainService?.GetAll();
            return Ok(response);
        }
    }
}
