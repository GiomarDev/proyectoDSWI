using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PcO
    {
        public int codigo { get; set; }
        public string des_pc { get; set; }
        public int id_proc { get; set; }
        public int id_ram { get; set; }
        public int id_alm { get; set; }
        public int id_psu { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
        public string foto { get; set; }
    }
}