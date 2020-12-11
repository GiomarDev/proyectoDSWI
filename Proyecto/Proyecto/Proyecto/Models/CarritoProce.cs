using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class CarritoProce
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBRE")]
        public string desc { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK ACTUAL")]
        public int cantidad { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}