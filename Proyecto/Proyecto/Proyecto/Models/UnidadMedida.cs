using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class UnidadMedida
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("U. MEDIDA")]
        public String nombre { get; set; }

        [DisplayName("ABREVIATURA")]
        public String abr { get; set; }
    }
}