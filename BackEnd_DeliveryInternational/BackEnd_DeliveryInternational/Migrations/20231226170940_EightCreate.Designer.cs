﻿// <auto-generated />
using System;
using BackEnd_DeliveryInternational.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackEnd_DeliveryInternational.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231226170940_EightCreate")]
    partial class EightCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Cart", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("dishid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("orderid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("dishid");

                    b.HasIndex("orderid");

                    b.HasIndex("userid");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Dish", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Orderid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid?>("Userid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("Orderid");

                    b.HasIndex("Userid");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Order", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("Userid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Rating", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Dishid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Userid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Dishid");

                    b.HasIndex("Userid");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.StorageUsersTokens", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StorageUsersTokens");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Cart", b =>
                {
                    b.HasOne("BackEnd_DeliveryInternational.Models.Dish", "dish")
                        .WithMany()
                        .HasForeignKey("dishid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd_DeliveryInternational.Models.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderid");

                    b.HasOne("BackEnd_DeliveryInternational.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dish");

                    b.Navigation("order");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Dish", b =>
                {
                    b.HasOne("BackEnd_DeliveryInternational.Models.Order", null)
                        .WithMany("Dishes")
                        .HasForeignKey("Orderid");

                    b.HasOne("BackEnd_DeliveryInternational.Models.User", null)
                        .WithMany("Dishes")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Order", b =>
                {
                    b.HasOne("BackEnd_DeliveryInternational.Models.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Rating", b =>
                {
                    b.HasOne("BackEnd_DeliveryInternational.Models.Dish", null)
                        .WithMany("Ratings")
                        .HasForeignKey("Dishid");

                    b.HasOne("BackEnd_DeliveryInternational.Models.User", null)
                        .WithMany("Ratings")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Dish", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.Order", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("BackEnd_DeliveryInternational.Models.User", b =>
                {
                    b.Navigation("Dishes");

                    b.Navigation("Orders");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
