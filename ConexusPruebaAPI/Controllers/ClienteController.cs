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
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Post(ClienteDto resultDto)
        {
            var result = _mapper.Map<Cliente>(resultDto);
            _unitOfWork.Clientes.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }   
            var results = await _unitOfWork.Clientes.GetAllAsync("ObtenerClientes");
            return _mapper.Map<List<ClienteDto>>(results);
            //resultDto.Id = resultDto.Id;
            //return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto resultDto)
        {
            var result = await _unitOfWork.Clientes.GetByIdAsync(id, "ObtenerClientesId");
            if (result == null)
            {
                return NotFound();
            }
            result.Telefono = resultDto.Telefono;
            result.Correo = resultDto.Correo;
            result.Nombre = resultDto.Nombre;
            _mapper.Map(resultDto, result);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ClienteDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Clientes.GetByIdAsync(id, "ObtenerClientesId");
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
