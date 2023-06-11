﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicesStore.Api.Basket.Persistence;

namespace ServicesStore.Api.Basket.Migrations
{
    [DbContext(typeof(BasketContext))]
    [Migration("20230610181455_InitialMigrationMySql")]
    partial class InitialMigrationMySql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ServicesStore.Api.Basket.Model.BasketSession", b =>
                {
                    b.Property<int>("BasketSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.HasKey("BasketSessionId");

                    b.ToTable("BasketSession");
                });

            modelBuilder.Entity("ServicesStore.Api.Basket.Model.BasketSessionDetail", b =>
                {
                    b.Property<int>("BasketSessionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BasketSessionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Product")
                        .HasColumnType("text");

                    b.HasKey("BasketSessionDetailId");

                    b.HasIndex("BasketSessionId");

                    b.ToTable("BasketSessionDetail");
                });

            modelBuilder.Entity("ServicesStore.Api.Basket.Model.BasketSessionDetail", b =>
                {
                    b.HasOne("ServicesStore.Api.Basket.Model.BasketSession", "BasketSession")
                        .WithMany("BasketSessionDetails")
                        .HasForeignKey("BasketSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}