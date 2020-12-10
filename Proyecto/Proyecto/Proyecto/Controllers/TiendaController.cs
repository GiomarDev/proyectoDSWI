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

        public ActionResult agregarProductoPC(int id, int cant=0)
        {
            var miProductoPc = listPcCarrito().Where(p => p.codigo == id).FirstOrDefault();
            
            ItemPc objI = new ItemPc()
            {
                codigo = miProductoPc.codigo,
                desc = miProductoPc.desc,
                precio = miProductoPc.precio,
                cantidad = cant,
                foto = miProductoPc.foto
            };

            var miCarritoPc = (List<ItemPc>)Session["carritoPC"];
            miCarritoPc.Add(objI);
            Session["carritoPC"] = miCarritoPc;
            return RedirectToAction("carritoComprasPC");
        }

        public ActionResult comprarPC()
        {
            if (Session["carritoPC"] == null)
            {
                return RedirectToAction("carritoComprasPC");
            }

            var miCarritoPc = (List<ItemPc>)Session["carritoPC"];
            ViewBag.total = miCarritoPc.Sum(i => i.subTotal);
            return View(miCarritoPc);
        }

        public ActionResult eliminarItemPC(int id)
        {
            var miCarritoPc = (List<ItemPc>)Session["carritoPC"];
            var miProductoPc = miCarritoPc.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoPc.Remove(miProductoPc);

            //Actualizar el Carrito con los nuevos registros
            Session["carritoPC"] = miCarritoPc;
            return RedirectToAction("comprarPC");
        }

        public ActionResult pagoPc()
        {
            List<ItemPc> detalle = (List<ItemPc>)Session["carritoPC"];
            double total = 0;
            foreach (ItemPc i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View(detalle);
        }

        public ActionResult FinalizarVentaPC(string dni, string nom)
        {
            ViewBag.dni = dni;
            ViewBag.nom = nom;
            List<ItemPc> detalle = (List<ItemPc>)Session["carritoPC"];
            double total = 0;
            foreach (ItemPc i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View();
            
        }

        public ActionResult borrarSessionPC()
        {
            Session["carritoPC"] = null;
            return RedirectToAction("Index");
        }

    }
}