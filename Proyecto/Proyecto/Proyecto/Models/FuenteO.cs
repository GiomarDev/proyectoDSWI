using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class FuenteO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [Required(ErrorMessage = "Seleccione Proveedor")]
        [DisplayName("PROVEEDOR")]
        public int id_prov { get; set; }

        [Required(ErrorMessage = "Seleccione Descripcion")]
        [DisplayName("DESCRIPCION")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "Seleccione Marca")]
        [DisplayName("MARCA")]
        public int id_marca { get; set; }

        [Required(ErrorMessage = "Seleccione Categoria")]
        [DisplayName("CATEGORIA")]
        public int id_categ { get; set; }

        [Required(ErrorMessage = "Ingrese watts")]
        [DisplayName("CANT. WATTS")]
        public int can_watt { get; set; }

        [Required(ErrorMessage = "Seleccione existencia de certificcion")]
        [DisplayName("CERTIFICACION")]
        public int psu_cert { get; set; }

        [Required(ErrorMessage = "Ingrese Precio")]
        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [Required(ErrorMessage = "Ingrese stock")]
        [DisplayName("UNIDAD EN EXISTENCIA")]
        public int uex { get; set; }

        [Required(ErrorMessage = "Ingrese unidad en pedido")]
        [DisplayName("UNIDAD EN PEDIDO")]
        public int upe { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}