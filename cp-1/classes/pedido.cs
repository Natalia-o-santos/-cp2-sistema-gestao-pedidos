namespace cp;

public class pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Data { get; set; } = DateTime.Now;

    public Guid UsuarioId { get; set; }
    public cliente? Usuario { get; set; }

    public List<funcionario> Funcionarios { get; set; } = new();
}