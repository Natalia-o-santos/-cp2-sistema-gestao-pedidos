using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Application.Services;

public interface IFuncionarioService
{
    Task<IEnumerable<FuncionarioDto>> GetAllAsync();
    Task<FuncionarioDto?> GetByIdAsync(Guid id);
    Task<FuncionarioDto> CreateAsync(CreateFuncionarioDto dto);
    Task<FuncionarioDto?> UpdateAsync(Guid id, UpdateFuncionarioDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<FuncionarioDto>> GetByStatusAsync(string status);
    Task<IEnumerable<FuncionarioDto>> GetByCargoAsync(string cargo);
    Task<IEnumerable<FuncionarioDto>> GetDisponiveisAsync();
}

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repository;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FuncionarioDto>> GetAllAsync()
    {
        var funcionarios = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<FuncionarioDto>>(funcionarios);
    }

    public async Task<FuncionarioDto?> GetByIdAsync(Guid id)
    {
        var funcionario = await _repository.GetByIdAsync(id);
        return funcionario != null ? _mapper.Map<FuncionarioDto>(funcionario) : null;
    }

    public async Task<FuncionarioDto> CreateAsync(CreateFuncionarioDto dto)
    {
        var funcionario = _mapper.Map<Funcionario>(dto);
        var createdFuncionario = await _repository.AddAsync(funcionario);
        return _mapper.Map<FuncionarioDto>(createdFuncionario);
    }

    public async Task<FuncionarioDto?> UpdateAsync(Guid id, UpdateFuncionarioDto dto)
    {
        var funcionario = await _repository.GetByIdAsync(id);
        if (funcionario == null)
            return null;

        funcionario.AtualizarDados(dto.Nome, dto.Cargo, dto.Salario);
        await _repository.UpdateAsync(funcionario);
        return _mapper.Map<FuncionarioDto>(funcionario);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var funcionario = await _repository.GetByIdAsync(id);
        if (funcionario == null)
            return false;

        // Verificar se funcionário tem pedidos em andamento
        if (funcionario.Pedidos.Any(p => p.Status == StatusPedido.EmAndamento))
            throw new InvalidOperationException("Não é possível excluir funcionário com pedidos em andamento");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<FuncionarioDto>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusFuncionario>(status, true, out var statusEnum))
            throw new ArgumentException("Status inválido");

        var funcionarios = await _repository.GetByStatusAsync(statusEnum);
        return _mapper.Map<IEnumerable<FuncionarioDto>>(funcionarios);
    }

    public async Task<IEnumerable<FuncionarioDto>> GetByCargoAsync(string cargo)
    {
        var funcionarios = await _repository.GetByCargoAsync(cargo);
        return _mapper.Map<IEnumerable<FuncionarioDto>>(funcionarios);
    }

    public async Task<IEnumerable<FuncionarioDto>> GetDisponiveisAsync()
    {
        var funcionarios = await _repository.GetDisponiveisAsync();
        return _mapper.Map<IEnumerable<FuncionarioDto>>(funcionarios);
    }
}
