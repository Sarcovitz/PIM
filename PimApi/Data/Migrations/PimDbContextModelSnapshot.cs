﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PimApi.Data;

#nullable disable

namespace PIM.Api.Data.Migrations
{
    [DbContext(typeof(PimDbContext))]
    partial class PimDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PIM")
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryProductAttributeProto", b =>
                {
                    b.Property<int>("AttributeProtosId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.HasKey("AttributeProtosId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("CategoryProductAttributeProto", "PIM");
                });

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

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

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

            modelBuilder.Entity("PimModels.Models.ProductAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeProtoId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttributeProtoId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttributes", "PIM");
                });

            modelBuilder.Entity("PimModels.Models.ProductAttributeProto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeType")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PossibleValues")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("ProductAttributeProtos", "PIM");
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

            modelBuilder.Entity("CategoryProductAttributeProto", b =>
                {
                    b.HasOne("PimModels.Models.ProductAttributeProto", null)
                        .WithMany()
                        .HasForeignKey("AttributeProtosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PimModels.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("PimModels.Models.Catalog", "Catalog")
                        .WithMany("Categories")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PimModels.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Catalog");

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

            modelBuilder.Entity("PimModels.Models.ProductAttribute", b =>
                {
                    b.HasOne("PimModels.Models.ProductAttributeProto", "AttributeProto")
                        .WithMany("ProductAttributes")
                        .HasForeignKey("AttributeProtoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PimModels.Models.Product", "Product")
                        .WithMany("ProductAttributes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttributeProto");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PimModels.Models.ProductAttributeProto", b =>
                {
                    b.HasOne("PimModels.Models.Catalog", "Catalog")
                        .WithMany("ProductAttributeProtos")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("PimModels.Models.Catalog", b =>
                {
                    b.Navigation("CatalogUsers");

                    b.Navigation("Categories");

                    b.Navigation("ProductAttributeProtos");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PimModels.Models.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("PimModels.Models.Product", b =>
                {
                    b.Navigation("ProductAttributes");
                });

            modelBuilder.Entity("PimModels.Models.ProductAttributeProto", b =>
                {
                    b.Navigation("ProductAttributes");
                });

            modelBuilder.Entity("PimModels.Models.User", b =>
                {
                    b.Navigation("CatalogUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
