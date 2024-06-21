using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class FacturaModel
    {
        public int id { get; set; }
        public int id_cliente { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{6}$")]
        public string nro_factura { get; set; }
        public DateTime fecha_hora { get; set; }
        public decimal total { get; set; }
        public decimal total_iva5 { get; set; }
        public decimal total_iva10 { get; set; }
        public decimal total_iva { get; set; }
        public string total_letras { get; set; }
        public string sucursal { get; set; }
    }
}
