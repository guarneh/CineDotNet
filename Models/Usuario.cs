using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CIneDotNet.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
        public ICollection<Funcion> MisFunciones { get; set; } = new List<Funcion>();
        public double Credito { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsAdmin { get; set; }

        public List<FuncionUsuario> Tickets { get; set; }

        public Usuario() { }

        public Usuario( int dni, string nombre, string apellido, string mail, string password, int intentos, bool bloqueo, double credit, DateTime fechaNacimiento,bool esAdmin )
        {
            
            DNI = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Mail = mail;
            this.Password = password;
            this.FechaNacimiento = fechaNacimiento;
            this.EsAdmin = esAdmin;
            this.IntentosFallidos = intentos;
            this.Bloqueado = bloqueo;
            this.Credito = credit;
            
        }   

        public string[] ToString()
        {

            return new string[] { id.ToString(),DNI.ToString(), Nombre, Apellido, Mail, Password, FechaNacimiento.ToString(), EsAdmin.ToString(), IntentosFallidos.ToString(), Bloqueado.ToString(), Credito.ToString()};

        }
    }
}
