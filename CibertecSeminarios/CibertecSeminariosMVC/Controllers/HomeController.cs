using CibertecSeminariosMVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CibertecSeminariosMVC.Controllers
{
    public class HomeController : Controller
    {
        SeminarioDAL dal = new SeminarioDAL();

        public ActionResult Index()
        {
            return View(dal.ListarDisponibles());
        }

        public ActionResult Detalle(int id)
        {
            return View(dal.ObtenerPorCodigo(id));
        }

        [HttpPost]
        public ActionResult Registrar(int codigoSeminario, string codigoEstudiante)
        {
            int nro = dal.RegistrarAsistencia(codigoSeminario, codigoEstudiante);
            ViewBag.Mensaje = "Registro exitoso. N° Asistencia: " + nro;
            return View("Resultado");
        }
    }
}