using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2_POO
{
    //clase base cobro
    public class Cobro : IComparable<Cobro>

    {
        public Cobro() { }
        private Cliente cliente;

        private string codigo;
        private DateTime fechaDeVencimiento;
        private decimal importe;
        

        public string Codigo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Importe { get; set; }
        public Cliente Cliente { get; set; }
        

        public Cobro(string cCodigo, DateTime cFecha , decimal cImporte = 0) : this()
        {
            Codigo = cCodigo; FechaVencimiento = cFecha; Importe = cImporte;
        }

        public Cobro(Cobro cCobro) : this(cCobro.Codigo, cCobro.FechaVencimiento, cCobro.Importe)
        {

        }
        public void AsignaCliente(Cliente cCliente) { Cliente = cCliente == null ? null : new Cliente(cCliente); }
        public void ModificarDueño(Cliente cCliente) 
        {
            if (cCliente != null) Cliente.Nombre = cCliente.Nombre; Cliente.Apellido = cCliente.Apellido;
        }
        public virtual decimal CalcularImporteAdicional(DateTime f)
        {
            // Implementación genérica para el cálculo del importe adicional
            return 0;
        }
        public int CompareTo(Cobro other)
        {
            return Importe.CompareTo(other.Importe);
        }
        
    }
}
