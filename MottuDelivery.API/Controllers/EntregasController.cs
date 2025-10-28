using Microsoft.AspNetCore.Mvc;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Application.Services;
using FluentValidation;

namespace MottuDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EntregasController : ControllerBase
{
    private readonly IEntregaService _entregaService;
    private readonly IValidator<CreateEntregaDto> _createValidator;
    private readonly IValidator<UpdateEntregaStatusDto> _statusValidator;
    private readonly IValidator<UpdateEntregaObservacoesDto> _observacoesValidator;

    public EntregasController(
        IEntregaService entregaService,
        IValidator<CreateEntregaDto> createValidator,
        IValidator<UpdateEntregaStatusDto> statusValidator,
        IValidator<UpdateEntregaObservacoesDto> observacoesValidator)
    {
        _entregaService = entregaService;
        _createValidator = createValidator;
        _statusValidator = statusValidator;
        _observacoesValidator = observacoesValidator;
    }

    /// <summary>
    /// Obtém todas as entregas
    /// </summary>
    /// <returns>Lista de entregas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EntregaDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregaDto>>> GetAll()
    {
        var entregas = await _entregaService.GetAllAsync();
        return Ok(entregas);
    }

    /// <summary>
    /// Obtém uma entrega por ID
    /// </summary>
    /// <param name="id">ID da entrega</param>
    /// <returns>Dados da entrega</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EntregaDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EntregaDto>> GetById(Guid id)
    {
        var entrega = await _entregaService.GetByIdAsync(id);
        if (entrega == null)
            return NotFound($"Entrega com ID {id} não encontrada");

        return Ok(entrega);
    }

    /// <summary>
    /// Cria uma nova entrega
    /// </summary>
    /// <param name="dto">Dados da entrega</param>
    /// <returns>Entrega criada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EntregaDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<EntregaDto>> Create([FromBody] CreateEntregaDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var entrega = await _entregaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = entrega.Id }, entrega);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza o status de uma entrega
    /// </summary>
    /// <param name="id">ID da entrega</param>
    /// <param name="dto">Novo status e observações</param>
    /// <returns>Entrega atualizada</returns>
    [HttpPut("{id}/status")]
    [ProducesResponseType(typeof(EntregaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EntregaDto>> UpdateStatus(Guid id, [FromBody] UpdateEntregaStatusDto dto)
    {
        var validationResult = await _statusValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var entrega = await _entregaService.UpdateStatusAsync(id, dto);
            if (entrega == null)
                return NotFound($"Entrega com ID {id} não encontrada");

            return Ok(entrega);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza as observações de uma entrega
    /// </summary>
    /// <param name="id">ID da entrega</param>
    /// <param name="dto">Novas observações</param>
    /// <returns>Entrega atualizada</returns>
    [HttpPut("{id}/observacoes")]
    [ProducesResponseType(typeof(EntregaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EntregaDto>> UpdateObservacoes(Guid id, [FromBody] UpdateEntregaObservacoesDto dto)
    {
        var validationResult = await _observacoesValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var entrega = await _entregaService.UpdateObservacoesAsync(id, dto);
        if (entrega == null)
            return NotFound($"Entrega com ID {id} não encontrada");

        return Ok(entrega);
    }

    /// <summary>
    /// Exclui uma entrega
    /// </summary>
    /// <param name="id">ID da entrega</param>
    /// <returns>Resultado da operação</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _entregaService.DeleteAsync(id);
            if (!result)
                return NotFound($"Entrega com ID {id} não encontrada");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém entregas por entregador
    /// </summary>
    /// <param name="entregadorId">ID do entregador</param>
    /// <returns>Lista de entregas</returns>
    [HttpGet("entregador/{entregadorId}")]
    [ProducesResponseType(typeof(IEnumerable<EntregaDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregaDto>>> GetByEntregador(Guid entregadorId)
    {
        var entregas = await _entregaService.GetByEntregadorIdAsync(entregadorId);
        return Ok(entregas);
    }

    /// <summary>
    /// Obtém entregas por status
    /// </summary>
    /// <param name="status">Status da entrega (Pendente, EmAndamento, Concluida, Cancelada)</param>
    /// <returns>Lista de entregas</returns>
    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(IEnumerable<EntregaDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<EntregaDto>>> GetByStatus(string status)
    {
        try
        {
            var entregas = await _entregaService.GetByStatusAsync(status);
            return Ok(entregas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtém entregas por período
    /// </summary>
    /// <param name="dataInicio">Data de início</param>
    /// <param name="dataFim">Data de fim</param>
    /// <returns>Lista de entregas</returns>
    [HttpGet("periodo")]
    [ProducesResponseType(typeof(IEnumerable<EntregaDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregaDto>>> GetByPeriodo(
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim)
    {
        var entregas = await _entregaService.GetByPeriodoAsync(dataInicio, dataFim);
        return Ok(entregas);
    }

    /// <summary>
    /// Obtém entregas pendentes
    /// </summary>
    /// <returns>Lista de entregas pendentes</returns>
    [HttpGet("pendentes")]
    [ProducesResponseType(typeof(IEnumerable<EntregaDto>), 200)]
    public async Task<ActionResult<IEnumerable<EntregaDto>>> GetPendentes()
    {
        var entregas = await _entregaService.GetEntregasPendentesAsync();
        return Ok(entregas);
    }
}
