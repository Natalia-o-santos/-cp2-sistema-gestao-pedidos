using FluentValidation;

namespace MottuDelivery.Application.Validators;

// Validators para Cliente (evolução do CP1)
public class CreateClienteDtoValidator : AbstractValidator<DTOs.CreateClienteDto>
{
    public CreateClienteDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter formato válido");
    }
}

public class UpdateClienteDtoValidator : AbstractValidator<DTOs.UpdateClienteDto>
{
    public UpdateClienteDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter formato válido");
    }
}

// Validators para Funcionario (evolução do CP1)
public class CreateFuncionarioDtoValidator : AbstractValidator<DTOs.CreateFuncionarioDto>
{
    public CreateFuncionarioDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Cargo)
            .NotEmpty().WithMessage("Cargo é obrigatório")
            .MaximumLength(50).WithMessage("Cargo deve ter no máximo 50 caracteres");

        RuleFor(x => x.Salario)
            .GreaterThan(0).WithMessage("Salário deve ser maior que zero");
    }
}

public class UpdateFuncionarioDtoValidator : AbstractValidator<DTOs.UpdateFuncionarioDto>
{
    public UpdateFuncionarioDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Cargo)
            .NotEmpty().WithMessage("Cargo é obrigatório")
            .MaximumLength(50).WithMessage("Cargo deve ter no máximo 50 caracteres");

        RuleFor(x => x.Salario)
            .GreaterThan(0).WithMessage("Salário deve ser maior que zero");
    }
}

// Validators para Pedido (evolução do CP1)
public class CreatePedidoDtoValidator : AbstractValidator<DTOs.CreatePedidoDto>
{
    public CreatePedidoDtoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(5).WithMessage("Descrição deve ter pelo menos 5 caracteres")
            .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("Cliente é obrigatório");

        RuleFor(x => x.ValorTotal)
            .GreaterThan(0).WithMessage("Valor total deve ser maior que zero");
    }
}

public class UpdatePedidoDtoValidator : AbstractValidator<DTOs.UpdatePedidoDto>
{
    public UpdatePedidoDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório")
            .Must(BeValidStatus).WithMessage("Status deve ser: Pendente, EmAndamento, Concluido ou Cancelado");
    }

    private static bool BeValidStatus(string status)
    {
        return Enum.TryParse<Domain.Enums.StatusPedido>(status, true, out _);
    }
}
