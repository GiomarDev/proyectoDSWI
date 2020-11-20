using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Proyecto.Models
{
    public class ProcesadorO
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

        [DisplayName("NUCLEOS")]
        public int nucleos { get; set; }

        [DisplayName("HILOS")]
        public int hilos { get; set; }

        [DisplayName("RELOJ BASE")]
        public double reloj_base { get; set; }

        [DisplayName("RELOJ MAXIMO")]
        public double reloj_max { get; set; }

        [DisplayName("TDP")]
        public double tdp { get; set; }

        [DisplayName("GRAFICA INTEGRADA")]
        public int gpui { get; set; }

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