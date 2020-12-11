using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Marca
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBRE")]
        public String nombre { get; set; }
    }
}