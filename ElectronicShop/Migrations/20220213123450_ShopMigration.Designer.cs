﻿// <auto-generated />
using System;
using ElectronicShop.ShopModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ElectronicShop.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20220213123450_ShopMigration")]
    partial class ShopMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.electronicproductmodel", b =>
                {
                    b.Property<int>("electronicproductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("electronicproductID"));

                    b.Property<int>("battery")
                        .HasColumnType("int4");

                    b.Property<int>("memory")
                        .HasColumnType("int4");

                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("numberofcores")
                        .HasColumnType("int4");

                    b.Property<int>("numberofproducts")
                        .HasColumnType("int4");

                    b.Property<int>("price")
                        .HasColumnType("int4");

                    b.Property<string>("processor")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("screensize")
                        .HasColumnType("int4");

                    b.Property<int>("storage")
                        .HasColumnType("int4");

                    b.Property<string>("type")
                        .HasColumnType("varchar(255)");

                    b.HasKey("electronicproductID");

                    b.ToTable("electronicproducts");

                    b.HasData(
                        new
                        {
                            electronicproductID = 1,
                            battery = 20,
                            memory = 8,
                            name = "MacbookPro",
                            numberofcores = 8,
                            numberofproducts = 1,
                            price = 1500,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 2,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 8,
                            numberofproducts = 1,
                            price = 1500,
                            processor = "Apple M1",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 3,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 8,
                            numberofproducts = 2,
                            price = 750,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 4,
                            battery = 20,
                            memory = 8,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 2,
                            price = 1500,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 5,
                            battery = 20,
                            memory = 8,
                            name = "MacbookPro",
                            numberofcores = 8,
                            numberofproducts = 1,
                            price = 750,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 6,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 1,
                            price = 1500,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 7,
                            battery = 20,
                            memory = 8,
                            name = "MacbookPro",
                            numberofcores = 8,
                            numberofproducts = 2,
                            price = 1500,
                            processor = "Apple M1",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 8,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 1,
                            price = 750,
                            processor = "Apple M1",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 9,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 2,
                            price = 1500,
                            processor = "Apple M1",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 10,
                            battery = 20,
                            memory = 8,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 1,
                            price = 750,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 11,
                            battery = 20,
                            memory = 32,
                            name = "MacbookPro",
                            numberofcores = 6,
                            numberofproducts = 1,
                            price = 750,
                            processor = "i7",
                            screensize = 14,
                            storage = 256,
                            type = "Laptop"
                        },
                        new
                        {
                            electronicproductID = 12,
                            battery = 15,
                            memory = 4,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 3,
                            price = 800,
                            processor = "A13 bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 13,
                            battery = 15,
                            memory = 4,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 2,
                            price = 1200,
                            processor = "A15 Bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 14,
                            battery = 15,
                            memory = 8,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 3,
                            price = 800,
                            processor = "A15 Bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 15,
                            battery = 15,
                            memory = 8,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 1,
                            price = 1200,
                            processor = "A15 Bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 16,
                            battery = 15,
                            memory = 8,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 2,
                            price = 1200,
                            processor = "A13 bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 17,
                            battery = 15,
                            memory = 4,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 1,
                            price = 800,
                            processor = "A15 Bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        },
                        new
                        {
                            electronicproductID = 18,
                            battery = 15,
                            memory = 4,
                            name = "Iphone12",
                            numberofcores = 4,
                            numberofproducts = 3,
                            price = 1200,
                            processor = "A13 bionic",
                            screensize = 4,
                            storage = 32,
                            type = "Phone"
                        });
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.ElectronicShopExceptionTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Application")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Callsite")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Exception")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Level")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Logged")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Logger")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Message")
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("AllExceptionsTable");
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.orderdetailsmodel", b =>
                {
                    b.Property<int>("orderdetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("orderdetailID"));

                    b.Property<DateTime>("boughtdate")
                        .HasColumnType("Date");

                    b.Property<int>("electronicproductID")
                        .HasColumnType("integer");

                    b.Property<int>("orderID")
                        .HasColumnType("integer");

                    b.Property<double>("price")
                        .HasColumnType("float8");

                    b.Property<int>("quantity")
                        .HasColumnType("int4");

                    b.HasKey("orderdetailID");

                    b.HasIndex("electronicproductID");

                    b.HasIndex("orderID");

                    b.ToTable("ordersdetails");

                    b.HasData(
                        new
                        {
                            orderdetailID = 1,
                            boughtdate = new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2468),
                            electronicproductID = 1,
                            orderID = 3,
                            price = 700.0,
                            quantity = 2
                        },
                        new
                        {
                            orderdetailID = 2,
                            boughtdate = new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2528),
                            electronicproductID = 2,
                            orderID = 3,
                            price = 1166.0,
                            quantity = 2
                        },
                        new
                        {
                            orderdetailID = 3,
                            boughtdate = new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2546),
                            electronicproductID = 1,
                            orderID = 1,
                            price = 833.0,
                            quantity = 1
                        });
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.ordersmodel", b =>
                {
                    b.Property<int>("orderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("orderID"));

                    b.Property<int>("totalnumberofobjectsbought")
                        .HasColumnType("int4");

                    b.Property<double>("totalprice")
                        .HasColumnType("float8");

                    b.Property<int>("userID")
                        .HasColumnType("integer");

                    b.HasKey("orderID");

                    b.HasIndex("userID");

                    b.ToTable("orders");

                    b.HasData(
                        new
                        {
                            orderID = 1,
                            totalnumberofobjectsbought = 2,
                            totalprice = 2100.0,
                            userID = 1
                        },
                        new
                        {
                            orderID = 2,
                            totalnumberofobjectsbought = 6,
                            totalprice = 3500.0,
                            userID = 2
                        },
                        new
                        {
                            orderID = 3,
                            totalnumberofobjectsbought = 1,
                            totalprice = 2100.0,
                            userID = 1
                        });
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.usermodel", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userID"));

                    b.Property<int>("age")
                        .HasColumnType("int4");

                    b.Property<double>("balance")
                        .HasColumnType("float8");

                    b.Property<string>("firstname")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("lastname")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("password")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("phonenumber")
                        .HasColumnType("int4");

                    b.HasKey("userID");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            userID = 1,
                            age = 44,
                            balance = 9916.0,
                            firstname = "Joe",
                            lastname = "Joe",
                            password = "UqXM",
                            phonenumber = 79795894
                        },
                        new
                        {
                            userID = 2,
                            age = 20,
                            balance = 18030.0,
                            firstname = "John",
                            lastname = "John",
                            password = "gRS",
                            phonenumber = 74795087
                        },
                        new
                        {
                            userID = 3,
                            age = 19,
                            balance = 18862.0,
                            firstname = "Maria",
                            lastname = "Maria",
                            password = "OuUiJhCzo",
                            phonenumber = 71866662
                        });
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.orderdetailsmodel", b =>
                {
                    b.HasOne("ElectronicShop.ShopModels.Models.electronicproductmodel", "electronicproduct")
                        .WithMany()
                        .HasForeignKey("electronicproductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicShop.ShopModels.Models.ordersmodel", "ordersmodel")
                        .WithMany()
                        .HasForeignKey("orderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("electronicproduct");

                    b.Navigation("ordersmodel");
                });

            modelBuilder.Entity("ElectronicShop.ShopModels.Models.ordersmodel", b =>
                {
                    b.HasOne("ElectronicShop.ShopModels.Models.usermodel", "usermodel")
                        .WithMany()
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usermodel");
                });
#pragma warning restore 612, 618
        }
    }
}
