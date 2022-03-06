﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherReports.DAL.Context;

#nullable disable

namespace WeatherReports.DAL.Migrations
{
    [DbContext(typeof(WeatherReportsDB))]
    [Migration("20220306193815_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeatherReports.Domain.Entities.Base.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AtmosphericPressure")
                        .HasColumnType("int");

                    b.Property<int>("Cloudiness")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("H")
                        .HasColumnType("int");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("Td")
                        .HasColumnType("float");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("VV")
                        .HasColumnType("int");

                    b.Property<string>("WeatherPhenomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weather");
                });
#pragma warning restore 612, 618
        }
    }
}