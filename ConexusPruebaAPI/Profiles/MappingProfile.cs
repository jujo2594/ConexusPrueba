using AutoMapper;
using ConexusPruebaAPI.Dto.Cliente;
using ConexusPruebaAPI.Dto.DetalleFactura;
using ConexusPruebaAPI.Dto.Factura;
using ConexusPruebaAPI.Dto.Producto;
using Domain.Entities;

namespace ConexusPruebaAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Cliente, ClienteGetDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, ProductoGetDto>().ReverseMap();
            CreateMap<Factura, FacturaDto>().ReverseMap();
            CreateMap<Factura, FacturaGetDto>().ReverseMap();
            CreateMap<DetalleFactura, DetalleFacturaDto>().ReverseMap();
            CreateMap<DetalleFactura, DetalleFacturaGetDto>().ReverseMap();
        }
    }
}
