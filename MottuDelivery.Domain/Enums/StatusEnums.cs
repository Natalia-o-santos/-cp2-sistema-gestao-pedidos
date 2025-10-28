namespace MottuDelivery.Domain.Enums;

public enum StatusPedido
{
    Pendente = 1,
    EmAndamento = 2,
    Concluido = 3,
    Cancelado = 4
}

public enum StatusFuncionario
{
    Ativo = 1,
    Inativo = 2,
    EmPedido = 3
}

public enum StatusCliente
{
    Ativo = 1,
    Inativo = 2,
    Bloqueado = 3
}
