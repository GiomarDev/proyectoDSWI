using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        //Cambian de Conexion
        SqlConnection con = new SqlConnection("server=GONDAR\\MSSQLSERVER01;database=COMPULAST;Integrated Security=true");


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult iniciarSesion(string codigo, string pass)
        {
            int resultado = -1;

            SqlCommand sqlCommand = new SqlCommand("SP_BUSCAR_USUARIO", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@p_usuari_codigo", codigo);
            sqlCommand.Parameters.AddWithValue("@p_usuari_pass", pass);

            con.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                Usuario usuario = new Usuario();
                usuario.codigo = sqlDataReader.GetString(0);
                usuario.pass = sqlDataReader.GetString(1);
                usuario.apellidoPaterno = sqlDataReader.GetString(2);
                usuario.apellidoMaterno = sqlDataReader.GetString(3);
                usuario.nombres = sqlDataReader.GetString(4);
                usuario.correo = sqlDataReader.GetString(5);
                usuario.telefono = sqlDataReader.GetString(6);
                resultado = 1;
            }
            sqlDataReader.Close();
            con.Close();

            if (resultado == 1)
            {
                return RedirectToAction("Index", "Home", new { USUARIO = codigo });
            }
            else
            {
                TempData["Error"] = "Usuario y/o contraseña incorrecta";
                return RedirectToAction("Index");
            }
        }


    }
}