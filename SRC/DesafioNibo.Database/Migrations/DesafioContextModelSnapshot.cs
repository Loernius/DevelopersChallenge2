﻿// <auto-generated />
using System;
using Desafio.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Desafio.Database.Migrations
{
    [DbContext(typeof(DesafioContext))]
    partial class DesafioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Desafio.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("active")
                        .HasColumnType("INTEGER");

                    b.Property<float>("amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<string>("memo")
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Extrato");
                });
#pragma warning restore 612, 618
        }
    }
}