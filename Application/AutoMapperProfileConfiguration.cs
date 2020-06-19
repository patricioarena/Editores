using AutoMapper;
using Dominio.DTOs;
using Dominio.Entities;

namespace Application
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<EscritosTexto, EscritosTextoDto>()
            .ForMember(destino => destino.Titulo, options => options.MapFrom(origen => origen.Titulo))
            .ForMember(destino => destino.Texto, options => options.MapFrom(origen => origen.Texto))
            .ReverseMap();
        }

    }
}