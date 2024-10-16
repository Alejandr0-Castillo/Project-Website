using System.Data;

namespace WindowsForms1
{
    public partial class Form1 : Form
    {
        DataTable dtAlumno = new DataTable("Alumnos"); // Crea una nueva tabla de datos para almacenar información de alumnos
        
        public Form1()
        {
            InitializeComponent(); // Inicializa los componentes del formulario
            CargarAsignatura(); // Llama al método para cargar las asignaturas en los comboboxes
        }
        
        private void CargarAsignatura()
        {
            // Agrega opciones al combobox de registro de asignatura
            cmbRegAsignatura.Items.Insert(0, "Seleccione");
            cmbRegAsignatura.Items.Insert(1, "Programación");
            cmbRegAsignatura.Items.Insert(2, "Base de datos");
            cmbRegAsignatura.Items.Insert(3, "Diseño Web");

            cmbRegAsignatura.SelectedIndex = 0; // Selecciona la primera opción por defecto

            // Agrega las mismas opciones al combobox de filtro de asignatura
            cmbFiltroAsignatura.Items.Insert(0, "Seleccione");
            cmbFiltroAsignatura.Items.Insert(1, "Programación");
            cmbFiltroAsignatura.Items.Insert(2, "Base de datos");
            cmbFiltroAsignatura.Items.Insert(3, "Diseño Web");

            cmbFiltroAsignatura.SelectedIndex = 0; // Selecciona la primera opción por defecto
        }
        
        private void Limpiar()
        {
            txtBoxRegNombre.Clear(); // Limpia el campo de nombre
            txtBoxRegNombre.Focus(); // Pone el foco en el campo de nombre
            nudRegSemestre.Value = 0; // Reinicia el valor del semestre
            cmbRegAsignatura.SelectedIndex = 0; // Reinicia la selección de asignatura
        }
        
        private DataTable generarTabla(string nombre, decimal semestre, string asignatura, bool esVacio)
        {
            try
            {
                if (esVacio) // Si la tabla está vacía, agrega las columnas
                {
                    dtAlumno.Columns.Add("Nombre_Alumno", typeof(string));
                    dtAlumno.Columns.Add("Semestre_Actual", typeof(decimal));
                    dtAlumno.Columns.Add("Asignatura", typeof(string));
                }
                DataRow fila = dtAlumno.NewRow(); // Crea una nueva fila
                fila["Nombre_Alumno"] = nombre; // Asigna el nombre
                fila["Semestre_Actual"] = semestre; // Asigna el semestre
                fila["Asignatura"] = asignatura; // Asigna la asignatura

                dtAlumno.Rows.Add(fila); // Agrega la fila a la tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); // Muestra cualquier error que ocurra
            }
            return dtAlumno; // Devuelve la tabla actualizada
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            // Verifica si todos los campos necesarios están llenos
            if (!string.IsNullOrEmpty(txtBoxRegNombre.Text) && nudRegSemestre.Value > 0
                && cmbRegAsignatura.SelectedIndex > 0)
            {
                // Genera la tabla y la asigna como fuente de datos del DataGridView
                dgv.DataSource = generarTabla(txtBoxRegNombre.Text, nudRegSemestre.Value,
                    cmbRegAsignatura.Text, dgv.RowCount == 0); 
                Limpiar(); // Limpia los campos después de guardar
            }
            else
            {
                // Muestra mensajes de error si falta algún dato
                if (string.IsNullOrEmpty(txtBoxRegNombre.Text)) MessageBox.Show("Ingrese Nombre Alumno.");
                if (nudRegSemestre.Value == 0) MessageBox.Show("Ingrese Semestre Valido.");
                if (cmbRegAsignatura.SelectedIndex == 0) MessageBox.Show("Seleccione una asignatura.");
            }
        }

        private void btFiltrar_Click(object sender, EventArgs e)
        {
            string consulta = ""; // Inicializa la cadena de consulta
            DataRow[] datosFiltrados = null; // Inicializa el array para los datos filtrados

            // Si no hay filtros, muestra todos los datos
            if(string.IsNullOrEmpty(txtBoxFiltroNombre.Text) && nudFiltroSemestre.Value == 0 &&
                cmbFiltroAsignatura.SelectedIndex == 0)
            {
                dgv.DataSource = dtAlumno;
            }
            
            // Construye la consulta basada en los filtros seleccionados
            if (!string.IsNullOrEmpty(txtBoxFiltroNombre.Text))
            {
                consulta = "Nombre_Alumno like '" + txtBoxFiltroNombre.Text + "'";
            }
            if(nudFiltroSemestre.Value > 0)
            {
                if (!string.IsNullOrEmpty(consulta))
                {
                    consulta = consulta + " and Semestre_Actual =" + nudFiltroSemestre.Value;
                }
                else
                {
                    consulta = "Semestre_Actual =" + nudFiltroSemestre.Value;
                }
            }
            if(cmbFiltroAsignatura.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(consulta))
                {
                    consulta = consulta + " and Asignatura like '" + cmbFiltroAsignatura.SelectedIndex + "'";
                }
                else
                {
                    consulta = "Asignatura like '" + cmbFiltroAsignatura.Text + "'";
                }
            }
            datosFiltrados = dtAlumno.Select(consulta); // Aplica el filtro
            if (datosFiltrados.Length > 0)
            {
                dgv.DataSource = datosFiltrados.CopyToDataTable(); // Muestra los datos filtrados
            }
            else
            {
                dgv.DataSource = null; // Si no hay resultados, limpia el DataGridView
            }
        }
        
        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null) // Verifica si hay una fila seleccionada
            {
                int fila = dgv.CurrentRow.Index; // Obtiene el índice de la fila seleccionada
                dtAlumno.Rows.RemoveAt(fila); // Elimina la fila de la tabla de datos
                dgv.DataSource = dtAlumno; // Actualiza el DataGridView
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar."); // Muestra un mensaje si no hay fila seleccionada
            }
        }
    }
}
