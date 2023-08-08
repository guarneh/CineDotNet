using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CIneDotNet.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }    

        public DbSet<Pelicula> peliculas { get; set; }

        public DbSet<Sala> salas { get; set; }

        public DbSet<Funcion> funciones { get; set; } 

        public DbSet<FuncionUsuario> funcionUsuarios { get; set; }

        public MyContext() { }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            //var connectionString = configuration.GetConnectionString("trabajo");
            //optionsBuilder.UseSqlServer(connectionString);

            var connectionString = configuration.GetConnectionString("casa");
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //agrego tablas

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.id);

            modelBuilder.Entity<Pelicula>()
                .ToTable("Peliculas")
                .HasKey(p => p.id);

            modelBuilder.Entity<Sala>()
                .ToTable("Salas")
                .HasKey(s => s.id);

            modelBuilder.Entity<Funcion>()
                .ToTable("Funciones")
                .HasKey(f => f.ID);

            //agrego relaciones

            modelBuilder.Entity<Funcion>()
                .HasOne(f => f.miPelicula)
                .WithMany(p => p.misFunciones)
                .HasForeignKey(f => f.idPelicula)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Funcion>()
                .HasOne(f => f.miSala)
                .WithMany(s => s.misFunciones)
                .HasForeignKey(f => f.idSala)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.MisFunciones)
                .WithMany(f => f.clientes)
                .UsingEntity<FuncionUsuario>(
                uf => uf.HasOne(uf => uf.funcion).WithMany(f => f.funcionUsuarios).HasForeignKey(f => f.idFuncion),
                uf => uf.HasOne(uf => uf.usuario).WithMany(u => u.Tickets).HasForeignKey(u => u.idUsuario),
                uf => uf.HasKey(k => new {k.idUsuario, k.idFuncion })
                );

            //propiedades de los datos
            modelBuilder.Entity<Usuario>(
                usr => 
                {
                    usr.Property(u => u.DNI).HasColumnType("int");
                    usr.Property(u => u.Nombre).HasColumnType("varchar(50)");
                    usr.Property(u => u.Apellido).HasColumnType("varchar(50)");
                    usr.Property(u => u.Mail).HasColumnType("varchar(512)");
                    usr.Property(u => u.Password).HasColumnType("varchar(50)");
                    usr.Property(u => u.FechaNacimiento).HasColumnType("date");
                    usr.Property(u => u.Credito).HasColumnType("float");
                    usr.Property(u => u.IntentosFallidos).HasColumnType("int");
                    usr.Property(u => u.Bloqueado).HasColumnType("bit");
                    usr.Property(u => u.EsAdmin).HasColumnType("bit");
                });
            modelBuilder.Entity<FuncionUsuario>(
                fu =>
                { 
                    fu.Property(fu => fu.cantEntradas).HasColumnType("int");
                });

            modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                id = 1,
                DNI = 12345678,
                Nombre = "John",
                Apellido = "Doe",
                Mail = "johndoe@gmail.com",
                Password = "1234",
                IntentosFallidos = 0,
                Bloqueado = false,
                Credito = 10000.00,
                FechaNacimiento = new DateTime(1990, 1, 1),
                EsAdmin = true
            },
            new Usuario
            {
                id = 2,
                DNI = 98765432,
                Nombre = "Jane",
                Apellido = "Doe",
                Mail = "janedoe@gmail.com",
                Password = "1234",
                IntentosFallidos = 0,
                Bloqueado = false,
                Credito = 10000.00,
                FechaNacimiento = new DateTime(1985, 5, 15),
                EsAdmin = false
            });

            modelBuilder.Entity<Sala>().HasData(
            new Sala
            {
                id = 1,
                ubicacion = "Sala A",
                capacidad = 50
            },
            new Sala
            {
                id = 2,
                ubicacion = "Sala B",
                capacidad = 30
            });


            modelBuilder.Entity<Pelicula>().HasData(
            new Pelicula
            {
                id = 1,
                Nombre = "Barbie",
                Sinopsis = "Después de ser expulsada de Barbieland por no ser una muñeca de aspecto perfecto, Barbie parte hacia el mundo humano para encontrar la verdadera felicidad.\r\n",
                Poster = "ken.jpg",
                Duracion = 120
            },
            new Pelicula
            {
                id = 2,
                Nombre = "Oppenheimer",
                Sinopsis = "El físico J Robert Oppenheimer trabaja con un equipo de científicos durante el Proyecto Manhattan, que condujo al desarrollo de la bomba atómica.\r\n",
                Poster = "oppenhaimer.jpeg",
                Duracion = 180
            },
            new Pelicula
            {
                id = 3,
                Nombre = "Sound of Freedom",
                Sinopsis = "Sonido De Libertad, basada en una increíble historia real, trae luz y esperanza al obscuro mundo del trafico de menores. Después de rescatar a un niño de los traficantes, un agente federal descubre que la hermana del niño todavía está cautiva y decide embarcarse en una peligrosa misión para salvarla. Con el tiempo en su contra, renuncia a su trabajo y se adentra en lo profundo de la selva colombiana, poniendo su vida en riesgo para liberarla y traerla de vuelta a casa.\r\n",
                Poster = "freedom.jpg",
                Duracion = 105
            }
            // Agrega más películas si es necesario...
        );
            //Ignoro todo esto




        }
    }
}
