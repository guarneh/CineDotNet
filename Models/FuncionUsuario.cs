using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIneDotNet.Models
{
    public class FuncionUsuario
    {
        public int idUsuario;
        public Usuario usuario { get; set; }

        public int idFuncion;
        public Funcion funcion { get; set; }

        public int cantEntradas;

        public FuncionUsuario() { }
        public FuncionUsuario(int idUsuario, int idFuncion, int cantEntradas)
        { 
            this.idUsuario = idUsuario;
            this.idFuncion = idFuncion;
            this.cantEntradas = cantEntradas;
        }



    }
}
