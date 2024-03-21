using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2_POO
{
    public class Pago
    {
        Cliente cliente;

        Cobro Cobro;

        private string codigo;
        private decimal importePago;

        public Cliente Cliente { get; }

        public string Codigo { get; }
        public decimal ImportePago { get; }
        public DateTime Fecha { get; set;}

        public void ModificarDueño(Cliente cCliente)
        {
            if (cCliente != null) Cliente.Nombre = cCliente.Nombre; Cliente.Apellido = cCliente.Apellido;
        }

        public Pago( Cobro pCobro , DateTime pfecha, decimal pImporte , Cliente pCliente)
        {

            ImportePago = pImporte;
            Fecha = pfecha;
            Codigo = pCobro.Codigo;
            Cliente = pCliente;
            
            
        }
    }

}
