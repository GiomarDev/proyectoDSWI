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

        //LISTADO DE CARRITO DE COMPRAS PC
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

        //LISTADO DE CARRITO DE COMPRAS PROCESADOR
        List<CarritoProce> listProceCarrito()
        {
            List<CarritoProce> aCarritoProce = new List<CarritoProce>();
            SqlCommand cmd = new SqlCommand("SP_LISTACARRITOPROCESADOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCarritoProce.Add(new CarritoProce()
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
            return aCarritoProce;
        }

        public ActionResult Index()
        {
            return View();
        }

        //CARRITO DE COMPRAS DE PC
        public ActionResult carritoComprasPC()
        {
            if (Session["carritoPC"] == null)
            {
                Session["carritoPC"] = new List<ItemPc>();
            }
            return View(listPcCarrito());
        }
        //CARRITO DE COMPRAS DE PROCESADORES
        public ActionResult carritoComprasPRO()
        {
            if (Session["carritoPROCE"] == null)
            {
                Session["carritoPROCE"] = new List<ItemProce>();
            }
            return View(listProceCarrito());
        }
        //SELECCION DE PRODUCTOS DE PC
        public ActionResult seleccionarProductoPC(int id)
        {
            CarritoPc objC = listPcCarrito().Where(p=> p.codigo == id).FirstOrDefault();
            return View(objC);
        }
        //CARRITO DE PRODUCTOS DE PROCESADORES
        public ActionResult seleccionarProductoPROCE(int id)
        {
            CarritoProce objC = listProceCarrito().Where(p => p.codigo == id).FirstOrDefault();
            return View(objC);
        }

        //AGREGA PRODUCTO DE PC
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

        //AGREGA PRODUCTO DE PROCESADOR
        public ActionResult agregarProductoPROCE(int id, int cant = 0)
        {
            var miProductoProce = listProceCarrito().Where(p => p.codigo == id).FirstOrDefault();

            ItemProce objI = new ItemProce()
            {
                codigo = miProductoProce.codigo,
                desc = miProductoProce.desc,
                precio = miProductoProce.precio,
                cantidad = cant,
                foto = miProductoProce.foto
            };

            var miCarritoPc = (List<ItemProce>)Session["carritoPROCE"];
            miCarritoPc.Add(objI);
            Session["carritoPROCE"] = miCarritoPc;
            return RedirectToAction("carritoComprasPRO");
        }

        //COMPRAR PC
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

        //COMPRAR PROCESADOR
        public ActionResult comprarPROCE()
        {
            if (Session["carritoPROCE"] == null)
            {
                return RedirectToAction("carritoComprasPRO");
            }

            var miCarritoProce = (List<ItemProce>)Session["carritoPROCE"];
            ViewBag.total = miCarritoProce.Sum(i => i.subTotal);
            return View(miCarritoProce);
        }
        
        //ELIMINAR ITEM DE PC
        public ActionResult eliminarItemPC(int id)
        {
            var miCarritoPc = (List<ItemPc>)Session["carritoPC"];
            var miProductoPc = miCarritoPc.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoPc.Remove(miProductoPc);

            //Actualizar el Carrito con los nuevos registros
            Session["carritoPC"] = miCarritoPc;
            return RedirectToAction("comprarPC");
        }

        //ELIMINAR ITEM DE PROCESADOR
        public ActionResult eliminarItemPROCE(int id)
        {
            var miCarritoProce = (List<ItemProce>)Session["carritoPROCE"];
            var miProductoProce = miCarritoProce.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoProce.Remove(miProductoProce);

            //Actualizar el Carrito con los nuevos registros
            Session["carritoPROCE"] = miCarritoProce;
            return RedirectToAction("comprarPROCE");
        }

        //PAGAR PC
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

        //PAGAR PROCESADOR
        public ActionResult pagoProce()
        {
            List<ItemProce> detalle = (List<ItemProce>)Session["carritoPROCE"];
            double total = 0;
            foreach (ItemProce i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View(detalle);
        }

        //FINALIZA VENTA DE PC
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

        //FINALIZA VENTA DE PROCESADOR
        public ActionResult FinalizarVentaPROCE(string dni, string nom)
        {
            ViewBag.dni = dni;
            ViewBag.nom = nom;
            List<ItemProce> detalle = (List<ItemProce>)Session["carritoPROCE"];
            double total = 0;
            foreach (ItemProce i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View();

        }

        //BORRAR SESSION DE PC
        public ActionResult borrarSessionPC()
        {
            Session["carritoPC"] = null;
            return RedirectToAction("Index");
        }

        //BORRAR SESSION DE PROCESADOR
        public ActionResult borrarSessionPROCE()
        {
            Session["carritoPROCE"] = null;
            return RedirectToAction("Index");
        }

        ///////carrito del monitor
        List<CarritoMonitor> listMonitorCarrito()
        {
            List<CarritoMonitor> aCarritoMonitor = new List<CarritoMonitor>();
            SqlCommand cmd = new SqlCommand("SP_LISTACARRITOMONITOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCarritoMonitor.Add(new CarritoMonitor()
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
            return aCarritoMonitor;
        }

        public ActionResult carritoComprasMonitor()
        {
            if (Session["CarritoMonitor"] == null)
            {
                Session["carritoMonitor"] = new List<ItemMonitor>();
            }
            return View(listMonitorCarrito());
        }

        public ActionResult seleccionarProductoMonitor(int id)
        {
            CarritoMonitor objM = listMonitorCarrito().Where(m => m.codigo == id).FirstOrDefault();
            return View(objM);
        }

        public ActionResult agregarProductoMonitor(int id, int cant = 0)
        {
            var miProductoMonitor = listMonitorCarrito().Where(m => m.codigo == id).FirstOrDefault();

            ItemMonitor objIM = new ItemMonitor()
            {
                codigo = miProductoMonitor.codigo,
                desc = miProductoMonitor.desc,
                precio = miProductoMonitor.precio,
                cantidad = cant,
                foto = miProductoMonitor.foto
            };

            var miCarritoMonitor = (List<ItemMonitor>)Session["carritoMonitor"];
            miCarritoMonitor.Add(objIM);
            Session["carritoMonitor"] = miCarritoMonitor;
            return RedirectToAction("carritoComprasMonitor");
        }
        public ActionResult comprarMonitor()
        {
            if (Session["carritoMonitor"] == null)
            {
                return RedirectToAction("carritoComprasMonitor");
            }

            var miCarritoMonitor = (List<ItemMonitor>)Session["carritoMonitor"];
            ViewBag.total = miCarritoMonitor.Sum(i => i.subTotal);
            return View(miCarritoMonitor);
        }

        public ActionResult eliminarItemMonitor(int id)
        {
            var miCarritoMonitor = (List<ItemMonitor>)Session["carritoMonitor"];
            var miProductoMonitor = miCarritoMonitor.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoMonitor.Remove(miProductoMonitor);

            //Actualizar el Carrito con los nuevos registros
            Session["carritoMonitor"] = miCarritoMonitor;
            return RedirectToAction("comprarMonitor");
        }

        public ActionResult pagoMonitor()
        {
            List<ItemMonitor> detalle = (List<ItemMonitor>)Session["carritoMonitor"];
            double total = 0;
            foreach (ItemMonitor i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View(detalle);
        }

        public ActionResult FinalizarVentaMonitor(string dni, string nom)
        {
            ViewBag.dni = dni;
            ViewBag.nom = nom;
            List<ItemMonitor> detalle = (List<ItemMonitor>)Session["carritoMonitor"];
            double total = 0;
            foreach (ItemMonitor i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View();

        }

        public ActionResult borrarSessionMonitor()
        {
            Session["carritoMonitor"] = null;
            return RedirectToAction("Index");
        }


        ////carrito RAM
        List<CarritoRam> listRamCarrito()
        {
            List<CarritoRam> aCarritoRam = new List<CarritoRam>();
            SqlCommand cmd = new SqlCommand("SP_LISTACARRITORAM", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCarritoRam.Add(new CarritoRam()
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
            return aCarritoRam;
        }
        public ActionResult carritoComprasRam()
        {
            if (Session["CarritoRam"] == null)
            {
                Session["carritoRam"] = new List<ItemRam>();
            }
            return View(listRamCarrito());
        }

        public ActionResult seleccionarProductoRam(int id)
        {
            CarritoRam objR = listRamCarrito().Where(m => m.codigo == id).FirstOrDefault();
            return View(objR);
        }

        public ActionResult agregarProductoRam(int id, int cant = 0)
        {
            var miProductoRam = listRamCarrito().Where(m => m.codigo == id).FirstOrDefault();

            ItemRam objIM = new ItemRam()
            {
                codigo = miProductoRam.codigo,
                desc = miProductoRam.desc,
                precio = miProductoRam.precio,
                cantidad = cant,
                foto = miProductoRam.foto
            };

            var miCarritoRam = (List<ItemRam>)Session["CarritoRam"];
            miCarritoRam.Add(objIM);
            Session["CarritoRam"] = miCarritoRam;
            return RedirectToAction("carritoComprasRam");
        }

        public ActionResult comprarRam()
        {
            if (Session["CarritoRam"] == null)
            {
                return RedirectToAction("carritoComprasRam");
            }

            var miCarritoRam = (List<ItemRam>)Session["CarritoRam"];
            ViewBag.total = miCarritoRam.Sum(i => i.subTotal);
            return View(miCarritoRam);
        }

        public ActionResult eliminarItemRam(int id)
        {
            var miCarritoRam = (List<ItemRam>)Session["CarritoRam"];
            var miProductoRam = miCarritoRam.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoRam.Remove(miProductoRam);

            //Actualizar el Carrito con los nuevos registros
            Session["CarritoRam"] = miCarritoRam;
            return RedirectToAction("comprarRam");
        }

        public ActionResult pagoRam()
        {
            List<ItemRam> detalle = (List<ItemRam>)Session["CarritoRam"];
            double total = 0;
            foreach (ItemRam i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View(detalle);
        }

        public ActionResult FinalizarVentaRam(string dni, string nom)
        {
            ViewBag.dni = dni;
            ViewBag.nom = nom;
            List<ItemRam> detalle = (List<ItemRam>)Session["CarritoRam"];
            double total = 0;
            foreach (ItemRam i in detalle)
            {
                total += i.subTotal;
            }

            ViewBag.total = total;
            return View();

        }

        public ActionResult borrarSessionRam()
        {
            Session["CarritoRam"] = null;
            return RedirectToAction("Index");
        }

        /// ///////////////***************////////////////////////


        List<CarritoMobo> listMoboCarrito()
        {
            List<CarritoMobo> aMobos = new List<CarritoMobo>();
            SqlCommand cmd = new SqlCommand("SP_LISTACARRITOMOBO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMobos.Add(new CarritoMobo()
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
            return aMobos;
        }

        public ActionResult carritoComprasMobo()
        {
            if (Session["carritoMobo"] == null)
            {
                Session["carritoMobo"] = new List<ItemMotherboard>();
            }
            return View(listMoboCarrito());
        }

        public ActionResult seleccionarProductoMobo(int id)
        {
            CarritoMobo objC = listMoboCarrito().Where(p => p.codigo == id).FirstOrDefault();
            return View(objC);
        }

        public ActionResult agregarProductoMobo(int id, int cant = 0)
        {
            var miProductoMobo = listMoboCarrito().Where(p => p.codigo == id).FirstOrDefault();

            ItemMotherboard objI = new ItemMotherboard()
            {
                codigo = miProductoMobo.codigo,
                nombre = miProductoMobo.desc,
                precio = miProductoMobo.precio,
                cantidad = cant,
                foto = miProductoMobo.foto
            };

            var miCarritoMobo = (List<ItemMotherboard>)Session["carritoMobo"];
            miCarritoMobo.Add(objI);
            Session["carritoMobo"] = miCarritoMobo;
            return RedirectToAction("carritoComprasMobo");
        }

        public ActionResult comprarMobo()
        {
            if (Session["carritoMobo"] == null)
            {
                return RedirectToAction("carritoComprasMobo");
            }

            var miCarritoMobo = (List<ItemMotherboard>)Session["carritoMobo"];
            ViewBag.total = miCarritoMobo.Sum(i => i.subtotal);
            return View(miCarritoMobo);
        }

        public ActionResult eliminarItemMobo(int id)
        {
            var miCarritoMobo = (List<ItemMotherboard>)Session["carritoMobo"];
            var miProductoMobo = miCarritoMobo.Where(i => i.codigo == id).FirstOrDefault();
            miCarritoMobo.Remove(miProductoMobo);

            //Actualizar el Carrito con los nuevos registros
            Session["carritoMobo"] = miCarritoMobo;
            return RedirectToAction("comprarMobo");
        }

        public ActionResult pagoMobo()
        {
            List<ItemMotherboard> detalle = (List<ItemMotherboard>)Session["carritoMobo"];
            double total = 0;
            foreach (ItemMotherboard i in detalle)
            {
                total += i.subtotal;
            }

            ViewBag.total = total;
            return View(detalle);
        }

        public ActionResult FinalizarVentaMobo(string dni, string nom)
        {
            ViewBag.dni = dni;
            ViewBag.nom = nom;
            List<ItemMotherboard> detalle = (List<ItemMotherboard>)Session["carritoMobo"];
            double total = 0;
            foreach (ItemMotherboard i in detalle)
            {
                total += i.subtotal;
            }

            ViewBag.total = total;
            return View();

        }

        public ActionResult borrarSessionMobo()
        {
            Session["carritoMobo"] = null;
            return RedirectToAction("Index");
        }

    }
}