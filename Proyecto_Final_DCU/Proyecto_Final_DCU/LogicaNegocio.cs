using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Final_DCU
{
    class LogicaNegocio
    {
        private AccesoDatos _accesoDatos;

        public LogicaNegocio()
        {
            _accesoDatos = new AccesoDatos();
        }

        #region CRUD Metodos

        public Estudiante InsertarEs(Estudiante estudiante)
        {
            if (estudiante.ID == 0)
            {
                _accesoDatos.InsertarEs(estudiante);
            }
            else
            {
                MessageBox.Show("Registro que intenta agregar ya existe");
            }
            return estudiante;
        }

        public Estudiante ModificarEs(Estudiante estudiante)
        {
           _accesoDatos.ModificarEs(estudiante);
            return estudiante;
        }


        public Estudiante BorrarEs(Estudiante estudiante)
        {
           _accesoDatos.BorrarEs(estudiante);
            return estudiante;
        }

        #endregion

        #region Cargar Datos En DataGridView
        public List<Estudiante> GetEstudiante(string searchText = null)
        {
            return _accesoDatos.GetEstudiante(searchText);
        }

        #endregion
    }
}
