namespace MottuDelivery.Application.DTOs;

// DTOs para Cliente (evolução do CP1)
public class ClienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public int TotalPedidos { get; set; }
}

public class CreateClienteDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateClienteDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

// DTOs para Funcionario (evolução do CP1)
public class FuncionarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public decimal Salario { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataContratacao { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public int TotalPedidos { get; set; }
}

public class CreateFuncionarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public decimal Salario { get; set; }
}

public class UpdateFuncionarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public decimal Salario { get; set; }
}

// DTOs para Pedido (evolução do CP1)
public class PedidoDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    public string? Observacoes { get; set; }
    public decimal ValorTotal { get; set; }
    public Guid ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public IEnumerable<string> FuncionariosNomes { get; set; } = new List<string>();
}

public class CreatePedidoDto
{
    public string Descricao { get; set; } = string.Empty;
    public Guid ClienteId { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacoes { get; set; }
}

public class UpdatePedidoDto
{
    public string Status { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
}
