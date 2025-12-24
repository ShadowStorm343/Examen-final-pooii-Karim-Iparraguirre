using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CibertecSeminariosMVC.Models
{
    public class Seminario
    {
        public int CodigoSeminario { get; set; }
        public string NombreCurso { get; set; }
        public string HorarioClase { get; set; }
        public int Capacidad { get; set; }
        public string FotoUrl { get; set; }
    }

}