using AutoMapper;
using ConexusPruebaAPI.Dto.Cliente;
using ConexusPruebaAPI.Dto.Factura;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexusPruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacturaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FacturaGetDto>>> Get()
        {
            var results = await _unitOfWork.Facturas.GetAllAsync("ObtenerFactura");
            return _mapper.Map<List<FacturaGetDto>>(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FacturaGetDto>> Get(int id)
        {
            var result = await _unitOfWork.Facturas.GetByIdAsync(id, "ObtenerFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<FacturaGetDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(FacturaDto facturaDto)
        {
            var factura = _mapper.Map<Factura>(facturaDto);

            int resultado = await _unitOfWork.Facturas.InsertarFactura(factura);

            if (resultado <= 0)
            {
                return BadRequest("Error al insertar la factura.");
            }

            return CreatedAtAction(nameof(Post), new { id = factura.Id }, facturaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FacturaDto>> Put(int id, [FromBody] FacturaDto facturaDto)
        {
            var facturaExistente = await _unitOfWork.Facturas.GetByIdAsync(id, "ObtenerFacturaId");
            if (facturaExistente == null)
            {
                return NotFound();
            }

            facturaExistente.Fecha = facturaDto.Fecha;
            facturaExistente.Total = facturaDto.Total;
            facturaExistente.ClienteId = facturaDto.ClienteId;

            var resultado = await _unitOfWork.Facturas.ActualizarFactura(facturaExistente);

            if (resultado == 0)
            {
                return BadRequest("No se pudo actualizar la factura.");
            }
            return Ok(_mapper.Map<FacturaDto>(facturaExistente));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var facturaExistente = await _unitOfWork.Facturas.GetByIdAsync(id, "ObtenerFacturaId");
            if (facturaExistente == null)
            {
                return NotFound();
            }
            var resultado = await _unitOfWork.Facturas.EliminarFactura(id);
            if (resultado == 0)
            {
                return BadRequest("No se pudo eliminar la factura.");
            }
            return NoContent();
        }
    }
}
