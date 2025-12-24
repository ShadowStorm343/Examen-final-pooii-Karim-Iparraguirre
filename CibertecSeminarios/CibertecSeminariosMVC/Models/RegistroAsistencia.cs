using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CibertecSeminariosMVC.Models
{
    public class RegistroAsistencia
    {
        public int NumeroRegistro { get; set; }
        public int CodigoSeminario { get; set; }
        public string CodigoEstudiante { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}