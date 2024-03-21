using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_2_POO
{
    
    public class Empresa
    {
       
            public List<Cliente> Clientes { get; set; }
            public List<Cobro> CobrosPendientes { get; set; }
            public List<Cobro> CobrosPagados { get; set; }
            public List<Pago> Pagos { get; set; }
            public event Action ImporteAlto;


            public Empresa()
            {
                Clientes = new List<Cliente>();
                CobrosPendientes = new List<Cobro>();
                CobrosPagados = new List<Cobro>();
                Pagos = new List<Pago>();
                
             }

            public void AltaCliente(Cliente cliente)
            {
                Clientes.Add(cliente);
            }

            public void BajaCliente(Cliente cliente)
            {
                Clientes.Remove(cliente);
            }

            public void ModificarCliente(Cliente cCliente)
            {
            // Implementación para modificar los datos de un cliente existente
            try
            {
                Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);
                if (c == null) throw new Exception("El Cliente que intenta modificar no existe !!!");
                c.Nombre = cCliente.Nombre;
                c.Apellido = cCliente.Apellido;
                List<Cobro> ElCliente = c.RetornaListaCobrosPendientes();
                foreach (Cobro p in ElCliente)
                {
                    Cobro auxCobro = CobrosPendientes.Find(x => x.Codigo == p.Codigo);

                    auxCobro.ModificarDueño(new Cliente(c.Nombre, c.Apellido));

                }
                List<Cobro> Elcliente = c.RetornaListaCobrosPagados();
                foreach (Cobro p in Elcliente)
                {
                    Cobro aux = CobrosPagados.Find(x => x.Codigo == p.Codigo);

                    aux.ModificarDueño(new Cliente(c.Nombre, c.Apellido));

                }

            }
            catch (Exception ex) { throw ex; }
            }
            // FUNCION PARA BORRAR 
            public void BorrarCliente(Cliente cCliente)
                {
    
                 try
                 {
                    
                    Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);
                     if (c == null) throw new Exception("El Cliente que intenta borrar no existe !!!");
                     
                     Clientes.Remove(c);

                  }
                    catch (Exception ex) { throw ex; }


                 }
            public bool ValidaExisteLegajo(Cliente cliente) 
            {
            return Clientes.Exists(x => x.Legajo == cliente.Legajo);
            }

            public bool ValidaExisteCodigo(Cobro cobro) 
            {
            return CobrosPendientes.Exists(x => x.Codigo == cobro.Codigo);
            }
                //retorna lista de clientes
            public Object RetornaListaClientes()
        
            {
                return (from c in Clientes select new { Legajo = c.Legajo, Nombre = c.Nombre, Apellido = c.Apellido, Deuda = c.Deuda}).ToArray();

            }
            //retorna lista de cobros pendientes de un cliente
            public object RetornaListaCobrosPendientes(Cliente cCliente)
            {
                Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);

                return (from p in c.RetornaListaCobrosPendientes() select new { Codigo = p.Codigo, Fecha_De_Vencimiento = p.FechaVencimiento , Importe = p.Importe }).ToArray();
            
            }

            //retorna lista de cobros pagados de un cliente
            public object RetornaListaCobrosPagados(Cliente cCliente)
            {
                Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);

                 return (from p in c.RetornaListaCobrosPagados() select new { Codigo = p.Codigo, Fecha_De_Vencimiento = p.FechaVencimiento, Importe = p.Importe }).ToArray();
               
            }
             public void AltaCobro(Cobro cobro)
            {
                CobrosPendientes.Add(cobro);
            }
            public void AsignaCobro(Cliente cCliente, Cobro cCobro)
            {

                Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);
                if (cCobro == null || c == null) throw new Exception("Uno o ambos de los elementos a asignar es nulo !!!");

            if (c.RetornaTamañoListaCobrosPendientes() > 1) { throw new Exception("El cliente tiene al menos 2 cobros pendientes"); }
            
            else
            {
                cCobro.AsignaCliente(new Cliente(c));
                c.AgregarCobro(cCobro);
            }




             }
            public void RealizarPago(string s, Cliente cliente , DateTime fFecha)
            {
            

            Cliente c = Clientes.FirstOrDefault(x => x.Legajo == cliente.Legajo);
            Cobro p = CobrosPendientes.FirstOrDefault(x => x.Codigo == s);

            if (p == null || c == null) throw new Exception("Uno o ambos de los elementos a asignar es nulo !!!");
                // Verificar si el cobro existe en la lista de pendientes
           
                // Realizar el cobro
                decimal importeAdicional = p.CalcularImporteAdicional(fFecha);
                decimal totalAPagar = p.Importe + importeAdicional;

                // Mostrar información del cobro al usuario
                MessageBox.Show($"Importe a pagar: {p.Importe}\nRecargo: {importeAdicional}\nTotal: {totalAPagar}");

                // Mover el cobro de la lista de pendientes a la lista de pagados
                

                Pago pago = new Pago(p, DateTime.UtcNow , totalAPagar , c);

                CobrosPendientes.Remove(p);
                CobrosPagados.Add(p);
                Pagos.Add(pago);
                c.PagarCobro(p, pago);
                 
                
                // Verificar si el total a pagar supera los 10,000 pesos y desencadenar el evento correspondiente
                if (totalAPagar > 10000)
                {
                    ImporteAlto.Invoke();
                }
            
               
                
            
            }


            public Object OrdenarMenorMayor (Cliente cCliente)
            {

            Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);

            List<Cobro> cobrosPagados = c.CobrosPagados;
            cobrosPagados.Sort();
            return (from p in cobrosPagados select new { Codigo = p.Codigo, Fecha_De_Vencimiento = p.FechaVencimiento, Importe = p.Importe }).ToArray();


            }

            public Object OrdenarMayorMenor (Cliente cCliente)
             {

                 Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);

                 List<Cobro> cobrosPagados = c.CobrosPagados;
                 cobrosPagados.Reverse();
                    return (from p in cobrosPagados select new { Codigo = p.Codigo, Fecha_De_Vencimiento = p.FechaVencimiento, Importe = p.Importe }).ToArray();

    
             }

            public Object RetornaGrilla5 (Cliente cCliente) 
              {
                 Cliente c = Clientes.Find(x => x.Legajo == cCliente.Legajo);

            
                return( from pago in c.RetornaListaPagos()
                   select new { NombreCliente = c.Nombre, ImporteTotalCancelado = pago.ImportePago }).ToArray();

             }




    }
}
