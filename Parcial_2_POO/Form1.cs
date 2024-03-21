using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Parcial_2_POO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            empresa = new Empresa();
            empresa.ImporteAlto += MostrarMensajeImporteAlto;

        }
        Empresa empresa;
        Regex re;
        Regex re2;
       

        //metodo para mostrar en datagridview
        private void Mostrar(DataGridView pDGV, object pO)
        {
            pDGV.DataSource = null; pDGV.DataSource = pO;
        }

        public void MostrarMensajeImporteAlto()
        { 
        MessageBox.Show("El monto supera los 10,000 pesos");
        }

        //Alta de cliente
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string Legajo = Interaction.InputBox("Ingrese Legajo: ");
                Cliente c = new Cliente(Legajo);
                if (empresa.ValidaExisteLegajo(c)) throw new Exception("El Legajo ya existe !!!");
                c.Nombre = Interaction.InputBox("Nombre: ");
                c.Apellido = Interaction.InputBox("Apellido: ");
                empresa.AltaCliente(c);
                Mostrar(dataGridView1, empresa.RetornaListaClientes());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        //Generar Cobro
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //seleccion de fila y columno del DataGridView
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Cliente para asignar !!!");
                re = new Regex(@"\d{2}.{2}");
                string Codigo = Interaction.InputBox("Ingrese Codigo: \n" + "(Formato: 99XX)");
                if (!(re.IsMatch(Codigo) && Codigo.Length == 4)) throw new Exception("El Codigo no posee el formato correcto !!!");
                CobroNormal c = new CobroNormal(Codigo, DateTime.Now.AddDays(30),0); 
                if (empresa.ValidaExisteCodigo(c)) throw new Exception("El Codigo ya existe !!!");
                c.Importe = decimal.Parse(Interaction.InputBox("Importe $: "));
                empresa.AltaCobro(c);
                empresa.AsignaCobro(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()),c);
                Mostrar(dataGridView2, empresa.RetornaListaCobrosPendientes(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView1, empresa.RetornaListaClientes());

                dataGridView1_RowEnter(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                Mostrar(dataGridView2, empresa.RetornaListaCobrosPendientes(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView3, empresa.RetornaListaCobrosPagados(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView4, null);
                Mostrar(dataGridView5, empresa.RetornaGrilla5(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
            }
            catch (Exception) { }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //seleccion de fila y columno del DataGridView
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Cliente para asignar !!!");
                re = new Regex(@"\d{2}.{2}");
                string Codigo = Interaction.InputBox("Ingrese Codigo: \n" + "(Formato: 99XX)");
                if (!(re.IsMatch(Codigo) && Codigo.Length == 4)) throw new Exception("El Codigo no posee el formato correcto !!!");
                CobroEspecial c = new CobroEspecial(Codigo, DateTime.Now.AddDays(30));
                if (empresa.ValidaExisteCodigo(c)) throw new Exception("El Codigo ya existe !!!");
                c.Importe = decimal.Parse(Interaction.InputBox("Importe $: "));
                empresa.AltaCobro(c);
                empresa.AsignaCobro(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), c);
                Mostrar(dataGridView2, empresa.RetornaListaCobrosPendientes(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView1, empresa.RetornaListaClientes());

                dataGridView1_RowEnter(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //seleccion de fila y columno del DataGridView
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Cliente seleccionado !!!");
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay Cobro seleccionado !!!");
                
                re2 = new Regex(@"\d{2}/\d{2}/\d{4}");
                string f = Interaction.InputBox("Ingrese Fecha de pago:\n" + "(Formato: DD/MM/AAAA)");
                if (!(re2.IsMatch(f) && f.Length == 10)) throw new Exception("La fecha no posee el formato correcto !!!");
                DateTime FechaCobro = DateTime.Parse(f);
                empresa.RealizarPago(dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), (new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())), FechaCobro);
                Mostrar(dataGridView3, empresa.RetornaListaCobrosPagados(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView2, empresa.RetornaListaCobrosPendientes(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                Mostrar(dataGridView1, empresa.RetornaListaClientes());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void radioButtonMayorMenor_CheckedChanged(object sender, EventArgs e)
        {
            
                try
                {
                    //seleccion de fila y columno del DataGridView
                    if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Cliente seleccionado !!!");
                    if (radioButtonMayorMenor.Checked)
                    {
                        Mostrar(dataGridView4, empresa.OrdenarMayorMenor(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButtonMenorMayor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //seleccion de fila y columno del DataGridView
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Cliente seleccionado !!!");
                if (radioButtonMenorMayor.Checked)
                {
               Mostrar(dataGridView4, empresa.OrdenarMenorMayor(new Cliente(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButtonMayorMenor_CheckedChanged_1(object sender, EventArgs e)
        {
           
        }

        private void radioButtonMenorMayor_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay nada para modificar !!!");
                DataGridViewRow f = dataGridView1.SelectedRows[0];
                Cliente c = new Cliente(f.Cells[0].Value.ToString());

                c.Nombre = Interaction.InputBox("Nombre: ", "Modificando nombre ...");
                c.Apellido = Interaction.InputBox("Apellido: ", "Modificando apellido ...");
                empresa.ModificarCliente(c);
                Mostrar(dataGridView1, empresa.RetornaListaClientes());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay nada para borrar !!!");
                DataGridViewRow f = dataGridView1.SelectedRows[0];
                Cliente c = new Cliente(f.Cells[0].Value.ToString());
                empresa.BorrarCliente(c);
                Mostrar(dataGridView1, empresa.RetornaListaClientes());
                dataGridView1_RowEnter(null, null);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
