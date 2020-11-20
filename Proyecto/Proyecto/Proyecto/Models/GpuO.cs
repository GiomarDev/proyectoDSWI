using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Proyecto.Models
{
    public class GpuO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("PROVEEDOR")]
        public int id_prov { get; set; }

        [DisplayName("DESCRIPCION")]
        public String nombre { get; set; }

        [DisplayName("MARCA")]
        public int id_marca { get; set; }

        [DisplayName("CATEGORIA")]
        public int id_categ { get; set; }

        [DisplayName("CANT. MEMORIA GPU")]
        public int mem_gpu { get; set; }

        [DisplayName("UNIDAD DE MEDIDA")]
        public int id_uni_med { get; set; }

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