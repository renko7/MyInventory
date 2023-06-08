﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyInventory.Api;

#nullable disable

namespace MyInventory.Api.Migrations
{
    [DbContext(typeof(MyInventoryDbContext))]
    [Migration("20230608011158_GuidPublicId")]
    partial class GuidPublicId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ItemPicture", b =>
                {
                    b.Property<int>("ItemsId")
                        .HasColumnType("int");

                    b.Property<int>("PictureId")
                        .HasColumnType("int");

                    b.HasKey("ItemsId", "PictureId");

                    b.HasIndex("PictureId");

                    b.ToTable("ItemPicture");
                });

            modelBuilder.Entity("MyInventory.Api.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Items", (string)null);
                });

            modelBuilder.Entity("MyInventory.Api.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pictures", (string)null);
                });

            modelBuilder.Entity("ItemPicture", b =>
                {
                    b.HasOne("MyInventory.Api.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyInventory.Api.Models.Picture", null)
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyInventory.Api.Models.Item", b =>
                {
                    b.HasOne("MyInventory.Api.Models.Item", null)
                        .WithMany("Items")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("MyInventory.Api.Models.Item", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
