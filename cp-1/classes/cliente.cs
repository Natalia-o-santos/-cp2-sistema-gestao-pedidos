namespace cp;

public class cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<pedido> Pedidos { get; set; } = new();
}