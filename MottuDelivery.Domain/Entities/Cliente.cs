namespace MottuDelivery.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataUltimaAtualizacao { get; private set; }
    public Enums.StatusCliente Status { get; private set; }

    // Navigation Properties
    public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();

    // Construtor privado para EF Core
    private Cliente() { }

    public Cliente(string nome, string email)
    {
        Id = Guid.NewGuid();
        Nome = ValidarNome(nome);
        Email = ValidarEmail(email);
        DataCadastro = DateTime.UtcNow;
        Status = Enums.StatusCliente.Ativo;
    }

    public void AtualizarDados(string nome, string email)
    {
        Nome = ValidarNome(nome);
        Email = ValidarEmail(email);
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void AlterarStatus(Enums.StatusCliente novoStatus)
    {
        Status = novoStatus;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Status = Enums.StatusCliente.Ativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Inativar()
    {
        if (Pedidos.Any(p => p.Status == Enums.StatusPedido.EmAndamento))
            throw new InvalidOperationException("Não é possível inativar cliente com pedidos em andamento");
        
        Status = Enums.StatusCliente.Inativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public bool PodeFazerPedidos()
    {
        return Status == Enums.StatusCliente.Ativo;
    }

    public int TotalPedidos()
    {
        return Pedidos.Count;
    }

    public int PedidosConcluidos()
    {
        return Pedidos.Count(p => p.Status == Enums.StatusPedido.Concluido);
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório", nameof(nome));
        
        if (nome.Length < 2)
            throw new ArgumentException("Nome deve ter pelo menos 2 caracteres", nameof(nome));
        
        if (nome.Length > 100)
            throw new ArgumentException("Nome deve ter no máximo 100 caracteres", nameof(nome));

        return nome.Trim();
    }

    private static string ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email é obrigatório", nameof(email));

        if (!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("Email deve ter formato válido", nameof(email));

        return email.Trim().ToLowerInvariant();
    }
}
