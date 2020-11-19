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
                    nom_pro = dr[2].ToString(),
                    des_ram = dr[3].ToString(),
                    des_alm = dr[4].ToString(),
                    des_psu = dr[5].ToString(),
                    precio = double.Parse(dr[6].ToString()),
                    stock = int.Parse(dr[7].ToString()),
                    foto = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPc;
        }



        public ActionResult Index()
        {
            return View();
        }


        public ActionResult listadoPc()
        {
            return View(ListPc());
        }
    }
}