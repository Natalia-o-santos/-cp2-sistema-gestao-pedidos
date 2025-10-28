using FluentValidation;

namespace MottuDelivery.Application.Validators;

public class CreateEntregadorDtoValidator : AbstractValidator<DTOs.CreateEntregadorDto>
{
    public CreateEntregadorDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF é obrigatório")
            .Length(11).WithMessage("CPF deve ter 11 dígitos")
            .Matches(@"^\d{11}$").WithMessage("CPF deve conter apenas números");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve ter 10 ou 11 dígitos");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter formato válido");
    }
}

public class UpdateEntregadorDtoValidator : AbstractValidator<DTOs.UpdateEntregadorDto>
{
    public UpdateEntregadorDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve ter 10 ou 11 dígitos");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter formato válido");
    }
}

public class CreateEntregaDtoValidator : AbstractValidator<DTOs.CreateEntregaDto>
{
    public CreateEntregaDtoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(5).WithMessage("Descrição deve ter pelo menos 5 caracteres")
            .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

        RuleFor(x => x.EntregadorId)
            .NotEmpty().WithMessage("Entregador é obrigatório");

        RuleFor(x => x.EnderecoOrigem)
            .NotNull().WithMessage("Endereço de origem é obrigatório");

        RuleFor(x => x.EnderecoDestino)
            .NotNull().WithMessage("Endereço de destino é obrigatório");

        RuleFor(x => x.EnderecoOrigem.Logradouro)
            .NotEmpty().WithMessage("Logradouro de origem é obrigatório");

        RuleFor(x => x.EnderecoOrigem.Numero)
            .NotEmpty().WithMessage("Número de origem é obrigatório");

        RuleFor(x => x.EnderecoOrigem.Bairro)
            .NotEmpty().WithMessage("Bairro de origem é obrigatório");

        RuleFor(x => x.EnderecoOrigem.Cidade)
            .NotEmpty().WithMessage("Cidade de origem é obrigatória");

        RuleFor(x => x.EnderecoOrigem.Estado)
            .NotEmpty().WithMessage("Estado de origem é obrigatório")
            .Length(2).WithMessage("Estado deve ter 2 caracteres");

        RuleFor(x => x.EnderecoOrigem.Cep)
            .NotEmpty().WithMessage("CEP de origem é obrigatório")
            .Matches(@"^\d{8}$").WithMessage("CEP de origem deve ter 8 dígitos");

        RuleFor(x => x.EnderecoDestino.Logradouro)
            .NotEmpty().WithMessage("Logradouro de destino é obrigatório");

        RuleFor(x => x.EnderecoDestino.Numero)
            .NotEmpty().WithMessage("Número de destino é obrigatório");

        RuleFor(x => x.EnderecoDestino.Bairro)
            .NotEmpty().WithMessage("Bairro de destino é obrigatório");

        RuleFor(x => x.EnderecoDestino.Cidade)
            .NotEmpty().WithMessage("Cidade de destino é obrigatória");

        RuleFor(x => x.EnderecoDestino.Estado)
            .NotEmpty().WithMessage("Estado de destino é obrigatório")
            .Length(2).WithMessage("Estado deve ter 2 caracteres");

        RuleFor(x => x.EnderecoDestino.Cep)
            .NotEmpty().WithMessage("CEP de destino é obrigatório")
            .Matches(@"^\d{8}$").WithMessage("CEP de destino deve ter 8 dígitos");
    }
}

public class UpdateEntregaStatusDtoValidator : AbstractValidator<DTOs.UpdateEntregaStatusDto>
{
    public UpdateEntregaStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório")
            .Must(BeValidStatus).WithMessage("Status deve ser: Pendente, EmAndamento, Concluida ou Cancelada");
    }

    private static bool BeValidStatus(string status)
    {
        return Enum.TryParse<Domain.Enums.StatusEntrega>(status, true, out _);
    }
}
