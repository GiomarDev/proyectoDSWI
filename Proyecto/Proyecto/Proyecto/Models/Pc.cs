using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Pc
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("DESCRIPCION DE PC")]
        public string des_pc { get; set; }

        [DisplayName("CATEGORIA")]
        public string cat_pc { get; set; }

        [DisplayName("PROCESADOR")]
        public string des_pro { get; set; }

        [DisplayName("TARJETA GRAFICA")]
        public string des_gpu { get; set; }

        [DisplayName("MOTHERBOARD")]
        public string des_mb { get; set; }

        [DisplayName("MEMORIA RAM")]
        public string des_ram { get; set; }

        [DisplayName("ALAMACENAMIENTO")]
        public string des_alm { get; set; }

        [DisplayName("FUENTE DE PODER")]
        public string des_psu { get; set; }

        [DisplayName("GABINETE")]
        public string des_gab { get; set; }

        [DisplayName("MONITOR")]
        public string des_mon { get; set; }

        [DisplayName("PERIFERICO")]
        public string des_per { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("PEDIDO")]
        public int pedido { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}