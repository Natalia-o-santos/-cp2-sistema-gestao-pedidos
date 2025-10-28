using Microsoft.AspNetCore.Mvc;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Application.Services;
using FluentValidation;

namespace MottuDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EntregadoresController : ControllerBase
{
    private readonly IEntregadorService _entregadorService;
    private readonly IValidator<CreateEntregadorDto> _createValidator;
    private readonly IValidator<UpdateEntregadorDto> _updateValidator;

    public EntregadoresController(
        IEntregadorService entregadorService,
        IValidator<CreateEntregadorDto> createValidator,
        IValidator<UpdateEntregadorDto> updateValidator)
    {
        _entregadorService = entregadorService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <summary>
    /// Obtém todos os entregadores
    /// </summary>
    /// <returns>Lista de entregadores</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EntregadorDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregadorDto>>> GetAll()
    {
        var entregadores = await _entregadorService.GetAllAsync();
        return Ok(entregadores);
    }

    /// <summary>
    /// Obtém um entregador por ID
    /// </summary>
    /// <param name="id">ID do entregador</param>
    /// <returns>Dados do entregador</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EntregadorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EntregadorDto>> GetById(Guid id)
    {
        var entregador = await _entregadorService.GetByIdAsync(id);
        if (entregador == null)
            return NotFound($"Entregador com ID {id} não encontrado");

        return Ok(entregador);
    }

    /// <summary>
    /// Cria um novo entregador
    /// </summary>
    /// <param name="dto">Dados do entregador</param>
    /// <returns>Entregador criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EntregadorDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<EntregadorDto>> Create([FromBody] CreateEntregadorDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var entregador = await _entregadorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = entregador.Id }, entregador);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um entregador
    /// </summary>
    /// <param name="id">ID do entregador</param>
    /// <param name="dto">Novos dados do entregador</param>
    /// <returns>Entregador atualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EntregadorDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EntregadorDto>> Update(Guid id, [FromBody] UpdateEntregadorDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var entregador = await _entregadorService.UpdateAsync(id, dto);
            if (entregador == null)
                return NotFound($"Entregador com ID {id} não encontrado");

            return Ok(entregador);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Exclui um entregador
    /// </summary>
    /// <param name="id">ID do entregador</param>
    /// <returns>Resultado da operação</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _entregadorService.DeleteAsync(id);
            if (!result)
                return NotFound($"Entregador com ID {id} não encontrado");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém entregadores por status
    /// </summary>
    /// <param name="status">Status do entregador (Ativo, Inativo, EmEntrega)</param>
    /// <returns>Lista de entregadores</returns>
    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(IEnumerable<EntregadorDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<EntregadorDto>>> GetByStatus(string status)
    {
        try
        {
            var entregadores = await _entregadorService.GetByStatusAsync(status);
            return Ok(entregadores);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém entregadores disponíveis
    /// </summary>
    /// <returns>Lista de entregadores disponíveis</returns>
    [HttpGet("disponiveis")]
    [ProducesResponseType(typeof(IEnumerable<EntregadorDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregadorDto>>> GetDisponiveis()
    {
        var entregadores = await _entregadorService.GetDisponiveisAsync();
        return Ok(entregadores);
    }
}
