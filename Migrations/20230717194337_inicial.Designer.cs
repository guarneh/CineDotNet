﻿// <auto-generated />
using System;
using CIneDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CIneDotNet.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230717194337_inicial")]
    partial class inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CIneDotNet.Models.Funcion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("cantClientes")
                        .HasColumnType("int");

                    b.Property<double>("costo")
                        .HasColumnType("float");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("idPelicula")
                        .HasColumnType("int");

                    b.Property<int>("idSala")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("idPelicula");

                    b.HasIndex("idSala");

                    b.ToTable("Funciones", (string)null);
                });

            modelBuilder.Entity("CIneDotNet.Models.FuncionUsuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<int>("idFuncion")
                        .HasColumnType("int");

                    b.Property<int>("cantEntradas")
                        .HasColumnType("int");

                    b.HasKey("idUsuario", "idFuncion");

                    b.HasIndex("idFuncion");

                    b.ToTable("funcionUsuarios");
                });

            modelBuilder.Entity("CIneDotNet.Models.Pelicula", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sinopsis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Peliculas", (string)null);
                });

            modelBuilder.Entity("CIneDotNet.Models.Sala", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("capacidad")
                        .HasColumnType("int");

                    b.Property<string>("ubicacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Salas", (string)null);
                });

            modelBuilder.Entity("CIneDotNet.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Apellido")
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<double>("Credito")
                        .HasColumnType("float");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<bool>("EsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<int>("IntentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("CIneDotNet.Models.Funcion", b =>
                {
                    b.HasOne("CIneDotNet.Models.Pelicula", "miPelicula")
                        .WithMany("misFunciones")
                        .HasForeignKey("idPelicula")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CIneDotNet.Models.Sala", "miSala")
                        .WithMany("misFunciones")
                        .HasForeignKey("idSala")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("miPelicula");

                    b.Navigation("miSala");
                });

            modelBuilder.Entity("CIneDotNet.Models.FuncionUsuario", b =>
                {
                    b.HasOne("CIneDotNet.Models.Funcion", "funcion")
                        .WithMany("funcionUsuarios")
                        .HasForeignKey("idFuncion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CIneDotNet.Models.Usuario", "usuario")
                        .WithMany("Tickets")
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("funcion");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("CIneDotNet.Models.Funcion", b =>
                {
                    b.Navigation("funcionUsuarios");
                });

            modelBuilder.Entity("CIneDotNet.Models.Pelicula", b =>
                {
                    b.Navigation("misFunciones");
                });

            modelBuilder.Entity("CIneDotNet.Models.Sala", b =>
                {
                    b.Navigation("misFunciones");
                });

            modelBuilder.Entity("CIneDotNet.Models.Usuario", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
