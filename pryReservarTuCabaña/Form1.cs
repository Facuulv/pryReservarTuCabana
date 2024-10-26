using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryReservarTuCabaña
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Funcion para colocar los valores por defecto de la interfaz
        public void ValoresporDefecto()
        {
            cmbCabañas.Items.Clear();
            cmbCabañas.Items.Add("Cabaña A");
            cmbCabañas.Items.Add("Cabaña B");

            txtDias.Text = 1.ToString();
            rdbEfectivo.Checked = true;
            cmbTarjetas.Enabled = false;

            txtNombre.Text = "";
            txtTelefono.Text = "";
            chbCocina.Checked = false;
            chbHeladera.Checked = false;
            chbTelevision.Checked = false;

            montoCabaña = 0;
            montoPorPersona = 0;
            personas = 0;
            total = 0;
        }

        //Funcion que habilita el boton Calcular dependiendo si las 3 cajas de texto tienen valores
        public void HabilitarBoton()
        {
            int dias = 0;
            if (txtNombre.Text != "" && txtTelefono.Text != "" && int.TryParse(txtDias.Text, out dias) && dias > 0)
            {
                btnCalcular.Enabled = true;
            } else
            {
                btnCalcular.Enabled = false;
            }
        }

        //Funciones para habilitar el boton y colocar los valores por defecto en el Formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            ValoresporDefecto();
            HabilitarBoton();
            cmbTarjetas.Items.Add("Card Red");
            cmbTarjetas.Items.Add("Card Blue");
            cmbTarjetas.Items.Add("Card Green");
            cmbCabañas.SelectedIndex = 0;
            cmbPersonas.SelectedIndex = 0;
        }

        //Variables globales
        int montoCabaña = 0;
        int personas = 0;
        int montoPorPersona = 0;
        int total = 0;

        //Segun el tipo de cabaña el comboBox Personas se coloca de 1 a 4 personas o de 1 a 8 personas
        private void cmbCabañas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int limite = 0;
            cmbPersonas.Items.Clear();

            switch (cmbCabañas.SelectedIndex)
            {
                case 0:
                    limite = 5;
                    montoCabaña = 20;
                    break;
                case 1:
                    limite = 9;
                    montoCabaña = 34;
                    break;
            }
            for (int i = 1; i < limite; i++)
            {
                cmbPersonas.Items.Add(i.ToString());
            }
        }

        //Personas toma el valor del comboBox Personas y montoPorPersona aumenta el valor de personas
        private void cmbPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            personas = int.Parse(cmbPersonas.Text);
            montoPorPersona = personas++;
        }

        //Llamar la funcion HabilitarBoton, corroborar si cambio el texto y habilita el boton
        private void txtDias_TextChanged(object sender, EventArgs e)
        {
            HabilitarBoton();
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            HabilitarBoton();
        }
        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            HabilitarBoton();
        }

        //Si se selecciona el radioButton Tarjeta se habilita el comboBox de Tarjetas
        private void rdbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTarjeta.Checked)
            {
                cmbTarjetas.Enabled = true;
                cmbTarjetas.SelectedIndex = 0;
            } else
            {
                cmbTarjetas.Enabled = false;
                cmbTarjetas.SelectedIndex = -1;
            }
        }

        //Calculo de Boton de comando Calcular
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            decimal sinRecargo = montoCabaña + montoPorPersona;
           
            decimal suma = 0;
            if(chbCocina.Checked)
            {
                suma += 1;                
            }
            if (chbHeladera.Checked)
            {
                suma += 1.5m;
            }
            if (chbTelevision.Checked)
            {
                suma += 2;
            }

            sinRecargo += suma;

            decimal recargoPorTarjeta = 0;
            switch (cmbTarjetas.SelectedIndex)
            {
                case 0:
                    recargoPorTarjeta += sinRecargo * 0.1m;
                    break;
                case 1:
                case 2:
                    recargoPorTarjeta += sinRecargo * 0.2m;
                    break;
            }

            decimal totalFinal = sinRecargo + recargoPorTarjeta;
            txtResultado.Text = "$" + totalFinal.ToString();
            total = 0;

            ValoresporDefecto();
            cmbCabañas.SelectedIndex = 0;
            cmbPersonas.SelectedIndex = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
