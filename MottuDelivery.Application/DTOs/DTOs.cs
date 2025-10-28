namespace MottuDelivery.Application.DTOs;

public class EnderecoDto
{
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
}

public class EntregadorDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public int TotalEntregas { get; set; }
}

public class CreateEntregadorDto
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateEntregadorDto
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class EntregaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public EnderecoDto EnderecoOrigem { get; set; } = null!;
    public EnderecoDto EnderecoDestino { get; set; } = null!;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }
    public string? Observacoes { get; set; }
    public Guid EntregadorId { get; set; }
    public string EntregadorNome { get; set; } = string.Empty;
    public TimeSpan? TempoTotalEntrega { get; set; }
}

public class CreateEntregaDto
{
    public string Descricao { get; set; } = string.Empty;
    public EnderecoDto EnderecoOrigem { get; set; } = null!;
    public EnderecoDto EnderecoDestino { get; set; } = null!;
    public Guid EntregadorId { get; set; }
    public string? Observacoes { get; set; }
}

public class UpdateEntregaStatusDto
{
    public string Status { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
}

public class UpdateEntregaObservacoesDto
{
    public string Observacoes { get; set; } = string.Empty;
}
