using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Empleados_Base_Datos
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=Nomina;Integrated Security=True;";
        //string connectionString = "Server=localhost;Database=Nomina;Integrated Security=True;";
       int Id_Empleado;
        int Id_Cargo;
        int Id_Departamento;
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CargarComboBoxCargos();
            CargarComboBoxEmpleados();
            CargarComboBoxDepartamentos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Consultar los datos de la base de datos
                    string query = "SELECT * FROM Empleados";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Llenar el DataTable con los datos
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Mostrar los datos en el DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Nombre = textBox1.Text.Trim();
            string Apellido = textBox2.Text.Trim();
            string Documentoidentidad = textBox3.Text.Trim();
            string FechaIngreso = dateTimePicker1.Text.Trim();
            int Id_Cargo = (int)comboBoxCargo.SelectedValue; // Obtener el valor seleccionado del ComboBox
            int Id_Empleado = (int)comboBoxEmpleado.SelectedValue;
            int Id_Departamento = (int)comboBoxDepartamento.SelectedValue;


            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido) || string.IsNullOrWhiteSpace(Documentoidentidad) || string.IsNullOrWhiteSpace(FechaIngreso))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

 

            // Ajusta la consulta según los campos reales de tu tabla Empleados
            string query = "INSERT INTO Empleados (Nombre, Apellido, Documentoidentidad, FechaIngreso, Id_Cargo, Id_Departamento, Id_Empleado) " +
                           "VALUES (@Nombre, @Apellido, @Documentoidentidad, @FechaIngreso, @Id_Cargo, @Id_Empleado, @Id_Departamento)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = Nombre;
                        command.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50).Value = Apellido;
                        command.Parameters.Add("@DocumentoIdentidad", SqlDbType.Int).Value = Documentoidentidad;
                        command.Parameters.Add("@Id_Empleado", SqlDbType.Int).Value = Id_Empleado;
                        command.Parameters.Add("@Id_Cargo", SqlDbType.Int).Value = Id_Cargo;
                        command.Parameters.Add("@Id_Departamento", SqlDbType.Int).Value = Id_Departamento;
                        command.Parameters.Add("@FechaIngreso", SqlDbType.DateTime).Value = dateTimePicker1.Value;



                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro insertado exitosamente.");
                        }
                        else
                        {
                            MessageBox.Show("Error al insertar el registro.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void CargarComboBoxCargos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id_Cargo FROM Cargo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBoxCargo.DataSource = dt;
                comboBoxCargo.DisplayMember = "Id_Cargo";   // Lo que se muestra
                comboBoxCargo.ValueMember = "Id_Cargo";     // El valor real
            }
        }
        private void CargarComboBoxEmpleados()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id_Empleado FROM Empleados";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBoxEmpleado.DataSource = dt;
                comboBoxEmpleado.DisplayMember = "Id_Empleado";
                comboBoxEmpleado.ValueMember = "Id_Empleado";
            }
        }

        private void CargarComboBoxDepartamentos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id_Departamento FROM Departamento";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBoxDepartamento.DataSource = dt;
                comboBoxDepartamento.DisplayMember = "Id_Departamento";
                comboBoxDepartamento.ValueMember = "Id_Departamento";
            }
        }

    }
}
