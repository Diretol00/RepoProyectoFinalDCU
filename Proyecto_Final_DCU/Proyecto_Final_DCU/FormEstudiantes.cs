using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Final_DCU
{
    public partial class FormEstudiantes : Form
    {
        private LogicaNegocio _logicaNegocio;

        public FormEstudiantes()
        {
            InitializeComponent();
            _logicaNegocio = new LogicaNegocio();
            txtid.Enabled = false;
        }

        private void FormEstudiantes_Load(object sender, EventArgs e)
        {
            MostrarTabla();
            btnguardar.Enabled = false;
        }

        #region Metodos CRUD

        //Insertar
        private void Insertar()
        {
            Estudiante Es = new Estudiante();

            Es.Nombre = txtnombre.Text;
            Es.Apellido = txtapellido.Text;
            Es.Correo = txtcorreo.Text;
            Es.Telefono = mtxttelfono.Text;

            _logicaNegocio.InsertarEs(Es);
            MessageBox.Show("Registro guardado correctamente");
        }

        //Modificar
        private void Modificar()
        {
            Estudiante Es = new Estudiante();

            Es.IDTemp = txtid.Text;
            Es.Nombre = txtnombre.Text;
            Es.Apellido = txtapellido.Text;
            Es.Correo = txtcorreo.Text;
            Es.Telefono = mtxttelfono.Text;

            if (MessageBox.Show("Está seguro que desea modificar este registro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                _logicaNegocio.ModificarEs(Es);
                MessageBox.Show("Registro modificado correctamente");
            }
        }

        //Borrar
        private void Borrar()
        {
            Estudiante Es = new Estudiante();

            Es.IDTemp = txtid.Text;


            if (MessageBox.Show("Está seguro que desea Borrar este registro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                _logicaNegocio.BorrarEs(Es);
                Limpiar();
                MessageBox.Show("Registro borrado correctamente");
            }
        }

        #endregion


        #region Metodos
        private void MostrarTabla(string searchText = null)
        {
            List<Estudiante> estudiante = _logicaNegocio.GetEstudiante(searchText);
            dgvEs.DataSource = estudiante;
        }

        private void Limpiar()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtcorreo.Text = "";
            mtxttelfono.Text = "";

            txtnombre.Focus();
        }

        #endregion


        #region Eventos

        //Nuevo
        private void btnnuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        //Modificar
        private void btnmodificar_Click(object sender, EventArgs e)
        {
            Modificar();
            MostrarTabla();
        }

        //Borrar
        private void btnborrar_Click(object sender, EventArgs e)
        {
            Borrar();
            MostrarTabla();
        }

        //Guardar
        private void btnguardar_Click(object sender, EventArgs e)
        {
            Insertar();
            MostrarTabla();
        }

        //Llenar campos
        private void dgvEs_SelectionChanged(object sender, EventArgs e)
        {
            var row = (sender as DataGridView).CurrentRow;

            txtid.Text = row.Cells[0].Value.ToString();
            txtnombre.Text = row.Cells[1].Value.ToString();
            txtapellido.Text = row.Cells[2].Value.ToString();
            txtcorreo.Text = row.Cells[3].Value.ToString();
            mtxttelfono.Text = row.Cells[4].Value.ToString();
        }

        //Buscar
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            MostrarTabla(txtBuscar.Text);
            txtBuscar.Text = string.Empty;
        }

        //Volver
        private void btnvolver_Click(object sender, EventArgs e)
        {
            FormPrincipal principal = new FormPrincipal();
            if (MessageBox.Show("Esta Seguro que Desea Volver?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                principal.Show();
                this.Hide();
            }
            
        }

        //Salir
        private void btnsalir2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta Seguro que Desea Salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }



        #endregion


        #region Validar para no dejar campos vacios

        //Nombre
        private void txtnombre_Validated(object sender, EventArgs e)
        {
            if (txtnombre.Text == string.Empty)
            {
                MessageBox.Show("Debe Completar el Campo");
                txtnombre.Focus();
            }

            btnguardar.Enabled = true;
        }

        //Apellido
        private void txtapellido_Validated(object sender, EventArgs e)
        {
            if (txtapellido.Text == string.Empty)
            {
                MessageBox.Show("Debe Completar el Campo");
                txtapellido.Focus();
            }
        }

        //Correo
        private void txtcorreo_Validated(object sender, EventArgs e)
        {
            if (txtcorreo.Text == string.Empty)
            {
                MessageBox.Show("Debe Completar el Campo");
                txtcorreo.Focus();
            }
        }

        //Telefono
        private void mtxttelfono_Validated(object sender, EventArgs e)
        {
            if (mtxttelfono.Text == string.Empty)
            {
                MessageBox.Show("Debe Completar el Campo");
                mtxttelfono.Focus();
            }
        }




        #endregion


        #region Validar que sean solo letras

        //Nombre
        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo acepta letras");
            }
        }

        //Apellido
        private void txtapellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo acepta letras");
            }
        }

        #endregion

        
    }
}
