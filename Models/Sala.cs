using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CIneDotNet.Models
{
    public class Sala
    {
        public int id { get; set; }

        public string ubicacion { get; set; }

        public int capacidad { get; set; }

        public List<Funcion> misFunciones { get; set; } = new List<Funcion>();

        public Sala() { }

        public Sala(int id, string ubicacion, int capacidad) { 

            this.id = id;
            this.ubicacion = ubicacion;
            this.capacidad = capacidad;
            
        
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), ubicacion, capacidad.ToString()};
        }


        


    }
}
