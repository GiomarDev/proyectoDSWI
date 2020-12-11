using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Proveedor
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBER")]
        public String nombre { get; set; }

        [DisplayName("DIRECCION")]
        public String abreviatura { get; set; }
    }
}