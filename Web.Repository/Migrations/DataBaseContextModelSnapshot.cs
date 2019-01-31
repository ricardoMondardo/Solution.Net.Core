﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Repository.Context;

namespace Web.Repository.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Web.Repository.Models.Product.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProductDetailId");

                    b.Property<int?>("ProductGradeId");

                    b.Property<string>("ProductGradeId1");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.HasIndex("ProductGradeId1");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Web.Repository.Models.Product.ProductDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComeFrom");

                    b.Property<string>("MadeFor");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.ToTable("ProductDetail");
                });

            modelBuilder.Entity("Web.Repository.Models.Product.ProductGrade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("ProductGrade");
                });

            modelBuilder.Entity("Web.Repository.Models.Product.ProdutItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProdutItem");
                });

            modelBuilder.Entity("Web.Repository.Models.Todo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("Web.Repository.Models.User.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Web.Repository.Models.Product.Product", b =>
                {
                    b.HasOne("Web.Repository.Models.Product.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailId");

                    b.HasOne("Web.Repository.Models.Product.ProductGrade", "ProductGrade")
                        .WithMany()
                        .HasForeignKey("ProductGradeId1");
                });

            modelBuilder.Entity("Web.Repository.Models.Product.ProdutItem", b =>
                {
                    b.HasOne("Web.Repository.Models.Product.Product", "Product")
                        .WithMany("ProdutItens")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}