using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.Models;
using MonApi.API.CartLines.Models;
using MonApi.API.Carts.Models;
using MonApi.API.Customers.Models;
using MonApi.API.Discounts.Models;
using MonApi.API.Employees.Models;
using MonApi.API.Families.Models;
using MonApi.API.Images.Models;
using MonApi.API.OrderLines.Models;
using MonApi.API.Orders.Models;
using MonApi.API.Passwords.Models;
using MonApi.API.Products.Models;
using MonApi.API.Reviews.Models;
using MonApi.API.Roles.Models;
using MonApi.API.Statuses.Models;
using MonApi.API.SupplierOrderLines.Models;
using MonApi.API.SupplierOrders.Models;
using MonApi.API.Suppliers.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MonApi.Shared.Data;

public partial class StockManagementContext : DbContext
{
    public StockManagementContext()
    {
    }

    public StockManagementContext(DbContextOptions<StockManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartLine> CartLines { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<Password> Passwords { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierOrder> SupplierOrders { get; set; }

    public virtual DbSet<SupplierOrderLine> SupplierOrderLines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                                  ?? throw new InvalidOperationException(
                                      "Connection string 'DATABASE_CONNECTION_STRING' not found.");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .HasColumnName("address_line");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Complement)
                .HasMaxLength(255)
                .HasColumnName("complement");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("country");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PRIMARY");

            entity.ToTable("cart");

            entity.HasIndex(e => e.CustomerId, "Cart_Customer_FK");

            entity.Property(e => e.CartId)
                .HasColumnType("int(11)")
                .HasColumnName("cart_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cart_Customer_FK");
        });

        modelBuilder.Entity<CartLine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cart_line");

            entity.HasIndex(e => e.CartId, "CartLine_Cart_FK");

            entity.HasIndex(e => e.ProductId, "CartLine_Product_FK");

            entity.Property(e => e.CartId)
                .HasColumnType("int(11)")
                .HasColumnName("cart_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.IsSetAside).HasColumnName("is_set_aside");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Cart).WithMany()
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartLine_Cart_FK");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartLine_Product_FK");
        });
        modelBuilder.Entity<CartLine>().HasKey(line => new { line.ProductId, line.CartId });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AddressId, "Customer_Address_FK");

            entity.HasIndex(e => e.PasswordId, "Customer_Password_FK");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordId)
                .HasColumnType("int(11)")
                .HasColumnName("password_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.ValidationId)
                .HasMaxLength(255)
                .HasColumnName("validation_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer_Address_FK");

            entity.HasOne(d => d.Password).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PasswordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer_Password_FK");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PRIMARY");

            entity.ToTable("discount");

            entity.HasIndex(e => e.ProductId, "Discount_Product_FK");

            entity.Property(e => e.DiscountId)
                .HasColumnType("int(11)")
                .HasColumnName("discount_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.Value)
                .HasColumnType("int(11)")
                .HasColumnName("value");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Discount_Product_FK");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.PasswordId, "Employee_Password_FK");

            entity.HasIndex(e => e.RoleId, "Employee_Role_FK");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employee_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordId)
                .HasColumnType("int(11)")
                .HasColumnName("password_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Password).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PasswordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_Password_FK");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_Role_FK");
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(e => e.FamilyId).HasName("PRIMARY");

            entity.ToTable("family");

            entity.Property(e => e.FamilyId)
                .HasColumnType("int(11)")
                .HasColumnName("family_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PRIMARY");

            entity.ToTable("image");

            entity.HasIndex(e => e.ProductId, "Image_Product_FK");

            entity.Property(e => e.ImageId)
                .HasMaxLength(150)
                .HasColumnName("image_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.FormatType)
                .HasMaxLength(10)
                .HasColumnName("format_type");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Image_Product_FK");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.CustomerId, "Order_Customer_FK");

            entity.HasIndex(e => e.StatusId, "Order_Status_FK");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_Customer_FK");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_Status_FK");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_line");

            entity.HasIndex(e => e.OrderId, "OrderLine_Order_FK");

            entity.HasIndex(e => e.ProductId, "OrderLine_Product_FK");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderLine_Order_FK");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderLine_Product_FK");
        });
        modelBuilder.Entity<OrderLine>().HasKey(orderLine => new { orderLine.ProductId, orderLine.OrderId });

        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.PasswordId).HasName("PRIMARY");

            entity.ToTable("password");

            entity.Property(e => e.PasswordId)
                .HasColumnType("int(11)")
                .HasColumnName("password_id");
            entity.Property(e => e.AttemptCount)
                .HasColumnType("int(11)")
                .HasColumnName("attempt_count");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("text")
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .HasColumnName("password_salt");
            entity.Property(e => e.ResetDate)
                .HasColumnType("datetime")
                .HasColumnName("reset_date");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.ResetToken)
                .HasMaxLength(255)
                .HasColumnName("reset_token");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.FamilyId, "Product_Family_FK");

            entity.HasIndex(e => e.SupplierId, "Product_Supplier_FK");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.AutoRestock).HasColumnName("auto_restock");
            entity.Property(e => e.AutoRestockTreshold)
                .HasColumnType("int(11)")
                .HasColumnName("auto_restock_treshold");
            entity.Property(e => e.BoxPrice).HasColumnName("box_price");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.Cuvee)
                .HasMaxLength(255)
                .HasColumnName("cuvee");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.FamilyId)
                .HasColumnType("int(11)")
                .HasColumnName("family_id");
            entity.Property(e => e.IsBio).HasColumnName("is_bio");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ProducerName)
                .HasMaxLength(255)
                .HasColumnName("producer_name");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("supplier_id");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.Year)
                .HasColumnType("int(11)")
                .HasColumnName("year");
            entity.Property(e => e.Description)
                .HasColumnName("description");


            entity.HasOne(d => d.Family).WithMany(p => p.Products)
                .HasForeignKey(d => d.FamilyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Family_FK");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Supplier_FK");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("review");

            entity.HasIndex(e => e.ProductId, "Review_Product_FK");

            entity.HasIndex(e => e.UserId, "Review_User_FK");

            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_Product_FK");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_User_FK");
        });
        modelBuilder.Entity<Review>().HasKey(r => new { r.ProductId, r.UserId });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.HasIndex(e => e.AddressId, "Supplier_Address_FK");

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("supplier_id");
            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Siret)
                .HasMaxLength(255)
                .HasColumnName("siret");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Address).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Supplier_Address_FK");
        });

        modelBuilder.Entity<SupplierOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("supplier_order");

            entity.HasIndex(e => e.EmployeeId, "SupplierOrder_Employee_FK");

            entity.HasIndex(e => e.StatusId, "SupplierOrder_Status_FK");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employee_id");
            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Employee).WithMany(p => p.SupplierOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SupplierOrder_Employee_FK");

            entity.HasOne(d => d.Status).WithMany(p => p.SupplierOrders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SupplierOrder_Status_FK");
        });

        modelBuilder.Entity<SupplierOrderLine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("supplier_order_line");

            entity.HasIndex(e => e.OrderId, "SupplierOrderLine_Order_FK");

            entity.HasIndex(e => e.ProductId, "SupplierOrderLine_Product_FK");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.DeletionTime)
                .HasColumnType("datetime")
                .HasColumnName("deletion_time");
            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UpdateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("update_time");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SupplierOrderLine_Order_FK");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SupplierOrderLine_Product_FK");
        });
        modelBuilder.Entity<SupplierOrderLine>().HasKey(orderLine => new { orderLine.ProductId, orderLine.OrderId });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}