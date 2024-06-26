﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Context;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ContextoAplicacionDB))]
    [Migration("20240621013940_MigracionInicial")]
    partial class MigracionInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Repository.Data.ClienteModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("celular")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("documento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("id_banco")
                        .HasColumnType("integer");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("cliente", (string)null);
                });

            modelBuilder.Entity("Repository.Data.FacturaModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("fecha_hora")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("id_cliente")
                        .HasColumnType("integer");

                    b.Property<string>("nro_factura")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("sucursal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("total")
                        .HasColumnType("numeric");

                    b.Property<decimal>("total_iva")
                        .HasColumnType("numeric");

                    b.Property<decimal>("total_iva10")
                        .HasColumnType("numeric");

                    b.Property<decimal>("total_iva5")
                        .HasColumnType("numeric");

                    b.Property<string>("total_letras")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("factura", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
