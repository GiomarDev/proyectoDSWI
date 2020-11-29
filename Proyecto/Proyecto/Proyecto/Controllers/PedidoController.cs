using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Utils;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Proyecto.Controllers
{
    public class PedidoController : Controller
    {
        Conexion objCon = new Conexion();
        SqlConnection cn = null;

        List<SP_LISTAPEDIDOS_Result> listPedido()
        {
            List<SP_LISTAPEDIDOS_Result> aPedidos = new List<SP_LISTAPEDIDOS_Result>();
            cn = objCon.getConecta();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SP_LISTAPEDIDOS_Result objP = new SP_LISTAPEDIDOS_Result()
                {
                    IDE_PED = int.Parse(dr[0].ToString()),
                    NOM_CLI = dr[1].ToString(),
                    EMPLEADO = dr[2].ToString(),
                    FEC_PED = DateTime.Parse(dr[3].ToString()),
                    FEN_PED = DateTime.Parse(dr[4].ToString()),
                    PAI_PED = dr[5].ToString()
                };
                aPedidos.Add(objP);
            }
            dr.Close();
            cn.Close();
            return aPedidos;
        }

        List<SP_LISTAPEDIDOSxFECHAS_Result> listPedidoXFecha(DateTime f1, DateTime f2)
        {
            List<SP_LISTAPEDIDOSxFECHAS_Result> aPedidos = new List<SP_LISTAPEDIDOSxFECHAS_Result>();
            cn = objCon.getConecta();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOSxFECHAS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@f1", SqlDbType.Date).Value = f1;
            cmd.Parameters.Add("@f2", SqlDbType.Date).Value = f2;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SP_LISTAPEDIDOSxFECHAS_Result objP = new SP_LISTAPEDIDOSxFECHAS_Result()
                {
                    IDE_PED = int.Parse(dr[0].ToString()),
                    NOM_CLI = dr[1].ToString(),
                    EMPLEADO = dr[2].ToString(),
                    FEC_PED = DateTime.Parse(dr[3].ToString()),
                    FEN_PED = DateTime.Parse(dr[4].ToString()),
                    PAI_PED = dr[5].ToString()
                };
                aPedidos.Add(objP);
            }
            dr.Close();
            cn.Close();
            return aPedidos;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadoPedidos()
        {
            return View(listPedido());
        }

        public ActionResult listadoPedidosXFechas(DateTime? f1, DateTime? f2)
        {
            ViewBag.f1 = f1;
            ViewBag.f2 = f2;
            return View(listPedidoXFecha(f1.GetValueOrDefault(),
                                        f2.GetValueOrDefault()
                                        ));
        }

        //Metodos Especiales para Reportes
        public ActionResult reportePedidosPDF()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "crPedidos.rpt"));
            rd.SetDataSource(listPedido());

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = false;

            try
            {
                Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);
                return File(st, "application/pdf", "ReportePedidos.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult reportePedidosPDFXFechas(DateTime f1, DateTime f2)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "crPedidos.rpt"));
            rd.SetDataSource(listPedidoXFecha(f1, f2));

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = false;

            try
            {
                Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);
                return File(st, "application/pdf", "ReportePedidosxFechas.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public ActionResult reportePedidosEXEL()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "crPedidos.rpt"));
        //    rd.SetDataSource(listPedido());

        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.Buffer = false;

        //    try
        //    {
        //        Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        //        st.Seek(0, SeekOrigin.Begin);
        //        return File(st, "application/excel", "ReportePedidos.xlsx");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


    }
}