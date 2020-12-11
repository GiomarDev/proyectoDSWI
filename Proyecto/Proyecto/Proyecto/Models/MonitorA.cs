using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class MonitorA
    {
            [DisplayName("CODIGO")]
            public int codigo { get; set; }

            [DisplayName("PROVEEDOR")]
            public string proveedor { get; set; }

            [DisplayName("DESCRIPCION")]
            public string nombre { get; set; }

            [DisplayName("MARCA")]
            public string marca { get; set; }

            [DisplayName("CATEGORIA")]
            public string categoria { get; set; }

            [DisplayName("FRECUENCIA MONITOR")]
            public int frec_monitor { get; set; }

            [DisplayName("PRECIO")]
            public double precio { get; set; }

            [DisplayName("UNIDAD EN EXISTENCIA")]
            public int uex_pro { get; set; }

            [DisplayName("UNIDAD EN PEDIDO")]
            public int upe_pro { get; set; }

            [DisplayName("UNIDAD EN FOTO")]
            public string foto { get; set; }
        }
}