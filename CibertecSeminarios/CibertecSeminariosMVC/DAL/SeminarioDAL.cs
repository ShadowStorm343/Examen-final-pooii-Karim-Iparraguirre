using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CibertecSeminariosMVC.Models;

namespace CibertecSeminariosMVC.DAL
{
    public class SeminarioDAL
    {
        string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        public List<Seminario> ListarDisponibles()
        {
            List<Seminario> lista = new List<Seminario>();

            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarSeminariosDisponibles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Seminario
                    {
                        CodigoSeminario = (int)dr["CodigoSeminario"],
                        NombreCurso = dr["NombreCurso"].ToString(),
                        HorarioClase = dr["HorarioClase"].ToString(),
                        Capacidad = (int)dr["Capacidad"],
                        FotoUrl = dr["FotoUrl"].ToString()
                    });
                }
            }
            return lista;
        }

        public Seminario ObtenerPorCodigo(int codigo)
        {
            Seminario s = null;

            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerSeminarioPorCodigo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodigoSeminario", codigo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    s = new Seminario
                    {
                        CodigoSeminario = (int)dr["CodigoSeminario"],
                        NombreCurso = dr["NombreCurso"].ToString(),
                        HorarioClase = dr["HorarioClase"].ToString(),
                        Capacidad = (int)dr["Capacidad"],
                        FotoUrl = dr["FotoUrl"].ToString()
                    };
                }
            }
            return s;
        }

        public int RegistrarAsistencia(int codigoSeminario, string codigoEstudiante)
        {
            int nroRegistro;

            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("usp_RegistrarAsistencia", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CodigoSeminario", codigoSeminario);
                cmd.Parameters.AddWithValue("@CodigoEstudiante", codigoEstudiante);

                SqlParameter output = new SqlParameter("@NumeroRegistro", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(output);

                con.Open();
                cmd.ExecuteNonQuery();

                nroRegistro = (int)output.Value;
            }

            return nroRegistro;
        }
    }
}