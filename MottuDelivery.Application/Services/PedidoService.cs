using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Application.Services;

public interface IPedidoService
{
    Task<IEnumerable<PedidoDto>> GetAllAsync();
    Task<PedidoDto?> GetByIdAsync(Guid id);
    Task<PedidoDto> CreateAsync(CreatePedidoDto dto);
    Task<PedidoDto?> UpdateAsync(Guid id, UpdatePedidoDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<PedidoDto>> GetByClienteIdAsync(Guid clienteId);
    Task<IEnumerable<PedidoDto>> GetByStatusAsync(string status);
    Task<IEnumerable<PedidoDto>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<PedidoDto>> GetPedidosPendentesAsync();
    Task<IEnumerable<PedidoDto>> GetByFuncionarioIdAsync(Guid funcionarioId);
}

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMapper _mapper;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _funcionarioRepository = funcionarioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PedidoDto>> GetAllAsync()
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<PedidoDto?> GetByIdAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        return pedido != null ? _mapper.Map<PedidoDto>(pedido) : null;
    }

    public async Task<PedidoDto> CreateAsync(CreatePedidoDto dto)
    {
        // Verificar se cliente existe
        var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
        if (cliente == null)
            throw new InvalidOperationException("Cliente não encontrado");

        var pedido = _mapper.Map<Pedido>(dto);
        var createdPedido = await _pedidoRepository.AddAsync(pedido);
        return _mapper.Map<PedidoDto>(createdPedido);
    }

    public async Task<PedidoDto?> UpdateAsync(Guid id, UpdatePedidoDto dto)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido == null)
            return null;

        if (!Enum.TryParse<StatusPedido>(dto.Status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        switch (statusEnum)
        {
            case StatusPedido.EmAndamento:
                pedido.IniciarProcessamento();
                break;
            case StatusPedido.Concluido:
                pedido.ConcluirPedido(dto.Observacoes);
                break;
            case StatusPedido.Cancelado:
                if (string.IsNullOrWhiteSpace(dto.Observacoes))
                    throw new ArgumentException("Motivo do cancelamento é obrigatório");
                pedido.CancelarPedido(dto.Observacoes);
                break;
            default:
                throw new ArgumentException("Status não pode ser alterado diretamente");
        }

        await _pedidoRepository.UpdateAsync(pedido);
        return _mapper.Map<PedidoDto>(pedido);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido == null)
            return false;

        // Não permitir exclusão de pedidos em andamento ou concluídos
        if (pedido.Status == StatusPedido.EmAndamento || pedido.Status == StatusPedido.Concluido)
            throw new InvalidOperationException("Não é possível excluir pedidos em andamento ou concluídos");

        await _pedidoRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<PedidoDto>> GetByClienteIdAsync(Guid clienteId)
    {
        var pedidos = await _pedidoRepository.GetByClienteIdAsync(clienteId);
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<IEnumerable<PedidoDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusPedido>(status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        var pedidos = await _pedidoRepository.GetByStatusAsync(statusEnum);
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<IEnumerable<PedidoDto>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var pedidos = await _pedidoRepository.GetByPeriodoAsync(dataInicio, dataFim);
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<IEnumerable<PedidoDto>> GetPedidosPendentesAsync()
    {
        var pedidos = await _pedidoRepository.GetPedidosPendentesAsync();
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<IEnumerable<PedidoDto>> GetByFuncionarioIdAsync(Guid funcionarioId)
    {
        var pedidos = await _pedidoRepository.GetByFuncionarioIdAsync(funcionarioId);
        return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }
}
