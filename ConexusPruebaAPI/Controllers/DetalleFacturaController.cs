using AutoMapper;
using ConexusPruebaAPI.Dto.Cliente;
using ConexusPruebaAPI.Dto.DetalleFactura;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexusPruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetalleFacturaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetalleFacturaGetDto>>> Get()
        {
            var results = await _unitOfWork.DetalleFacturas.GetAllAsync("ObtenerDetalleFactura");
            return _mapper.Map<List<DetalleFacturaGetDto>>(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleFacturaGetDto>> Get(int id)
        {
            var result = await _unitOfWork.DetalleFacturas.GetByIdAsync(id, "ObtenerDetalleFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<DetalleFacturaGetDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(DetalleFacturaDto detalleFacturaDto)
        {
            var detalleFactura = _mapper.Map<DetalleFactura>(detalleFacturaDto);

            int resultado = await _unitOfWork.DetalleFacturas.InsertarDetalleFactura(detalleFactura);

            if (resultado <= 0)
            {
                return BadRequest("Error al insertar detalle factura.");
            }

            return CreatedAtAction(nameof(Post), new { id = detalleFactura.Id }, detalleFacturaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleFacturaDto>> Put(int id, [FromBody] DetalleFacturaDto detalleFacturaDto)
        {
            var detalleFacturaExistente = await _unitOfWork.DetalleFacturas.GetByIdAsync(id, "ObtenerDetalleFacturaId");
            if (detalleFacturaExistente == null)
            {
                return NotFound();
            }

            detalleFacturaExistente.Cantidad = detalleFacturaDto.Cantidad;
            detalleFacturaExistente.PrecioUnitario = detalleFacturaDto.PrecioUnitario;
            detalleFacturaExistente.Subtotal = detalleFacturaDto.Subtotal;
            detalleFacturaExistente.FacturaId = detalleFacturaDto.FacturaId;
            detalleFacturaExistente.ProductoId = detalleFacturaDto.ProductoId;

            var resultado = await _unitOfWork.DetalleFacturas.ActualizarDetalleFactura(detalleFacturaExistente);

            if (resultado == 0)
            {
                return BadRequest("No se pudo actualizar el detalle factura.");
            }
            return Ok(_mapper.Map<DetalleFacturaDto>(detalleFacturaExistente));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var detalleFacturaExistente = await _unitOfWork.DetalleFacturas.GetByIdAsync(id, "ObtenerDetalleFacturaId");
            if (detalleFacturaExistente == null)
            {
                return NotFound();
            }
            var resultado = await _unitOfWork.DetalleFacturas.EliminarDetalleFactura(id);
            if (resultado == 0)
            {
                return BadRequest("No se pudo eliminar detalle factura.");
            }
            return NoContent();
        }
    }
}
