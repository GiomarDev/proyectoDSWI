using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Proyecto.Models
{
    public class ProveedorO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBER")]
        public String nombre { get; set; }

        [DisplayName("DIRECCION")]
        public String abreviatura { get; set; }
    }
}