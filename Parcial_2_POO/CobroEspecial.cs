using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2_POO
{
    public class CobroEspecial : Cobro
    {


        

        public CobroEspecial(string cCodigo, DateTime cFecha, decimal cImporte = 0) : base(cCodigo, cFecha, cImporte)
        { }
        
        public override decimal CalcularImporteAdicional(DateTime f)
        {
            int comparacion = FechaVencimiento.CompareTo(f);

            
            if (comparacion < 0)
            {
                return Importe * 0.2m; // Aplica un 20% adicional
            }
            else
                return Importe * 0;
                
        }
    }
}
