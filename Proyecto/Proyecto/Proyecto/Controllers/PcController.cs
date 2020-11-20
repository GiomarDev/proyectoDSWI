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
    public class PcController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        List<Pc> ListPc()
        {
            List<Pc> aPc = new List<Pc>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAPC", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPc.Add(new Pc()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    des_pc = dr[1].ToString(),
                    cat_pc = dr[2].ToString(),
                    des_pro = dr[3].ToString(),
                    des_gpu = dr[4].ToString(),
                    des_mb = dr[5].ToString(),
                    des_ram = dr[6].ToString(),
                    des_alm = dr[7].ToString(),
                    des_psu = dr[8].ToString(),
                    des_gab = dr[9].ToString(),
                    des_mon = dr[10].ToString(),
                    des_per = dr[11].ToString(),
                    precio = double.Parse(dr[12].ToString()),
                    stock = int.Parse(dr[13].ToString()),
                    pedido = int.Parse(dr[14].ToString()),
                    foto = dr[15].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPc;
        }

        List<PcO> ListPcO()
        {
            List<PcO> aPcO = new List<PcO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAPCO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPcO.Add(new PcO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    des_pc = dr[1].ToString(),
                    id_cat = int.Parse(dr[2].ToString()),
                    id_proc = int.Parse(dr[3].ToString()),
                    id_gpu = int.Parse(dr[4].ToString()),
                    id_mb = int.Parse(dr[5].ToString()),
                    id_ram = int.Parse(dr[6].ToString()),
                    id_alm = int.Parse(dr[7].ToString()),
                    id_psu = int.Parse(dr[8].ToString()),
                    id_gab = int.Parse(dr[9].ToString()),
                    id_mon = int.Parse(dr[10].ToString()),
                    id_per = int.Parse(dr[11].ToString()),
                    precio = double.Parse(dr[12].ToString()),
                    uex_pro = int.Parse(dr[13].ToString()),
                    upe_pro = int.Parse(dr[14].ToString()),
                    foto = dr[15].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPcO;
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

        List<Procesador> ListProcesador()
        {
            List<Procesador> aProcesador = new List<Procesador>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARPROC", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProcesador.Add(new Procesador()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aProcesador;
        }

        List<Gpu> ListGpu()
        {
            List<Gpu> aGpu = new List<Gpu>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARGPU", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aGpu.Add(new Gpu()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aGpu;
        }

        List<Motherboard> ListMotherboard()
        {
            List<Motherboard> aMotherboard = new List<Motherboard>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARMB", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMotherboard.Add(new Motherboard()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aMotherboard;
        }

        List<Ram> ListRam()
        {
            List<Ram> aRam = new List<Ram>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARRAM", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aRam.Add(new Ram()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aRam;
        }

        List<Almacenamiento> ListAlmacenamiento()
        {
            List<Almacenamiento> aAlmacenamiento = new List<Almacenamiento>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARALM", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aAlmacenamiento.Add(new Almacenamiento()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aAlmacenamiento;
        }

        List<Fuente> ListFuente()
        {
            List<Fuente> aFuente = new List<Fuente>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARPSU", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aFuente.Add(new Fuente()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aFuente;
        }

        List<Gabinete> ListGabinete()
        {
            List<Gabinete> aGabinete = new List<Gabinete>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARGAB", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aGabinete.Add(new Gabinete()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aGabinete;
        }

        List<Monitor> ListMonitor()
        {
            List<Monitor> aMonitor = new List<Monitor>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARMON", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMonitor.Add(new Monitor()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aMonitor;
        }

        List<Periferico> ListPeriferico()
        {
            List<Periferico> aPeriferico = new List<Periferico>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTARPER", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPeriferico.Add(new Periferico()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aPeriferico;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadoPc()
        {
            return View(ListPc());
        }

        public ActionResult nuevaPc()
        {
            //Para los combos
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre");
            ViewBag.procesadores = new SelectList(ListProcesador(), "codigo", "nombre");
            ViewBag.gpus = new SelectList(ListGpu(), "codigo", "nombre");
            ViewBag.mothers = new SelectList(ListMotherboard(), "codigo", "nombre");
            ViewBag.rams = new SelectList(ListRam(), "codigo", "nombre");
            ViewBag.almacs = new SelectList(ListAlmacenamiento(), "codigo", "nombre");
            ViewBag.fuentes = new SelectList(ListFuente(), "codigo", "nombre");
            ViewBag.gabinetes = new SelectList(ListGabinete(), "codigo", "nombre");
            ViewBag.monitores = new SelectList(ListMonitor(), "codigo", "nombre");
            ViewBag.perifericos = new SelectList(ListPeriferico(), "codigo", "nombre");
            return View(new PcO());
        }

        [HttpPost]
        public ActionResult nuevaPc(PcO objP, HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objP);
            }

            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objP);
            }
            List<SqlParameter> parametros = new List<SqlParameter>()
            { 
                //Parametros de registro            
                new SqlParameter(){ParameterName="@DES", SqlDbType=SqlDbType.VarChar,Value=objP.des_pc},
                new SqlParameter(){ParameterName="@CAT", SqlDbType=SqlDbType.Int,Value=objP.id_cat},
                new SqlParameter(){ParameterName="@PROC", SqlDbType=SqlDbType.Int,Value=objP.id_proc},
                new SqlParameter(){ParameterName="@GPU", SqlDbType=SqlDbType.Int,Value=objP.id_gpu},
                new SqlParameter(){ParameterName="@MB", SqlDbType=SqlDbType.Int,Value=objP.id_mb},
                new SqlParameter(){ParameterName="@RAM", SqlDbType=SqlDbType.Int,Value=objP.id_ram},
                new SqlParameter(){ParameterName="@ALM", SqlDbType=SqlDbType.Int,Value=objP.id_alm},
                new SqlParameter(){ParameterName="@PSU", SqlDbType=SqlDbType.Int,Value=objP.id_psu},
                new SqlParameter(){ParameterName="@GAB", SqlDbType=SqlDbType.Int,Value=objP.id_gab},
                new SqlParameter(){ParameterName="@MON", SqlDbType=SqlDbType.Int,Value=objP.id_mon},
                new SqlParameter(){ParameterName="@PER", SqlDbType=SqlDbType.Int,Value=objP.id_per},
                new SqlParameter(){ParameterName="@PRE", SqlDbType=SqlDbType.Money,Value=objP.precio},
                new SqlParameter(){ParameterName="@UEX", SqlDbType=SqlDbType.Int,Value=objP.uex_pro},
                new SqlParameter(){ParameterName="@UPE", SqlDbType=SqlDbType.Int,Value=objP.upe_pro},
                new SqlParameter(){ParameterName="@FOT", SqlDbType=SqlDbType.VarChar,Value="~/fotos_pc/"+ Path.GetFileName(f.FileName)}
                //(@DES, @CAT , @PROC , @GPU , @MB , @RAM , @ALM , @PSU , @GAB , @MON , @PER , @PRE , @UEX , @UPE , @FOT)
            };
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre");
            ViewBag.procesadores = new SelectList(ListProcesador(), "codigo", "nombre");
            ViewBag.gpus = new SelectList(ListGpu(), "codigo", "nombre");
            ViewBag.mothers = new SelectList(ListMotherboard(), "codigo", "nombre");
            ViewBag.rams = new SelectList(ListRam(), "codigo", "nombre");
            ViewBag.almacs = new SelectList(ListAlmacenamiento(), "codigo", "nombre");
            ViewBag.fuentes = new SelectList(ListFuente(), "codigo", "nombre");
            ViewBag.gabinetes = new SelectList(ListGabinete(), "codigo", "nombre");
            ViewBag.monitores = new SelectList(ListMonitor(), "codigo", "nombre");
            ViewBag.perifericos = new SelectList(ListPeriferico(), "codigo", "nombre");
            Crud("SP_NUEVAPC", parametros);
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_pc/"), Path.GetFileName(f.FileName)));
            return RedirectToAction("listadoPc");
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

        public ActionResult detallePc(int id)
        {
            Pc objE = ListPc().Where(e => e.codigo == id).FirstOrDefault();
            return View(objE);
        }

        public ActionResult eliminarPc(int id)
        {
            Pc objC = ListPc().Where(e => e.codigo == id).FirstOrDefault();
            List<SqlParameter> lista = new List<SqlParameter>() {
             new SqlParameter(){
                ParameterName="@ID_PC",
                SqlDbType=SqlDbType.Int,
                Value=objC.codigo }
            };

            Crud("SP_ELIMINAPC", lista);
            return RedirectToAction("listadoPc");
        }

        public ActionResult modificaPc(int id)
        {
            //Metodo para la busqueda
            PcO objC = ListPcO().Where(c => c.codigo == id).FirstOrDefault();
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre");
            ViewBag.procesadores = new SelectList(ListProcesador(), "codigo", "nombre");
            ViewBag.gpus = new SelectList(ListGpu(), "codigo", "nombre");
            ViewBag.mothers = new SelectList(ListMotherboard(), "codigo", "nombre");
            ViewBag.rams = new SelectList(ListRam(), "codigo", "nombre");
            ViewBag.almacs = new SelectList(ListAlmacenamiento(), "codigo", "nombre");
            ViewBag.fuentes = new SelectList(ListFuente(), "codigo", "nombre");
            ViewBag.gabinetes = new SelectList(ListGabinete(), "codigo", "nombre");
            ViewBag.monitores = new SelectList(ListMonitor(), "codigo", "nombre");
            ViewBag.perifericos = new SelectList(ListPeriferico(), "codigo", "nombre");
            return View(objC);

        }

       [HttpPost]
        public ActionResult modificaPc(PcO objC)
        {
            if (!ModelState.IsValid)
            {
                return View(objC);
            }
            cn.Open();
            ViewBag.mensaje = "";
            //Inicio de La transaccion
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAPC", cn, tr);
                //Parametros SQL @ID_PC ,@DES , @CAT , @PROC , @GPU , @MB  @RAM , @ALM , @PSU , @GAB , @MON , @PER , @PRE , @UEX , @UPE , @FOT 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE", objC.codigo);
                cmd.Parameters.AddWithValue("@DES", objC.des_pc);
                cmd.Parameters.AddWithValue("@CAT", objC.id_cat);
                cmd.Parameters.AddWithValue("@PROC", objC.id_proc);
                cmd.Parameters.AddWithValue("@GPU", objC.id_gpu);
                cmd.Parameters.AddWithValue("@MB", objC.id_mb);
                cmd.Parameters.AddWithValue("@RAM", objC.id_ram);
                cmd.Parameters.AddWithValue("@ALM", objC.id_alm);
                cmd.Parameters.AddWithValue("@PSU", objC.id_psu);
                cmd.Parameters.AddWithValue("@GAB", objC.id_gab);
                cmd.Parameters.AddWithValue("@MON", objC.id_mon);
                cmd.Parameters.AddWithValue("@PER", objC.id_per);
                cmd.Parameters.AddWithValue("@PRE", objC.precio);
                cmd.Parameters.AddWithValue("@UEX", objC.uex_pro);
                cmd.Parameters.AddWithValue("@UPE", objC.upe_pro);
                cmd.Parameters.AddWithValue("@FOT", objC.foto);
                int n = cmd.ExecuteNonQuery();
                //Asegurar que se registren
                tr.Commit();
                ViewBag.mensaje = n.ToString() + " PC Actualizado";
            }
            catch (Exception ex)
            {
                //Recarga para el registro
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            cn.Close(); 
            ViewBag.categorias = new SelectList(ListCategoria(), "codigo", "nombre", objC.id_cat);
            ViewBag.procesadores = new SelectList(ListProcesador(), "codigo", "nombre", objC.id_proc);
            ViewBag.gpus = new SelectList(ListGpu(), "codigo", "nombre", objC.id_gpu);
            ViewBag.mothers = new SelectList(ListMotherboard(), "codigo", "nombre", objC.id_mb);
            ViewBag.rams = new SelectList(ListRam(), "codigo", "nombre", objC.id_ram);
            ViewBag.almacs = new SelectList(ListAlmacenamiento(), "codigo", "nombre", objC.id_alm);
            ViewBag.fuentes = new SelectList(ListFuente(), "codigo", "nombre", objC.id_psu);
            ViewBag.gabinetes = new SelectList(ListGabinete(), "codigo", "nombre", objC.id_gab);
            ViewBag.monitores = new SelectList(ListMonitor(), "codigo", "nombre", objC.id_mon);
            ViewBag.perifericos = new SelectList(ListPeriferico(), "codigo", "nombre", objC.id_per);
            return View(objC);
        }

    }
}