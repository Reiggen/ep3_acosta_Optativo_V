using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Repository.Data;
using Services.Logica;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace ExamenParcial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly IValidator<ClienteModel> _validator;

        public ClienteController(Repository.Context.ContextoAplicacionDB context, IValidator<ClienteModel> validator)
        {
            _clienteService = new ClienteService(context);
            _validator = validator;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> ListarAsync()
        {
            try
            {
                var clientes = await _clienteService.ListarAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Consultar/{id}")]
        public async Task<ActionResult<ClienteModel>> ConsultarAsync(int id)
        {
            var cliente = await _clienteService.ConsultarAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult> AgregarAsync(ClienteModel cliente)
        {
            ValidationResult result = await _validator.ValidateAsync(cliente);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            await _clienteService.AgregarAsync(cliente);
            return Ok();
        }

        [HttpPut("Modificar/{id}")]
        public async Task<ActionResult> ModificarAsync(int id, ClienteModel cliente)
        {
            if (id != cliente.id)
            {
                return BadRequest();
            }

            ValidationResult result = await _validator.ValidateAsync(cliente);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            await _clienteService.ModificarAsync(cliente);
            return NoContent();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> EliminarAsync(int id)
        {
            var cliente = await _clienteService.ConsultarAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _clienteService.EliminarAsync(cliente);
            return NoContent();
        }
    }
}


