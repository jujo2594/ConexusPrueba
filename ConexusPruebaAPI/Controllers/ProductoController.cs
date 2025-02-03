using AutoMapper;
using ConexusPruebaAPI.Dto.Cliente;
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
        public async Task<ActionResult> Post(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);

            int resultado = await _unitOfWork.Productos.InsertarProducto(producto);

            if (resultado <= 0)
            {
                return BadRequest("Error al insertar el producto.");
            }

            return CreatedAtAction(nameof(Post), new { id = producto.Id }, productoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto productoDto)
        {
            var productoExistente = await _unitOfWork.Productos.GetByIdAsync(id, "ObtenerProductosId");
            if (productoExistente == null)
            {
                return NotFound();
            }

            productoExistente.Nombre = productoDto.Nombre;
            productoExistente.Precio = productoDto.Precio;
            productoExistente.Stock = productoDto.Stock;

            var resultado = await _unitOfWork.Productos.ActualizarProducto(productoExistente);

            if (resultado == 0)
            {
                return BadRequest("No se pudo actualizar el cliente.");
            }
            return Ok(_mapper.Map<ProductoDto>(productoExistente));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var productoExistente = await _unitOfWork.Productos.GetByIdAsync(id, "ObtenerProductosId");
            if (productoExistente == null)
            {
                return NotFound();
            }
            var resultado = await _unitOfWork.Productos.EliminarProducto(id);
            if (resultado == 0)
            {
                return BadRequest("No se pudo eliminar el producto.");
            }
            return NoContent();
        }
    }
}
