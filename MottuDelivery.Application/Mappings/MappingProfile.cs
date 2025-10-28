using AutoMapper;
using MottuDelivery.Application.DTOs;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.ValueObjects;

namespace MottuDelivery.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entregador mappings
        CreateMap<Entregador, EntregadorDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.TotalEntregas, opt => opt.MapFrom(src => src.Entregas.Count));

        CreateMap<CreateEntregadorDto, Entregador>()
            .ConstructUsing(src => new Entregador(src.Nome, src.Cpf, src.Telefone, src.Email));

        CreateMap<UpdateEntregadorDto, Entregador>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Endereco mappings
        CreateMap<Endereco, EnderecoDto>();
        CreateMap<EnderecoDto, Endereco>()
            .ConstructUsing(src => new Endereco(
                src.Logradouro,
                src.Numero,
                src.Complemento,
                src.Bairro,
                src.Cidade,
                src.Estado,
                src.Cep));

        // Entrega mappings
        CreateMap<Entrega, EntregaDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.EntregadorNome, opt => opt.MapFrom(src => src.Entregador.Nome))
            .ForMember(dest => dest.TempoTotalEntrega, opt => opt.MapFrom(src => src.TempoTotalEntrega()));

        CreateMap<CreateEntregaDto, Entrega>()
            .ConstructUsing(src => new Entrega(
                src.Descricao,
                new Endereco(
                    src.EnderecoOrigem.Logradouro,
                    src.EnderecoOrigem.Numero,
                    src.EnderecoOrigem.Complemento,
                    src.EnderecoOrigem.Bairro,
                    src.EnderecoOrigem.Cidade,
                    src.EnderecoOrigem.Estado,
                    src.EnderecoOrigem.Cep),
                new Endereco(
                    src.EnderecoDestino.Logradouro,
                    src.EnderecoDestino.Numero,
                    src.EnderecoDestino.Complemento,
                    src.EnderecoDestino.Bairro,
                    src.EnderecoDestino.Cidade,
                    src.EnderecoDestino.Estado,
                    src.EnderecoDestino.Cep),
                src.EntregadorId,
                src.Observacoes));

        // Status mappings
        CreateMap<string, StatusEntrega>()
            .ConvertUsing(src => Enum.Parse<StatusEntrega>(src, true));

        CreateMap<string, StatusEntregador>()
            .ConvertUsing(src => Enum.Parse<StatusEntregador>(src, true));
    }
}
