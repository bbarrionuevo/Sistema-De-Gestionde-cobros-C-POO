using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parcial_2_POO
{
    public class Cliente
    {

        public Cliente()
        {
            Pagos = new List<Pago>();
            CobrosPendientes = new List<Cobro>();
            CobrosPagados = new List<Cobro>();
        }

        // nombre del cliente.
        private string nombre;
        private string apellido;

        private int legajo;

        private decimal deuda = 0;


        public List<Cobro> CobrosPendientes { get; set; }
        public List<Cobro> CobrosPagados { get; set; }
        public List<Pago> Pagos { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Legajo { get; set; }

        public decimal Deuda { get; set; }

        public Cliente(string cLegajo, string cNombre = "", string cApellido = "") : this()
        {
            Nombre = cNombre; Apellido = cApellido; Legajo = cLegajo;
        }
        public Cliente(Cliente cCliente) : this(cCliente.Nombre, cCliente.Apellido, cCliente.Legajo)
        {

        }
        public void AgregarCobro(Cobro cCobro)
        {
            CobrosPendientes.Add(cCobro);
            CalculaDeuda();
        }
        public void PagarCobro(Cobro cobro, Pago pago)
        {
            CobrosPendientes.Remove(cobro);
            CobrosPagados.Add(cobro);
            Pagos.Add(pago);
            CalculaDeuda();

        }
        public void CalculaDeuda()
        {

            Deuda = 0;

            foreach (Cobro c in CobrosPendientes)
            {
                Deuda += c.Importe;
            }

            return;
        }

    
        public List<Cobro> RetornaListaCobrosPendientes()
        {
             return CobrosPendientes;
        }
        public List<Cobro> RetornaListaCobrosPagados()
        {
        return CobrosPagados;
        }
         public List<Pago> RetornaListaPagos() 
        {
            return Pagos;
        }

        public int RetornaTamañoListaCobrosPendientes()
        {
        int r = CobrosPendientes.Count();
            return r;
        }

    }
}
