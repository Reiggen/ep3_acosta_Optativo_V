using System;
using Repository.Data;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Services.Logica;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExamenParcial.Validaciones
{
    public class ClienteValidation : AbstractValidator<ClienteModel>
    {
        private readonly ClienteService _clienteService;

        public ClienteValidation(ClienteService clienteService)
        {
            _clienteService = clienteService;

            RuleFor(cliente => cliente.nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe contener un minimo de 3 caracteres.");

            RuleFor(cliente => cliente.apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MinimumLength(3).WithMessage("El apellido debe contener un minimo de 3 caracteres.");

            RuleFor(cliente => cliente.celular)
                .MaximumLength(10).WithMessage("El numero de celular debe contener una longitud de 10 digitos.")
                .Matches("^[0-9]+$").WithMessage("El campo solo acepta valores numericos.");

            RuleFor(cliente => cliente.documento)
                .NotEmpty().WithMessage("El numero de cedula es obligatorio.")
                .MinimumLength(7).WithMessage("El numero de cedula debe contener un minimo de 7 caracteres sin puntos")
                .Must(documento => BeUniqueDocumento(documento)).WithMessage("El numero de cedula debe de ser unico");
            
            RuleFor(cliente => cliente.mail)
                .EmailAddress().WithMessage("La dirección de correo es invalida");

            RuleFor(cliente => cliente.estado)
                .NotEmpty().WithMessage("El tipo de estado es obligatorio");

            RuleFor(cliente => cliente)
                .Must(cliente => IsActive(cliente.estado)).WithMessage("Los datos solo se pueden obtener si esta activo.");
        }

        private bool BeUniqueDocumento(string documento)
        {

            return _clienteService.IsDocumentoUnique(documento).Result;
        }

        private bool IsActive(string estado)
        {
            return string.Equals(estado, "activo", StringComparison.OrdinalIgnoreCase);
        }
    }
}