using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Application.Services;

public interface IEntregaService
{
    Task<IEnumerable<EntregaDto>> GetAllAsync();
    Task<EntregaDto?> GetByIdAsync(Guid id);
    Task<EntregaDto> CreateAsync(CreateEntregaDto dto);
    Task<EntregaDto?> UpdateStatusAsync(Guid id, UpdateEntregaStatusDto dto);
    Task<EntregaDto?> UpdateObservacoesAsync(Guid id, UpdateEntregaObservacoesDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<EntregaDto>> GetByEntregadorIdAsync(Guid entregadorId);
    Task<IEnumerable<EntregaDto>> GetByStatusAsync(string status);
    Task<IEnumerable<EntregaDto>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<EntregaDto>> GetEntregasPendentesAsync();
}

public class EntregaService : IEntregaService
{
    private readonly IEntregaRepository _entregaRepository;
    private readonly IEntregadorRepository _entregadorRepository;
    private readonly IMapper _mapper;

    public EntregaService(IEntregaRepository entregaRepository, IEntregadorRepository entregadorRepository, IMapper mapper)
    {
        _entregaRepository = entregaRepository;
        _entregadorRepository = entregadorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EntregaDto>> GetAllAsync()
    {
        var entregas = await _entregaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EntregaDto>>(entregas);
    }

    public async Task<EntregaDto?> GetByIdAsync(Guid id)
    {
        var entrega = await _entregaRepository.GetByIdAsync(id);
        return entrega != null ? _mapper.Map<EntregaDto>(entrega) : null;
    }

    public async Task<EntregaDto> CreateAsync(CreateEntregaDto dto)
    {
        // Verificar se entregador existe e está disponível
        var entregador = await _entregadorRepository.GetByIdAsync(dto.EntregadorId);
        if (entregador == null)
            throw new InvalidOperationException("Entregador não encontrado");

        if (!entregador.PodeReceberEntrega())
            throw new InvalidOperationException("Entregador não está disponível para receber entregas");

        var entrega = _mapper.Map<Entrega>(dto);
        var createdEntrega = await _entregaRepository.AddAsync(entrega);
        return _mapper.Map<EntregaDto>(createdEntrega);
    }

    public async Task<EntregaDto?> UpdateStatusAsync(Guid id, UpdateEntregaStatusDto dto)
    {
        var entrega = await _entregaRepository.GetByIdAsync(id);
        if (entrega == null)
            return null;

        if (!Enum.TryParse<StatusEntrega>(dto.Status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        switch (statusEnum)
        {
            case StatusEntrega.EmAndamento:
                entrega.IniciarEntrega();
                break;
            case StatusEntrega.Concluida:
                entrega.ConcluirEntrega(dto.Observacoes);
                break;
            case StatusEntrega.Cancelada:
                if (string.IsNullOrWhiteSpace(dto.Observacoes))
                    throw new ArgumentException("Motivo do cancelamento é obrigatório");
                entrega.CancelarEntrega(dto.Observacoes);
                break;
            default:
                throw new ArgumentException("Status não pode ser alterado diretamente");
        }

        await _entregaRepository.UpdateAsync(entrega);
        return _mapper.Map<EntregaDto>(entrega);
    }

    public async Task<EntregaDto?> UpdateObservacoesAsync(Guid id, UpdateEntregaObservacoesDto dto)
    {
        var entrega = await _entregaRepository.GetByIdAsync(id);
        if (entrega == null)
            return null;

        entrega.AtualizarObservacoes(dto.Observacoes);
        await _entregaRepository.UpdateAsync(entrega);
        return _mapper.Map<EntregaDto>(entrega);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entrega = await _entregaRepository.GetByIdAsync(id);
        if (entrega == null)
            return false;

        // Não permitir exclusão de entregas em andamento ou concluídas
        if (entrega.Status == StatusEntrega.EmAndamento || entrega.Status == StatusEntrega.Concluida)
            throw new InvalidOperationException("Não é possível excluir entregas em andamento ou concluídas");

        await _entregaRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<EntregaDto>> GetByEntregadorIdAsync(Guid entregadorId)
    {
        var entregas = await _entregaRepository.GetByEntregadorIdAsync(entregadorId);
        return _mapper.Map<IEnumerable<EntregaDto>>(entregas);
    }

    public async Task<IEnumerable<EntregaDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusEntrega>(status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        var entregas = await _entregaRepository.GetByStatusAsync(statusEnum);
        return _mapper.Map<IEnumerable<EntregaDto>>(entregas);
    }

    public async Task<IEnumerable<EntregaDto>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var entregas = await _entregaRepository.GetByPeriodoAsync(dataInicio, dataFim);
        return _mapper.Map<IEnumerable<EntregaDto>>(entregas);
    }

    public async Task<IEnumerable<EntregaDto>> GetEntregasPendentesAsync()
    {
        var entregas = await _entregaRepository.GetEntregasPendentesAsync();
        return _mapper.Map<IEnumerable<EntregaDto>>(entregas);
    }
}
