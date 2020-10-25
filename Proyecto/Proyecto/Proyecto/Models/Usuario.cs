using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Usuario
    {
        public string codigo { get; set; }
        public string pass { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
    }

}