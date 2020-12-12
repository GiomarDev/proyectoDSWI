using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class MonitorController : Controller
    { 
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        List<MonitorA> ListMonitor()
        {
            List<MonitorA> aMonitor = new List<MonitorA>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMONITOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMonitor.Add(new MonitorA()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    proveedor = dr[1].ToString(),
                    nombre = dr[2].ToString(),
                    marca = dr[3].ToString(),
                    categoria = dr[4].ToString(),
                    frec_monitor = int.Parse(dr[5].ToString()),
                    precio = double.Parse(dr[6].ToString()),
                    uex_pro = int.Parse(dr[7].ToString()),
                    upe_pro = int.Parse(dr[8].ToString()),

                });
            }
            dr.Close();
            cn.Close();
            return aMonitor;
        }

        List<MonitorO> ListMonitorO()
        {
            List<MonitorO> aMonitorO = new List<MonitorO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMONITORO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMonitorO.Add(new MonitorO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    id_prov = int.Parse(dr[1].ToString()),
                    nombre = dr[2].ToString(),
                    id_marca = int.Parse(dr[3].ToString()),
                    id_categ = int.Parse(dr[4].ToString()),
                    mon_frec = int.Parse(dr[5].ToString()),
                    precio = double.Parse(dr[6].ToString()),
                    uex = int.Parse(dr[7].ToString()),
                    upe = int.Parse(dr[8].ToString()),
                    foto = dr[9].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aMonitorO;
        }

        List<Proveedor> ListProveedor()
        {
            List<Proveedor> aProveedor = new List<Proveedor>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARPROV", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProveedor.Add(new Proveedor()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aProveedor;
        }

        List<Marca> ListMarca()
        {
            List<Marca> aMarca = new List<Marca>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARMARC", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMarca.Add(new Marca()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aMarca;
        }
        List<Categoria> ListCategoria()
        {
            List<Categoria> aCategoria = new List<Categoria>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARCAT", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCategoria.Add(new Categoria()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aCategoria;
        }

        List<MonitorA> ListDetalleMonitor()
        {
            List<MonitorA> aMonitor = new List<MonitorA>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTADETALLEMONITOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMonitor.Add(new MonitorA()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    proveedor = dr[1].ToString(),
                    nombre = dr[2].ToString(),
                    marca = dr[3].ToString(),
                    categoria = dr[4].ToString(),
                    frec_monitor = int.Parse(dr[5].ToString()),
                    precio = double.Parse(dr[6].ToString()),
                    uex_pro = int.Parse(dr[7].ToString()),
                    upe_pro = int.Parse(dr[8].ToString()),
                    foto = dr[9].ToString()

                });
            }
            dr.Close();
            cn.Close();
            return aMonitor;
        }




        public ActionResult listadoMonitor()
        {
            return View(ListMonitor());
        }


        //nuevo monitor
        public ActionResult nuevoMonitor()
        {
            //Para los combos
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre");

            return View(new MonitorO());
        }

        [HttpPost]
        public ActionResult nuevoMonitor(MonitorO objM, HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objM);
            }

            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objM); ;
            }
            List<SqlParameter> parametros = new List<SqlParameter>()
            { 
                //Parametros de registro            
                new SqlParameter(){ParameterName="@PROV", SqlDbType=SqlDbType.Int,Value=objM.id_prov},
                new SqlParameter(){ParameterName="@DES", SqlDbType=SqlDbType.VarChar,Value=objM.nombre},
                new SqlParameter(){ParameterName="@MARC", SqlDbType=SqlDbType.Int,Value=objM.id_marca},
                new SqlParameter(){ParameterName="@CAT", SqlDbType=SqlDbType.Int,Value=objM.id_categ},
                new SqlParameter(){ParameterName="@FREC", SqlDbType=SqlDbType.Int,Value=objM.mon_frec},
                new SqlParameter(){ParameterName="@PRE", SqlDbType=SqlDbType.Money,Value=objM.precio},
                new SqlParameter(){ParameterName="@UEX", SqlDbType=SqlDbType.Int,Value=objM.uex},
                new SqlParameter(){ParameterName="@UPE", SqlDbType=SqlDbType.Int,Value=objM.upe},
                new SqlParameter(){ParameterName="@FOT", SqlDbType=SqlDbType.VarChar,Value="../fotos_monitor/"+ Path.GetFileName(f.FileName)}
                //@PROV,@DES,@MARC,@CAT,@FREC,@PRE,@UEX,@UPE,@FOT
            };
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre");
            Crud("SP_NUEVOMONITOR", parametros);
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_monitor/"), Path.GetFileName(f.FileName)));
            return RedirectToAction("listadoMonitor");
        }

        void Crud(string proceso, List<SqlParameter> parametros)
        {
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //Soportar todos los parametros y hacer un match
                cmd.Parameters.AddRange(parametros.ToArray());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            cn.Close();
        }

        //detalle monitor
        public ActionResult detalleMonitor(int id)
        {
            MonitorA objM = ListDetalleMonitor().Where(m => m.codigo == id).FirstOrDefault();
            return View(objM);
        }

        ////elimina pc
        public ActionResult eliminarMonitor(int id)
        {
            MonitorA objM = ListMonitor().Where(m => m.codigo == id).FirstOrDefault();
            List<SqlParameter> lista = new List<SqlParameter>() {
             new SqlParameter(){
                ParameterName="@ID_MON",
                SqlDbType=SqlDbType.Int,
                Value=objM.codigo }
            };

            Crud("SP_ELIMINAMONITOR", lista);
            return RedirectToAction("listadoMonitor");
        }
        //el metodo se define como string porque se va a devolver una ruta (texto)
        public string MostrarFoto(int id)
        {
            cn.Open();
            //llamamos al storeprocedure que consulta la ruta de la foto usando el id
            SqlCommand cmd = new SqlCommand("SP_LISTAFOTOMONITOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDE_MONITOR", id);
            SqlDataReader dr = cmd.ExecuteReader();
            string ruta = "";
            while (dr.Read())
            {
                ruta = dr[0].ToString();
            }
            dr.Close();
            cn.Close();
            //se retorna la ruta 
            return ruta;
        }

        public ActionResult modificaMonitor(int id)
        {
            //Metodo para la busqueda
            MonitorO objM = ListMonitorO().Where(m => m.codigo == id).FirstOrDefault();
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre", objM.id_prov);
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre", objM.id_marca);
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre", objM.id_categ);
            return View(objM);
        }
        [HttpPost]
        public ActionResult modificaMonitor(MonitorO objM)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()

            { 
                //Parametros de registro  
                new SqlParameter(){ParameterName="@ID_MON", SqlDbType=SqlDbType.Int,Value=objM.codigo},
                new SqlParameter(){ParameterName="@PROV", SqlDbType=SqlDbType.Int,Value=objM.id_prov},
                new SqlParameter(){ParameterName="@DES", SqlDbType=SqlDbType.VarChar,Value=objM.nombre},
                new SqlParameter(){ParameterName="@MARC", SqlDbType=SqlDbType.Int,Value=objM.id_marca},
                new SqlParameter(){ParameterName="@CAT", SqlDbType=SqlDbType.Int,Value=objM.id_categ},
                new SqlParameter(){ParameterName="@FREC", SqlDbType=SqlDbType.Int,Value=objM.mon_frec},
                new SqlParameter(){ParameterName="@PRE", SqlDbType=SqlDbType.Money,Value=objM.precio},
                new SqlParameter(){ParameterName="@UEX", SqlDbType=SqlDbType.Int,Value=objM.uex},
                new SqlParameter(){ParameterName="@UPE", SqlDbType=SqlDbType.Int,Value=objM.upe},
                new SqlParameter(){ParameterName="@FOT", SqlDbType=SqlDbType.VarChar,Value=objM.foto}
                //@PROV,@DES,@MARC,@CAT,@FREC,@PRE,@UEX,@UPE,@FOT
            };
            Crud("SP_ACTUALIZAMONITOR", parametros);
            return RedirectToAction("listadoMonitor");
        }

    }
}
