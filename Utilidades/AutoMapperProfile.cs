using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Modelos;
using System.Globalization;

namespace BackEndAPI.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Persona
            CreateMap<Persona, PersonaDTO>().ReverseMap();
            #endregion

            #region Empleado
            CreateMap<Empleado,EmpleadoDTO>()
                .ForMember(destino =>
                destino.NombrePersona,
                opt => opt.MapFrom(origen => origen.Persona.Nombre))
                .ForMember(destino =>
                destino.FechaContrato,
                opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyy"))
                );
            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino => destino.Persona,
                opt => opt.Ignore()
                )
                 .ForMember(destino => destino.FechaContrato,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );
            #endregion


        }
    }
}
