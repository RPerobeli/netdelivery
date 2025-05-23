﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Net.Delivery.Order.Domain.Infrastructure;

#nullable disable

namespace Net.Delivery.Order.Domain.Migrations
{
    [DbContext(typeof(NetDeliveryContext))]
    [Migration("20250320224558_Pedidos_itensMigration")]
    partial class Pedidos_itensMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("NAME");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("TIPO_USUARIO");

                    b.HasKey("Id");

                    b.ToTable("USUARIOS", (string)null);
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Item", b =>
                {
                    b.Property<long>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ItemId"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UnitValue")
                        .HasColumnType("longtext");

                    b.HasKey("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("ITEM", (string)null);
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OrderCreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("OrderLastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderSituation")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PEDIDOS", (string)null);
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.OrderItem", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("PEDIDO_ITENS", (string)null);
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Item", b =>
                {
                    b.HasOne("Net.Delivery.Order.Domain.Entities.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Order", b =>
                {
                    b.HasOne("Net.Delivery.Order.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Net.Delivery.Order.Domain.Entities.Item", "Item")
                        .WithMany("OrderItens")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Net.Delivery.Order.Domain.Entities.Order", "Order")
                        .WithMany("OrderItens")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Item", b =>
                {
                    b.Navigation("OrderItens");
                });

            modelBuilder.Entity("Net.Delivery.Order.Domain.Entities.Order", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("OrderItens");
                });
#pragma warning restore 612, 618
        }
    }
}
