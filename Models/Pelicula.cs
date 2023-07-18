using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIneDotNet.Models
{
    public class Pelicula
    {
        public int id { get; set; }

        public string Nombre { get; set; }

        public string Sinopsis { get; set; }

        public string Poster { get; set; }

        public List<Funcion> misFunciones { get; set; } = new List<Funcion>();

        public int Duracion { get; set; }

        public Pelicula() { }

        public Pelicula(string nombre, string sinopsis, string poster, int duracion)
        {
            
            this.Nombre = nombre;
            this.Sinopsis = sinopsis;
            this.Poster = poster;
            this.Duracion = duracion;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), Nombre, Sinopsis, Poster, Duracion.ToString()};
        }
    }
}
