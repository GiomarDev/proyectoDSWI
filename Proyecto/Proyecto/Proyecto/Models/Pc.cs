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

        [DisplayName("NOMBRE DE PROCESADOR")]
        public string nom_pro { get; set; }

        [DisplayName("MEMORIA RAM")]
        public string des_ram { get; set; }

        [DisplayName("ALAMACENAMIENTO")]
        public string des_alm { get; set; }

        [DisplayName("FUENTE DE PODER")]
        public string des_psu { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}