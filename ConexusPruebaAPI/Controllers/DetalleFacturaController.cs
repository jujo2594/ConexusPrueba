using AutoMapper;
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
        public async Task<ActionResult<IEnumerable<DetalleFacturaDto>>> Post(DetalleFacturaDto resultDto)
        {
            var result = _mapper.Map<DetalleFactura>(resultDto);
            _unitOfWork.DetalleFacturas.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            //resultDto.Id = resultDto.Id;
            //return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
            var results = await _unitOfWork.DetalleFacturas.GetAllAsync("ObtenerDetalleFactura");
            return _mapper.Map<List<DetalleFacturaDto>>(results);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleFacturaDto>> Put(int id, [FromBody] DetalleFacturaDto resultDto)
        {
            var result = await _unitOfWork.DetalleFacturas.GetByIdAsync(id, "ObtenerDetalleFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            result.Cantidad = resultDto.Cantidad;
            result.PrecioUnitario = resultDto.PrecioUnitario;
            result.Subtotal = resultDto.Subtotal;
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DetalleFacturaDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.DetalleFacturas.GetByIdAsync(id, "ObtenerDetalleFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.DetalleFacturas.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
