﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Migrations
{
    [DbContext(typeof(SuperTradersContext))]
    [Migration("20220116140432_migrate1")]
    partial class migrate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SuperTraders.DataService.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("LastUpDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Share");
                });
#pragma warning restore 612, 618
        }
    }
}