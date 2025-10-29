using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;

namespace MottuDelivery.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cliente mappings
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.TotalPedidos, opt => opt.MapFrom(src => src.Pedidos.Count));

        CreateMap<CreateClienteDto, Cliente>()
            .ConstructUsing(src => new Cliente(src.Nome, src.Email));

        CreateMap<UpdateClienteDto, Cliente>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Funcionario mappings
        CreateMap<Funcionario, FuncionarioDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.TotalPedidos, opt => opt.MapFrom(src => src.Pedidos.Count));

        CreateMap<CreateFuncionarioDto, Funcionario>()
            .ConstructUsing(src => new Funcionario(src.Nome, src.Cargo, src.Salario));

        CreateMap<UpdateFuncionarioDto, Funcionario>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Pedido mappings
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome))
            .ForMember(dest => dest.FuncionariosNomes, opt => opt.MapFrom(src => src.Funcionarios.Select(f => f.Nome)));

        CreateMap<CreatePedidoDto, Pedido>()
            .ConstructUsing(src => new Pedido(src.Descricao, src.ClienteId, src.ValorTotal, src.Observacoes));

        CreateMap<UpdatePedidoDto, Pedido>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Status mappings
        CreateMap<string, Domain.Enums.StatusPedido>()
            .ConvertUsing(src => Enum.Parse<Domain.Enums.StatusPedido>(src, true));

        CreateMap<string, Domain.Enums.StatusFuncionario>()
            .ConvertUsing(src => Enum.Parse<Domain.Enums.StatusFuncionario>(src, true));

        CreateMap<string, Domain.Enums.StatusCliente>()
            .ConvertUsing(src => Enum.Parse<Domain.Enums.StatusCliente>(src, true));
    }
}
