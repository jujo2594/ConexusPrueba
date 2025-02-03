using AutoMapper;
using ConexusPruebaAPI.Dto.Cliente;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexusPruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteGetDto>>> Get()
        {
            var results = await _unitOfWork.Clientes.GetAllAsync("ObtenerClientes");
            return _mapper.Map<List<ClienteGetDto>>(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteGetDto>> Get(int id)
        {
            var result = await _unitOfWork.Clientes.GetByIdAsync(id, "ObtenerClientesId");
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<ClienteGetDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);

            int resultado = await _unitOfWork.Clientes.InsertarCliente(cliente);

            if (resultado <= 0)
            {
                return BadRequest("Error al insertar el cliente.");
            }
            return CreatedAtAction(nameof(Post), new { id = cliente.Id }, clienteDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clienteDto)
        {
            var clienteExistente = await _unitOfWork.Clientes.GetByIdAsync(id, "ObtenerClientesId");
            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.Nombre = clienteDto.Nombre;
            clienteExistente.Correo = clienteDto.Correo;
            clienteExistente.Telefono = clienteDto.Telefono;

            var resultado = await _unitOfWork.Clientes.ActualizarCliente(clienteExistente);

            if (resultado == 0)
            {
                return BadRequest("No se pudo actualizar el cliente.");
            }
            return Ok(_mapper.Map<ClienteDto>(clienteExistente));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteExistente = await _unitOfWork.Clientes.GetByIdAsync(id, "ObtenerClientesId");
            if (clienteExistente == null)
            {
                return NotFound();
            }
            var resultado = await _unitOfWork.Clientes.EliminarCliente(id);
            if (resultado == 0) 
            {
                return BadRequest("No se pudo eliminar el cliente.");
            }
            return NoContent(); 
        }

    }
}
