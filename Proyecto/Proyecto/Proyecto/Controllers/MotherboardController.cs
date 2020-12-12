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
    public class MotherboardController : Controller
    {
        // GET: Motherboard
        public ActionResult Index()
        {
            return View();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        List<MotherboardA> ListMobo()
        {
            List<MotherboardA> aGPU = new List<MotherboardA>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMOBO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aGPU.Add(new MotherboardA()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    id_prov = dr[1].ToString(),
                    nombre = dr[2].ToString(),
                    id_marca = dr[3].ToString(),
                    id_categ = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    uex = int.Parse(dr[6].ToString()),
                    upe = int.Parse(dr[7].ToString()),
                    foto = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aGPU;
        }

        public ActionResult listadoMobo()
        {
            return View(ListMobo());
        }

        List<MotherboardO> ListMoboO()
        {
            List<MotherboardO> aMobO = new List<MotherboardO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAMOBOO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMobO.Add(new MotherboardO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    id_prov = int.Parse(dr[1].ToString()),
                    nombre = dr[2].ToString(),
                    id_marca = int.Parse(dr[3].ToString()),
                    id_categ = int.Parse(dr[4].ToString()),
                    precio = double.Parse(dr[7].ToString()),
                    uex = int.Parse(dr[8].ToString()),
                    upe = int.Parse(dr[9].ToString()),
                    foto = dr[10].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aMobO;
        }

        List<ProveedorO> ListProveedor()
        {
            List<ProveedorO> aProv = new List<ProveedorO>();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAPROVEEDOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProv.Add(new ProveedorO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }

            dr.Close();
            cn.Close();
            return aProv;
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

        /****************************************/
        public ActionResult nuevaMobo()
        {
            //Para los combos
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            return View(new MotherboardO());
        }

        [HttpPost]
        public ActionResult nuevaMobo(MotherboardO objP, HttpPostedFileBase f)
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
                new SqlParameter(){ParameterName="@PRE", SqlDbType=SqlDbType.Money,Value=objP.precio},
                new SqlParameter(){ParameterName="@UEX", SqlDbType=SqlDbType.Int,Value=objP.uex},
                new SqlParameter(){ParameterName="@UPE", SqlDbType=SqlDbType.Int,Value=objP.upe},
                new SqlParameter(){ParameterName="@FOT", SqlDbType=SqlDbType.VarChar,Value="~/fotos_mobo/"+ Path.GetFileName(f.FileName)}
            };
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            Crud("SP_NUEVAMOBO", parametros);
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_mobo/"), Path.GetFileName(f.FileName)));
            return RedirectToAction("listadoMobo");
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

        public ActionResult detalleMobo(int id)
        {
            MotherboardA objE = ListMobo().Where(e => e.codigo == id).FirstOrDefault();
            return View(objE);
        }

        public ActionResult eliminarMobo(int id)
        {
            MotherboardA objC = ListMobo().Where(e => e.codigo == id).FirstOrDefault();
            List<SqlParameter> lista = new List<SqlParameter>() {
             new SqlParameter(){
                ParameterName="@ID",
                SqlDbType=SqlDbType.Int,
                Value=objC.codigo }
            };

            Crud("SP_ELIMINAMOBO", lista);
            return RedirectToAction("listadoGPU");
        }

        public ActionResult modificaMobo(int id)
        {
            //Metodo para la busqueda
            MotherboardO objC = ListMoboO().Where(c => c.codigo == id).FirstOrDefault();
            ViewBag.proveedores = new SelectList(ListProveedor(), "codigo", "nombre");
            ViewBag.marcas = new SelectList(ListMarca(), "codigo", "nombre");
            return View(objC);

        }

        [HttpPost]
        public ActionResult modificaMobo(GpuO objC)
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
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAMOBO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objC.codigo);
                cmd.Parameters.AddWithValue("@PROV", objC.id_prov);
                cmd.Parameters.AddWithValue("@DES", objC.nombre);
                cmd.Parameters.AddWithValue("@MAR", objC.id_marca);
                cmd.Parameters.AddWithValue("@PRE", objC.precio);
                cmd.Parameters.AddWithValue("@UEX", objC.uex);
                cmd.Parameters.AddWithValue("@UPE", objC.upe);
                cmd.Parameters.AddWithValue("@FOT", objC.foto);
                int n = cmd.ExecuteNonQuery();
                //Asegurar que se registren
                tr.Commit();
                ViewBag.mensaje = n.ToString() + " Motherboard Actualizado";
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
            return View(objC);
        }
    }
}