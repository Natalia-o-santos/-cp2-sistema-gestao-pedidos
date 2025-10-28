namespace MottuDelivery.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

public interface IClienteRepository : IRepository<Entities.Cliente>
{
    Task<Entities.Cliente?> GetByEmailAsync(string email);
    Task<IEnumerable<Entities.Cliente>> GetByStatusAsync(Enums.StatusCliente status);
    Task<IEnumerable<Entities.Cliente>> GetAtivosAsync();
}

public interface IFuncionarioRepository : IRepository<Entities.Funcionario>
{
    Task<IEnumerable<Entities.Funcionario>> GetByStatusAsync(Enums.StatusFuncionario status);
    Task<IEnumerable<Entities.Funcionario>> GetByCargoAsync(string cargo);
    Task<IEnumerable<Entities.Funcionario>> GetDisponiveisAsync();
}

public interface IPedidoRepository : IRepository<Entities.Pedido>
{
    Task<IEnumerable<Entities.Pedido>> GetByClienteIdAsync(Guid clienteId);
    Task<IEnumerable<Entities.Pedido>> GetByStatusAsync(Enums.StatusPedido status);
    Task<IEnumerable<Entities.Pedido>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<Entities.Pedido>> GetPedidosPendentesAsync();
    Task<IEnumerable<Entities.Pedido>> GetByFuncionarioIdAsync(Guid funcionarioId);
}
