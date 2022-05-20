﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PimApi.Data;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    [DbContext(typeof(PimDbContext))]
    [Migration("20220519151840_products and categories")]
    partial class productsandcategories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PIM")
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PimModels.Models.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DefaultCurrencyCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DefaultCurrencyCode");

                    b.ToTable("Catalogs", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.CatalogUser", b =>
                {
                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("CatalogId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CatalogUsers", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Currencies", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Ean")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.Catalog", b =>
                {
                    b.HasOne("PimModels.Models.Currency", "DefaultCurrency")
                        .WithMany()
                        .HasForeignKey("DefaultCurrencyCode");

                    b.Navigation("DefaultCurrency");
                });

            modelBuilder.Entity("PimModels.Models.CatalogUser", b =>
                {
                    b.HasOne("PimModels.Models.Catalog", "Catalog")
                        .WithMany("CatalogUsers")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PimModels.Models.User", "User")
                        .WithMany("CatalogUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PimModels.Models.Category", b =>
                {
                    b.HasOne("PimModels.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("PimModels.Models.Product", b =>
                {
                    b.HasOne("PimModels.Models.Catalog", "Catalog")
                        .WithMany("Products")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PimModels.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PimModels.Models.Catalog", b =>
                {
                    b.Navigation("CatalogUsers");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PimModels.Models.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("PimModels.Models.User", b =>
                {
                    b.Navigation("CatalogUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
