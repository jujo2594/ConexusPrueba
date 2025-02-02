using AutoMapper;
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
        public async Task<ActionResult<IEnumerable<FacturaDto>>> Post(FacturaDto resultDto)
        {
            var result = _mapper.Map<Factura>(resultDto);
            _unitOfWork.Facturas.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            var results = await _unitOfWork.Facturas.GetAllAsync("ObtenerFactura");
            return _mapper.Map<List<FacturaDto>>(results);
            //resultDto.Id = resultDto.Id;
            //return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FacturaDto>> Put(int id, [FromBody] FacturaDto resultDto)
        {
            var result = await _unitOfWork.Facturas.GetByIdAsync(id, "ObtenerFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            result.Fecha = resultDto.Fecha;
            result.Total = resultDto.Total;
            result.ClienteId = resultDto.ClienteId;
            _mapper.Map(resultDto, result);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<FacturaDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Facturas.GetByIdAsync(id, "ObtenerFacturaId");
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Facturas.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
