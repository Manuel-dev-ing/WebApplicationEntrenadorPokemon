﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationEntrenadorPokemon;

#nullable disable

namespace WebApplicationEntrenadorPokemon.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Entrenador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EntrenadorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Pokemonid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("Pokemonid");

                    b.ToTable("Entrenador");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntrenadorId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntrenadorId");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntrenadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sprite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Pokemon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("EntrenadorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("altura")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("officialArtwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("peso")
                        .HasColumnType("int");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.TipoPokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("tipoPokemon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposPokemon");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Entrenador", b =>
                {
                    b.HasOne("WebApplicationEntrenadorPokemon.Entidades.Item", null)
                        .WithMany("Entrenador")
                        .HasForeignKey("ItemId");

                    b.HasOne("WebApplicationEntrenadorPokemon.Entidades.Pokemon", "Pokemon")
                        .WithMany("Entrenador")
                        .HasForeignKey("Pokemonid");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Genero", b =>
                {
                    b.HasOne("WebApplicationEntrenadorPokemon.Entidades.Entrenador", "Entrenador")
                        .WithMany("Genero")
                        .HasForeignKey("EntrenadorId");

                    b.Navigation("Entrenador");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Entrenador", b =>
                {
                    b.Navigation("Genero");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Item", b =>
                {
                    b.Navigation("Entrenador");
                });

            modelBuilder.Entity("WebApplicationEntrenadorPokemon.Entidades.Pokemon", b =>
                {
                    b.Navigation("Entrenador");
                });
#pragma warning restore 612, 618
        }
    }
}
