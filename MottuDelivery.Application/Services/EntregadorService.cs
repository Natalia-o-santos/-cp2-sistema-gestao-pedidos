using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Application.Services;

public interface IEntregadorService
{
    Task<IEnumerable<EntregadorDto>> GetAllAsync();
    Task<EntregadorDto?> GetByIdAsync(Guid id);
    Task<EntregadorDto> CreateAsync(CreateEntregadorDto dto);
    Task<EntregadorDto?> UpdateAsync(Guid id, UpdateEntregadorDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<EntregadorDto>> GetByStatusAsync(string status);
    Task<IEnumerable<EntregadorDto>> GetDisponiveisAsync();
}

public class EntregadorService : IEntregadorService
{
    private readonly IEntregadorRepository _repository;
    private readonly IMapper _mapper;

    public EntregadorService(IEntregadorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EntregadorDto>> GetAllAsync()
    {
        var entregadores = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<EntregadorDto>>(entregadores);
    }

    public async Task<EntregadorDto?> GetByIdAsync(Guid id)
    {
        var entregador = await _repository.GetByIdAsync(id);
        return entregador != null ? _mapper.Map<EntregadorDto>(entregador) : null;
    }

    public async Task<EntregadorDto> CreateAsync(CreateEntregadorDto dto)
    {
        // Verificar se CPF já existe
        var existingCpf = await _repository.GetByCpfAsync(dto.Cpf);
        if (existingCpf != null)
            throw new InvalidOperationException("CPF já cadastrado");

        // Verificar se Email já existe
        var existingEmail = await _repository.GetByEmailAsync(dto.Email);
        if (existingEmail != null)
            throw new InvalidOperationException("Email já cadastrado");

        var entregador = _mapper.Map<Entregador>(dto);
        var createdEntregador = await _repository.AddAsync(entregador);
        return _mapper.Map<EntregadorDto>(createdEntregador);
    }

    public async Task<EntregadorDto?> UpdateAsync(Guid id, UpdateEntregadorDto dto)
    {
        var entregador = await _repository.GetByIdAsync(id);
        if (entregador == null)
            return null;

        // Verificar se email já existe em outro entregador
        var existingEmail = await _repository.GetByEmailAsync(dto.Email);
        if (existingEmail != null && existingEmail.Id != id)
            throw new InvalidOperationException("Email já cadastrado para outro entregador");

        entregador.AtualizarDados(dto.Nome, dto.Telefone, dto.Email);
        await _repository.UpdateAsync(entregador);
        return _mapper.Map<EntregadorDto>(entregador);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entregador = await _repository.GetByIdAsync(id);
        if (entregador == null)
            return false;

        // Verificar se entregador tem entregas em andamento
        if (entregador.Entregas.Any(e => e.Status == StatusEntrega.EmAndamento))
            throw new InvalidOperationException("Não é possível excluir entregador com entregas em andamento");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<EntregadorDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusEntregador>(status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        var entregadores = await _repository.GetByStatusAsync(statusEnum);
        return _mapper.Map<IEnumerable<EntregadorDto>>(entregadores);
    }

    public async Task<IEnumerable<EntregadorDto>> GetDisponiveisAsync()
    {
        var entregadores = await _repository.GetDisponiveisAsync();
        return _mapper.Map<IEnumerable<EntregadorDto>>(entregadores);
    }
}
