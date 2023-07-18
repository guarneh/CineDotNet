using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIneDotNet.Models
{
    public class Funcion
    {
        public int ID { get; set; }

        public int idSala { get; set; }
        public Sala miSala { get; set; }

        public int idPelicula { get; set; }

        public Pelicula miPelicula { get; set; }

        public ICollection<Usuario> clientes { get; set;} = new List<Usuario>();

        public DateTime fecha { get; set; }

        public int cantClientes { get; set; }

        public double costo { get; set; }

        public List<FuncionUsuario> funcionUsuarios { get; set; } = new List<FuncionUsuario>();

        public Funcion() { }

        public Funcion(int ID, int idSala, int idPelicula, DateTime fecha, int cantClientes, double costo) 
        {
            this.ID = ID;
            this.idSala = idSala;
            this.idPelicula = idPelicula;
            this.fecha = fecha;
            this.cantClientes = clientes.Count;
            this.costo = costo;
            
        }

        public string[] ToString()
        {
            return new string[] { ID.ToString(),idSala.ToString(), idPelicula.ToString(), fecha.ToString(), cantClientes.ToString(), costo.ToString() };
        }
    }
}
