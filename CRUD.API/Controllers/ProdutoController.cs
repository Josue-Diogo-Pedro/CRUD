using CRUD.DOMAIN.Interfaces;
using CRUD.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IRepository<Produto> _repository;
	private readonly string bankError = "Erro ao retornar dados do banco de dados!", 
		productNotFound = "Produto não encontrado na base de dados!";

	public ProdutoController(IRepository<Produto> repository)
	{
		_repository= repository;
	}

    //============================================================================================================================
    [HttpGet]
	public async Task<ActionResult> GetProdutos()
	{
		try
		{
			var result = await _repository.GetAll();
			return Ok(result);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, bankError);
		}
	}

    //============================================================================================================================
    [HttpGet("{id:guid}")]
	public async Task<ActionResult<Produto>> GetProduto(Guid id)
	{
		try
		{
			var produto = await _repository.GetById(id);
			if (produto is null)
				return NotFound(new { message = productNotFound });

			return Ok(produto);
		}
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, bankError);
        }
	}

    //============================================================================================================================
    [HttpPost]
	public async Task<ActionResult> AddProduto(Produto produto)
	{
		try
		{
			if (produto is null)
				return BadRequest();

			var produtocreated = await _repository.Create(produto);
			return CreatedAtAction(nameof(GetProduto), new { Id = produtocreated.CategoriaId }, produtocreated);
		}
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, bankError);
        }
	}

    //============================================================================================================================
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "PodeAtualizar")]
    public async Task<ActionResult<Produto>> UpdateProduto(Guid id, Produto produto)
	{
		try
		{
			if(id != produto.Id)
				return BadRequest(new {message = "O código do produto não confere com o produto a ser atualizado!"});

			var produtoupdated = await _repository.Update(produto);
			if (produtoupdated is null)
				return NotFound(new { message = productNotFound });

			return await _repository.Update(produto);
		}
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, bankError);
        }
	}

    //============================================================================================================================
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "PodeExcluir")]
    public async Task<ActionResult<Produto>> DeleteProduto(Guid id)
	{
		try
		{
			var result = await _repository.GetById(id);
			if (result is null)
				return NotFound(new { message = productNotFound });

			return await _repository.Remove(id);
		}
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, bankError);
        }
	}
}
