using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class TiendaController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        List<CarritoPc> listPcCarrito()
        {
            List<CarritoPc> aCarritoPc = new List<CarritoPc>();
            SqlCommand cmd = new SqlCommand("SP_LISTACARRITOPC", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCarritoPc.Add(new CarritoPc()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    desc = dr[1].ToString(),
                    precio = double.Parse(dr[2].ToString()),
                    cantidad = int.Parse(dr[3].ToString()),
                    foto = dr[4].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aCarritoPc;
        
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult carritoComprasPC()
        {
            if (Session["carritoPC"] == null)
            {
                Session["carritoPC"] = new List<ItemPc>();
            }
            return View(listPcCarrito());
        }

        public ActionResult seleccionarProductoPC(int id)
        {
            CarritoPc objC = listPcCarrito().Where(p=> p.codigo == id).FirstOrDefault();
            return View(objC);
        }

    }
}