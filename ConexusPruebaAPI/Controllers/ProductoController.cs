using AutoMapper;
using ConexusPruebaAPI.Dto.Producto;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexusPruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoGetDto>>> Get()
        {
            var results = await _unitOfWork.Productos.GetAllAsync("ObtenerProductos");
            return _mapper.Map<List<ProductoGetDto>>(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoGetDto>> Get(int id)
        {
            var result = await _unitOfWork.Productos.GetByIdAsync(id, "ObtenerProductosId");
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductoGetDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Post(ProductoDto resultDto)
        {
            var result = _mapper.Map<Producto>(resultDto);
            _unitOfWork.Productos.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            var results = await _unitOfWork.Productos.GetAllAsync("ObtenerProductos");
            return _mapper.Map<List<ProductoDto>>(results);
            //resultDto.Id = resultDto.Id;
            //return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto resultDto)
        {
            var result = await _unitOfWork.Productos.GetByIdAsync(id, "ObtenerProductosId");
            if (result == null)
            {
                return NotFound();
            }
            result.Nombre = resultDto.Nombre;
            result.Precio = resultDto.Precio;
            result.Stock = resultDto.Stock;
            _mapper.Map(resultDto, result);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductoDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Productos.GetByIdAsync(id, "ObtenerProductosId");
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Productos.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
