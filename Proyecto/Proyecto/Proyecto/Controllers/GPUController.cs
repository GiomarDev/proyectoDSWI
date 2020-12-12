using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class GPUController : Controller
    {
        // GET: GPU
        public ActionResult Index()
        {
            return View();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        List<GpuA> ListGPU()
        {
            List<GpuA> aGPU = new List<GpuA>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAGPU", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aGPU.Add(new GpuA()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    id_prov = dr[1].ToString(),
                    nombre = dr[2].ToString(),
                    id_marca = dr[3].ToString(),
                    id_categ = dr[4].ToString(),
                    mem_gpu = int.Parse(dr[5].ToString()),
                    id_uni_med = dr[6].ToString(),
                    precio = double.Parse(dr[7].ToString()),
                    uex = int.Parse(dr[8].ToString()),
                    upe = int.Parse(dr[9].ToString()),
                    foto = dr[10].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aGPU;
        }

        public ActionResult listadoGPU()
        {
            return View(ListGPU());
        }

        List<GpuO> ListGPUO()
        {
            List<GpuO> aGPUO = new List<GpuO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAGPUO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aGPUO.Add(new GpuO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    id_prov = int.Parse(dr[1].ToString()),
                    nombre = dr[2].ToString(),
                    id_marca = int.Parse(dr[3].ToString()),
                    id_categ = int.Parse(dr[4].ToString()),
                    mem_gpu = int.Parse(dr[5].ToString()),
                    id_uni_med = int.Parse(dr[6].ToString()),
                    precio = double.Parse(dr[7].ToString()),
                    uex = int.Parse(dr[8].ToString()),
                    upe = int.Parse(dr[9].ToString()),
                    foto = dr[10].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aGPUO;
        }

        List<ProveedorO> ListProveedor()
        {
            List<ProveedorO> aRam = new List<ProveedorO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAPROVEEDOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aRam.Add(new ProveedorO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aRam;
        }

        List<Marca> ListMarca()
        {
            List<Marca> aMarca = new List<Marca>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMARCA", cn);
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

        List<UnidadMedida> ListUnidadMedida()
        {
            List<UnidadMedida> aUniM = new List<UnidadMedida>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMEDIDA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aUniM.Add(new UnidadMedida()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aUniM;
        }

        /****************************************/
        public ActionResult nuevaGPU()
        {
            //Para los combos
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            ViewBag.medidas = new SelectList(ListUnidadMedida(), "codigo", "nombre");
            return View(new GpuO());
        }

        [HttpPost]
        public ActionResult nuevaGPU(GpuO objP, HttpPostedFileBase f)
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
                new SqlParameter(){ParameterName="@PROV", SqlDbType=SqlDbType.Int,Value=objP.id_prov},
                new SqlParameter(){ParameterName="@DES", SqlDbType=SqlDbType.VarChar,Value=objP.nombre},
                new SqlParameter(){ParameterName="@MAR", SqlDbType=SqlDbType.Int,Value=objP.id_marca},
                new SqlParameter(){ParameterName="@MEM", SqlDbType=SqlDbType.Int,Value=objP.mem_gpu},
                new SqlParameter(){ParameterName="@UNIM", SqlDbType=SqlDbType.Int,Value=objP.id_uni_med},
                new SqlParameter(){ParameterName="@PRE", SqlDbType=SqlDbType.Money,Value=objP.precio},
                new SqlParameter(){ParameterName="@UEX", SqlDbType=SqlDbType.Int,Value=objP.uex},
                new SqlParameter(){ParameterName="@UPE", SqlDbType=SqlDbType.Int,Value=objP.upe},
                new SqlParameter(){ParameterName="@FOT", SqlDbType=SqlDbType.VarChar,Value="~/fotos_gpu/"+ Path.GetFileName(f.FileName)}
            };
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            ViewBag.medidas = new SelectList(ListUnidadMedida(), "codigo", "nombre");
            Crud("SP_NUEVAGPU", parametros);
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_gpu/"), Path.GetFileName(f.FileName)));
            return RedirectToAction("listadoGPU");
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

        public ActionResult detalleGPU(int id)
        {
            GpuA objE = ListGPU().Where(e => e.codigo == id).FirstOrDefault();
            return View(objE);
        }

        public ActionResult eliminarGPU(int id)
        {
            GpuA objC = ListGPU().Where(e => e.codigo == id).FirstOrDefault();
            List<SqlParameter> lista = new List<SqlParameter>() {
             new SqlParameter(){
                ParameterName="@ID",
                SqlDbType=SqlDbType.Int,
                Value=objC.codigo }
            };

            Crud("SP_ELIMINAGPU", lista);
            return RedirectToAction("listadoGPU");
        }

        public ActionResult modificaGPU(int id)
        {
            //Metodo para la busqueda
            GpuO objC = ListGPUO().Where(c => c.codigo == id).FirstOrDefault();
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            ViewBag.medidas = new SelectList(ListUnidadMedida(), "codigo", "nombre");
            return View(objC);

        }

        [HttpPost]
        public ActionResult modificaGPU(GpuO objC)
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
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAGPU", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objC.codigo);
                cmd.Parameters.AddWithValue("@PROV", objC.id_prov);
                cmd.Parameters.AddWithValue("@DES", objC.nombre);
                cmd.Parameters.AddWithValue("@MAR", objC.id_marca);
                cmd.Parameters.AddWithValue("@MEM", objC.mem_gpu);
                cmd.Parameters.AddWithValue("@UNIM", objC.id_uni_med);
                cmd.Parameters.AddWithValue("@PRE", objC.precio);
                cmd.Parameters.AddWithValue("@UEX", objC.uex);
                cmd.Parameters.AddWithValue("@UPE", objC.upe);
                cmd.Parameters.AddWithValue("@FOT", objC.foto);
                int n = cmd.ExecuteNonQuery();
                //Asegurar que se registren
                tr.Commit();
                ViewBag.mensaje = n.ToString() + " GPU Actualizado";
            }
            catch (Exception ex)
            {
                //Recarga para el registro
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            cn.Close();
            ViewBag.proveedors = new SelectList(ListProveedor(), "codigo", "nombre", objC.id_prov);
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre", objC.id_marca);
            ViewBag.medidas = new SelectList(ListUnidadMedida(), "codigo", "nombre", objC.id_uni_med);
            return View(objC);
        }
    }
}