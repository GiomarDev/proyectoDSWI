using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class GpuA
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("PROVEEDOR")]
        public String id_prov { get; set; }

        [DisplayName("DESCRIPCION")]
        public String nombre { get; set; }

        [DisplayName("MARCA")]
        public String id_marca { get; set; }

        [DisplayName("CATEGORIA")]
        public String id_categ { get; set; }

        [DisplayName("CANT. MEMORIA GPU")]
        public int mem_gpu { get; set; }

        [DisplayName("UNIDAD DE MEDIDA")]
        public String id_uni_med { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("UNIDAD EN EXISTENCIA")]
        public int uex { get; set; }

        [DisplayName("UNIDAD EN PEDIDO")]
        public int upe { get; set; }

        [DisplayName("FOTO")]
        public String foto { get; set; }
    }
}