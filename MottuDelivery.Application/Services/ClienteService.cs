using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Application.Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> GetAllAsync();
    Task<ClienteDto?> GetByIdAsync(Guid id);
    Task<ClienteDto> CreateAsync(CreateClienteDto dto);
    Task<ClienteDto?> UpdateAsync(Guid id, UpdateClienteDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<ClienteDto>> GetByStatusAsync(string status);
    Task<IEnumerable<ClienteDto>> GetAtivosAsync();
}

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> GetByIdAsync(Guid id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        return cliente != null ? _mapper.Map<ClienteDto>(cliente) : null;
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto dto)
    {
        // Verificar se Email já existe
        var existingEmail = await _repository.GetByEmailAsync(dto.Email);
        if (existingEmail != null)
            throw new InvalidOperationException("Email já cadastrado");

        var cliente = _mapper.Map<Cliente>(dto);
        var createdCliente = await _repository.AddAsync(cliente);
        return _mapper.Map<ClienteDto>(createdCliente);
    }

    public async Task<ClienteDto?> UpdateAsync(Guid id, UpdateClienteDto dto)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            return null;

        // Verificar se email já existe em outro cliente
        var existingEmail = await _repository.GetByEmailAsync(dto.Email);
        if (existingEmail != null && existingEmail.Id != id)
            throw new InvalidOperationException("Email já cadastrado para outro cliente");

        cliente.AtualizarDados(dto.Nome, dto.Email);
        await _repository.UpdateAsync(cliente);
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            return false;

        // Verificar se cliente tem pedidos em andamento
        if (cliente.Pedidos.Any(p => p.Status == StatusPedido.EmAndamento))
            throw new InvalidOperationException("Não é possível excluir cliente com pedidos em andamento");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ClienteDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusCliente>(status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        var clientes = await _repository.GetByStatusAsync(statusEnum);
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<IEnumerable<ClienteDto>> GetAtivosAsync()
    {
        var clientes = await _repository.GetAtivosAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }
}
