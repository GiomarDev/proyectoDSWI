using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class ItemMonitor
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("DESCRIPCION")]
        public string desc { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK ACTUAL")]
        public int cantidad { get; set; }

        [DisplayName("SUB TOTAL")]
        public double subTotal
        {
            get { return cantidad * precio; }
        }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}