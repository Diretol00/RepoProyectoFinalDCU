using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DCU
{
    class AccesoDatos
    {
        private SqlConnection cnn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Estudiante_DCU;Data Source=LAPTOP-6NLFL1U0\\SQLEXPRESS");

        #region Insertar

        public void InsertarEs(Estudiante estudiante)
        {
            try
            {
                cnn.Open();
                string query = @"
                                Insert into Estudiante (Nombre, Apellido, Correo_Electronico, Telefono)
                                Values(@Nombre, @Apellido, @Correo, @Telefono)";

                SqlParameter Nombre = new SqlParameter("@Nombre", estudiante.Nombre);
                SqlParameter Apellido = new SqlParameter("@Apellido", estudiante.Apellido);
                SqlParameter Correo = new SqlParameter("@Correo", estudiante.Correo);
                SqlParameter Telefono = new SqlParameter("@Telefono", estudiante.Telefono);

                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.Add(Nombre);
                command.Parameters.Add(Apellido);
                command.Parameters.Add(Correo);
                command.Parameters.Add(Telefono);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }

        #endregion


        #region Modificar
        public void ModificarEs(Estudiante estudiante)
        {
            try
            {
                cnn.Open();
                string query = @"
                                Update Estudiante set Nombre = @Nombre, Apellido = @Apellido, Correo_Electronico = @Correo, Telefono = @Telefono
                                    where ID = @IDTemp";

                SqlParameter IDTemp = new SqlParameter("@IDTemp", estudiante.IDTemp);
                SqlParameter Nombre = new SqlParameter("@Nombre", estudiante.Nombre);
                SqlParameter Apellido = new SqlParameter("@Apellido", estudiante.Apellido);
                SqlParameter Correo = new SqlParameter("@Correo", estudiante.Correo);
                SqlParameter Telefono = new SqlParameter("@Telefono", estudiante.Telefono);

                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.Add(IDTemp);
                command.Parameters.Add(Nombre);
                command.Parameters.Add(Apellido);
                command.Parameters.Add(Correo);
                command.Parameters.Add(Telefono);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        #endregion


        #region Borrar
        public void BorrarEs(Estudiante estudiante)
        {
            try
            {
                cnn.Open();
                string query = @"
                                Delete Estudiante where ID = @IDTemp";

                SqlParameter IDTemp = new SqlParameter("@IDTemp", estudiante.IDTemp);

                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.Add(IDTemp);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        #endregion


        #region Llenar DataGridView 
        public List<Estudiante> GetEstudiante(string search = null)
        {
            List<Estudiante> estudiante = new List<Estudiante>();
            try
            {


                cnn.Open();
                string query = @"
                              Select ID,Nombre,Apellido,Correo_Electronico, Telefono
                                From Estudiante ";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query += @"Where ID Like @Search Or Nombre Like @Search Or Apellido Like @Search Or Correo_Electronico Like @Search or Telefono Like @Search ";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = cnn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estudiante.Add(new Estudiante
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Correo = reader["Correo_Electronico"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
            return estudiante;


        }

        #endregion
    }
}
