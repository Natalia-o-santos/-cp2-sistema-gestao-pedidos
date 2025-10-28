namespace MottuDelivery.Domain.Entities;

public class Funcionario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cargo { get; private set; } = string.Empty;
    public decimal Salario { get; private set; }
    public DateTime DataContratacao { get; private set; }
    public Enums.StatusFuncionario Status { get; private set; }
    public DateTime? DataUltimaAtualizacao { get; private set; }

    // Navigation Properties
    public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();

    // Construtor privado para EF Core
    private Funcionario() { }

    public Funcionario(string nome, string cargo, decimal salario)
    {
        Id = Guid.NewGuid();
        Nome = ValidarNome(nome);
        Cargo = ValidarCargo(cargo);
        Salario = ValidarSalario(salario);
        DataContratacao = DateTime.UtcNow;
        Status = Enums.StatusFuncionario.Ativo;
    }

    public void AtualizarDados(string nome, string cargo, decimal salario)
    {
        Nome = ValidarNome(nome);
        Cargo = ValidarCargo(cargo);
        Salario = ValidarSalario(salario);
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void AlterarStatus(Enums.StatusFuncionario novoStatus)
    {
        Status = novoStatus;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Status = Enums.StatusFuncionario.Ativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Inativar()
    {
        if (Status == Enums.StatusFuncionario.EmPedido)
            throw new InvalidOperationException("Não é possível inativar um funcionário que está trabalhando em pedidos");
        
        Status = Enums.StatusFuncionario.Inativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public bool PodeTrabalharEmPedidos()
    {
        return Status == Enums.StatusFuncionario.Ativo;
    }

    public int AnosDeEmpresa()
    {
        return DateTime.UtcNow.Year - DataContratacao.Year;
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

    private static string ValidarCargo(string cargo)
    {
        if (string.IsNullOrWhiteSpace(cargo))
            throw new ArgumentException("Cargo é obrigatório", nameof(cargo));
        
        if (cargo.Length < 2)
            throw new ArgumentException("Cargo deve ter pelo menos 2 caracteres", nameof(cargo));
        
        if (cargo.Length > 50)
            throw new ArgumentException("Cargo deve ter no máximo 50 caracteres", nameof(cargo));

        return cargo.Trim();
    }

    private static decimal ValidarSalario(decimal salario)
    {
        if (salario <= 0)
            throw new ArgumentException("Salário deve ser maior que zero", nameof(salario));
        
        if (salario > 100000)
            throw new ArgumentException("Salário não pode ser maior que R$ 100.000", nameof(salario));

        return salario;
    }
}
