using Repository.Data;
using System;
using System.Globalization;
using FluentValidation;


namespace ExamenParcial.Validaciones
{
    public class FacturaValidation : AbstractValidator<FacturaModel>
    {
        public FacturaValidation()
        {
            //Nro. Factura de tipo cadena con patrones como 3 primeros caracteres con datos numéricos, 4to carácter guión, posiciones del 5 al 7 con datos numéricos, 8va posición con guión, 6 caracteres últimos con datos numéricos.
            RuleFor(factura => factura.nro_factura)
                .NotEmpty().WithMessage("El numero de factura es obligatorio")
                .Matches("^[0-9]{3}-[0-9]{3}-[0-9]{6}$").WithMessage("El numero de factura debe contener el formato correcto ej:XXX-XXX-XXXXXX.");

            //Total, Total_iva5, Total_iva10, Total_iva datos de tipo numéricos obligatorios.
            RuleFor(factura => factura.total)
                .NotEmpty().WithMessage("Total es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("Total tiene que ser un valor positivo")
                .Must(BeNumeric).WithMessage("Total tiene que ser valores numericos");

            RuleFor(factura => factura.total_iva5)
                .NotEmpty().WithMessage("Total IVA 5% es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA 5% tiene que ser un valor positivo")
                .Must(BeNumeric).WithMessage("Total IVA 5% tiene que ser valores numericos");

            RuleFor(factura => factura.total_iva10)
                .NotEmpty().WithMessage("Total IVA 10% es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA 10% tiene que ser un valor positivo")
                .Must(BeNumeric).WithMessage("Total IVA 10% tiene que ser valores numericos");

            RuleFor(factura => factura.total_iva)
                .NotEmpty().WithMessage("Total IVA es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("Total IVA tiene que ser un valor positivo")
                .Must(BeNumeric).WithMessage("Total IVA tiene que ser valores numericos");

            //Total en letras obligatorio, al menos 6 caracteres.
            RuleFor(factura => factura.total_letras)
                .NotEmpty().WithMessage("Total en Letras es obligatorio.")
                .MinimumLength(6).WithMessage("Total en Letras debe contener al menos 6 caracteres.")
                .Matches("^[a-zA-Z]+$").WithMessage("Total en Letras solo debe contener letras.");


           /* RuleFor(factura => factura.fecha_hora)
                .NotEmpty().WithMessage("Es necesario carga la Fecha y Hora - Formato: yyyy-MM-ddTHH:mm:ssZ")
                .Must(BeValidSwaggerDateTime).WithMessage("Fecha Hora must be in the format 'yyyy-MM-ddTHH:mm:ssZ', e.g., '2018-03-20T09:12:28Z'");
            */
        }

        private bool BeValidSwaggerDateTime(DateTime fechaHora)
        {
            // Comprobar si DateTime está en el formato 'aaaa-MM-ddTHH:mm:ssZ'
            string format = "yyyy-MM-ddTHH:mm:ssZ";
            return fechaHora.ToString(format, CultureInfo.InvariantCulture) == fechaHora.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        private bool BeNumeric(decimal value)
        {
            return value.GetType() == typeof(decimal);
        }
    }
}



