using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PcO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }
        [Required(ErrorMessage = "Ingrese Descripcion")]
        [DisplayName("DESCRIPCION DE PC")]
        public string des_pc { get; set; }
        [Required(ErrorMessage = "Seleccione Categoria")]
        [DisplayName("CATEGORIA")]
        public int id_cat { get; set; }
        [Required(ErrorMessage = "Seleccione procesador")]
        [DisplayName("NOMBRE DE PROCESADOR")]
        public int id_proc { get; set; }
        [Required(ErrorMessage = "Seleccione GPU")]
        [DisplayName("TARJETA GRAFICA")]
        public int id_gpu { get; set; }
        [Required(ErrorMessage = "Seleccione Motherboard")]
        [DisplayName("MOTHERBOARD")]
        public int id_mb { get; set; }
        [Required(ErrorMessage = "Seleccione Ram")]
        [DisplayName("MEMORIA RAM")]
        public int id_ram { get; set; }
        [Required(ErrorMessage = "Seleccione almacenamiento")]
        [DisplayName("ALAMACENAMIENTO")]
        public int id_alm { get; set; }
        [Required(ErrorMessage = "Seleccione fuente")]
        [DisplayName("FUENTE DE PODER")]
        public int id_psu { get; set; }
        [Required(ErrorMessage = "Seleccione gabinete")]
        [DisplayName("GABINETE")]
        public int id_gab { get; set; }
        [Required(ErrorMessage = "Seleccione monitor")]
        [DisplayName("MONITOR")]
        public int id_mon { get; set; }
        [Required(ErrorMessage = "Seleccione periferico")]
        [DisplayName("PERIFERICO")]
        public int id_per { get; set; }
        [Required(ErrorMessage = "Ingrese Precio")]
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [Required(ErrorMessage = "Ingrese Stock")]
        [DisplayName("STOCK")]
        public int uex_pro { get; set; }
        [Required(ErrorMessage = "Ingrese unidades en Pedido")]
        [DisplayName("PEDIDO")]
        public int upe_pro { get; set; }
        [DisplayName("FOTO")]
        public string foto { get; set; }


    }
}