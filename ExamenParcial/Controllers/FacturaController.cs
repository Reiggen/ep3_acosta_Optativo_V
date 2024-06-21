using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Repository.Data;
using Services.Logica;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using Repository.Context;

namespace api.personas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : Controller
    {
        private readonly FacturaService _facturaService;
        private readonly IValidator<FacturaModel> _validator;


        public FacturaController(ContextoAplicacionDB context, IValidator<FacturaModel> validator)
        {
            _facturaService = new FacturaService(context);
            _validator = validator;
        }

        [HttpGet("Listar Facturas")]
        public async Task<ActionResult> ListarAsync()
        {
            try
            {
                var facturas = await _facturaService.ListarAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Consultar Factura/{id}")]
        public async Task<ActionResult> ConsultarAsync(int id)
        {
            var factura = await _facturaService.ConsultarAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        [HttpPost("Agregar Factura")]
        public async Task<ActionResult> AgregarAsync(FacturaModel factura)
        {
            var validationResult = await _validator.ValidateAsync(factura);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            await _facturaService.AgregarAsync(factura);
            return Ok("La factura ha sido agregada correctamente");
        }/*
            if (ModelState.IsValid)
            {
                await facturaService.AgregarAsync(factura);
                return View();
            }
            else
            {
                return BadRequest(ModelState);
            }*/

        [HttpPut("Modificar factura")]
        public async Task<ActionResult> ModificarAsync(int id, FacturaModel factura)
        {
            if (id != factura.id)
            {
                return BadRequest("ID mismatch");
            }
            var validationResult = await _validator.ValidateAsync(factura);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _facturaService.ModificarAsync(factura);
            return NoContent();
        }

        [HttpDelete("Eliminar Factura/{id}")]
        public async Task<ActionResult> EliminarAsync(int id)
        {
            var factura = await _facturaService.ConsultarAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            await _facturaService.EliminarAsync(id);
            return NoContent();
        }
    }
}

