using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioData _UsuarioData;

        public UsuarioController(UsuarioData UsuarioData)
        {
            _UsuarioData = UsuarioData;
        }

        [HttpGet]
        public async Task<IActionResult>Lista()
        {
            List<Usuario> Lista = await _UsuarioData.Lista();
            return StatusCode(StatusCodes.Status200OK,Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Usuario objeto = await _UsuarioData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Usuario objeto)
        {
            bool respuesta = await _UsuarioData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Usuario objeto)
        {
            bool respuesta = await _UsuarioData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _UsuarioData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
