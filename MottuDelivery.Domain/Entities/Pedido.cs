namespace MottuDelivery.Domain.Entities;

// Evolução da classe 'pedido' do CP1
// CP1: public class pedido { public Guid Id { get; set; } = Guid.NewGuid(); public DateTime Data { get; set; } = DateTime.Now; public Guid UsuarioId { get; set; } public cliente? Usuario { get; set; } public List<funcionario> Funcionarios { get; set; } = new(); }
public class Pedido
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public Enums.StatusPedido Status { get; private set; }
    public DateTime DataCriacao { get; private set; } // Evolução do CP1: DateTime Data
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataConclusao { get; private set; }
    public string? Observacoes { get; private set; }
    public decimal ValorTotal { get; private set; }

    // Foreign Keys (evolução do CP1: Guid UsuarioId)
    public Guid ClienteId { get; private set; } // Renomeado de UsuarioId para ClienteId

    // Navigation Properties (evolução do CP1: cliente? Usuario e List<funcionario> Funcionarios)
    public Cliente Cliente { get; private set; } = null!; // Evolução: cliente? Usuario
    public ICollection<Funcionario> Funcionarios { get; private set; } = new List<Funcionario>(); // Mantém: List<funcionario> Funcionarios

    // Construtor privado para EF Core
    private Pedido() { }

    // Evolução: Adiciona validações e comportamentos ao construtor simples do CP1
    public Pedido(string descricao, Guid clienteId, decimal valorTotal, string? observacoes = null)
    {
        Id = Guid.NewGuid();
        Descricao = ValidarDescricao(descricao);
        ClienteId = clienteId; // Evolução do CP1: UsuarioId
        ValorTotal = ValidarValorTotal(valorTotal);
        Observacoes = observacoes;
        Status = Enums.StatusPedido.Pendente;
        DataCriacao = DateTime.UtcNow; // Mantém a lógica do CP1: DateTime.Now
    }

    public void IniciarProcessamento()
    {
        if (Status != Enums.StatusPedido.Pendente)
            throw new InvalidOperationException("Apenas pedidos pendentes podem ser iniciados");

        Status = Enums.StatusPedido.EmAndamento;
        DataInicio = DateTime.UtcNow;
    }

    public void ConcluirPedido(string? observacoesConclusao = null)
    {
        if (Status != Enums.StatusPedido.EmAndamento)
            throw new InvalidOperationException("Apenas pedidos em andamento podem ser concluídos");

        Status = Enums.StatusPedido.Concluido;
        DataConclusao = DateTime.UtcNow;
        
        if (!string.IsNullOrWhiteSpace(observacoesConclusao))
            Observacoes = observacoesConclusao;
    }

    public void CancelarPedido(string motivoCancelamento)
    {
        if (Status == Enums.StatusPedido.Concluido)
            throw new InvalidOperationException("Pedidos concluídos não podem ser cancelados");

        Status = Enums.StatusPedido.Cancelado;
        DataConclusao = DateTime.UtcNow;
        Observacoes = $"Cancelado: {motivoCancelamento}";
    }

    public void AtualizarObservacoes(string observacoes)
    {
        if (string.IsNullOrWhiteSpace(observacoes))
            throw new ArgumentException("Observações não podem ser vazias", nameof(observacoes));

        Observacoes = observacoes;
    }

    public void AdicionarFuncionario(Funcionario funcionario)
    {
        if (funcionario == null)
            throw new ArgumentNullException(nameof(funcionario));

        if (!funcionario.PodeTrabalharEmPedidos())
            throw new InvalidOperationException("Funcionário não está disponível para trabalhar em pedidos");

        if (Funcionarios.Contains(funcionario))
            throw new InvalidOperationException("Funcionário já está atribuído a este pedido");

        Funcionarios.Add(funcionario);
    }

    public void RemoverFuncionario(Funcionario funcionario)
    {
        if (funcionario == null)
            throw new ArgumentNullException(nameof(funcionario));

        if (!Funcionarios.Contains(funcionario))
            throw new InvalidOperationException("Funcionário não está atribuído a este pedido");

        Funcionarios.Remove(funcionario);
    }

    public TimeSpan? TempoTotalProcessamento()
    {
        if (DataInicio == null || DataConclusao == null)
            return null;

        return DataConclusao.Value - DataInicio.Value;
    }

    public bool EstaEmAndamento()
    {
        return Status == Enums.StatusPedido.EmAndamento;
    }

    public bool FoiConcluido()
    {
        return Status == Enums.StatusPedido.Concluido;
    }

    public bool FoiCancelado()
    {
        return Status == Enums.StatusPedido.Cancelado;
    }

    public int QuantidadeFuncionarios()
    {
        return Funcionarios.Count;
    }

    private static string ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória", nameof(descricao));
        
        if (descricao.Length < 5)
            throw new ArgumentException("Descrição deve ter pelo menos 5 caracteres", nameof(descricao));
        
        if (descricao.Length > 500)
            throw new ArgumentException("Descrição deve ter no máximo 500 caracteres", nameof(descricao));

        return descricao.Trim();
    }

    private static decimal ValidarValorTotal(decimal valorTotal)
    {
        if (valorTotal <= 0)
            throw new ArgumentException("Valor total deve ser maior que zero", nameof(valorTotal));
        
        if (valorTotal > 1000000)
            throw new ArgumentException("Valor total não pode ser maior que R$ 1.000.000", nameof(valorTotal));

        return valorTotal;
    }
}
