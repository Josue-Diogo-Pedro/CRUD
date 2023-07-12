using CRUD.DOMAIN.Interfaces;
using CRUD.DOMAIN.Models;
using CRUD.INFRA.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _repository;
        private readonly string bankError = "Erro ao retornar dados do banco de dados!",
            categoryNotFound = "Categoria não encontrada na base de dados!";

        public CategoriaController(CategoriaRepository repository)
        {
            _repository = repository;
        }

        //============================================================================================================================
        [HttpGet]
        public async Task<ActionResult> GetCategorias()
        {
            try
            {
                var result = await _repository.GetAllAndProducts();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, bankError);
            }
        }

        //============================================================================================================================
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Produto>> GetCategoria(Guid id)
        {
            try
            {
                var categoria = await _repository.GetById(id);
                if (categoria is null)
                    return NotFound(new { message = categoryNotFound });

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, bankError);
            }
        }

        //============================================================================================================================
        [HttpPost]
        public async Task<ActionResult> AddCategoria(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest();

                var categoriacreated = await _repository.Create(categoria);
                return Ok(categoriacreated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, bankError);
            }
        }

        //============================================================================================================================
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Categoria>> UpdateCategoria(Guid id, Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest(new { message = "O código da categoria não confere com a categoria a ser atualizado!" });

                var categoriaupdated = await _repository.Update(categoria);
                if (categoriaupdated is null)
                    return NotFound(new { message = categoryNotFound });

                return await _repository.Update(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, bankError);
            }
        }

        //============================================================================================================================
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(Guid id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result is null)
                    return NotFound(new { message = categoryNotFound });

                return await _repository.Remove(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, bankError);
            }
        }
    }
}
