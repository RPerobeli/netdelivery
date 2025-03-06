using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Net.Delivery.Order.Domain.Config;
using Net.Delivery.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Infrastructure
{
    public  class NetDeliveryContext : DbContext
    {
        public DbSet<Customer> Usuarios { get; set; }
        public DbSet<Entities.Order> Pedidos { get; set; }
        public DbSet<Item> Items { get; set; }

        private readonly IConfiguration _configuration;

        public NetDeliveryContext() { }
        public NetDeliveryContext(DbContextOptions<NetDeliveryContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            //irá criar o banco e a estrutura de tabelas necessárias
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Customer>().ToTable("USUARIOS");
            modelBuilder.Entity<Customer>().Property(c=>c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().HasKey(c => c.Id) ;

            modelBuilder.Entity<Entities.Order>().ToTable("PEDIDOS");
            modelBuilder.Entity<Entities.Order>().Property(o => o.OrderId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Entities.Order>().HasKey(c => c.OrderId);

            modelBuilder.Entity<Item>().ToTable("ITEM");
            modelBuilder.Entity<Item>().Property(o => o.ItemId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Item>().HasKey(c=>c.ItemId);

            modelBuilder.Entity<Entities.Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)  // Define a FK
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<Entities.Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .OnDelete(DeleteBehavior.Cascade)
                ;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "server=localhost;port=3306;database=net_delivery;user=net_delivery;password=mauFJcuf5dhRMQrjj";
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
            
        }
    }
}
