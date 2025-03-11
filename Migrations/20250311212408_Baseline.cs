using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonApi.Migrations
{
    /// <inheritdoc />
    public partial class Baseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterDatabase()
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "address",
            //    columns: table => new
            //    {
            //        address_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        address_line = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        zip_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        complement = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.address_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "family",
            //    columns: table => new
            //    {
            //        family_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.family_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "password",
            //    columns: table => new
            //    {
            //        password_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        password_hash = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        password_salt = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        attempt_count = table.Column<int>(type: "int(11)", nullable: false),
            //        reset_date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.password_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "role",
            //    columns: table => new
            //    {
            //        role_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.role_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "status",
            //    columns: table => new
            //    {
            //        status_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.status_id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "supplier",
            //    columns: table => new
            //    {
            //        supplier_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        contact = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        siret = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        address_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.supplier_id);
            //        table.ForeignKey(
            //            name: "Supplier_Address_FK",
            //            column: x => x.address_id,
            //            principalTable: "address",
            //            principalColumn: "address_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "customer",
            //    columns: table => new
            //    {
            //        customer_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        active = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //        validation_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        password_id = table.Column<int>(type: "int(11)", nullable: false),
            //        address_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.customer_id);
            //        table.ForeignKey(
            //            name: "Customer_Address_FK",
            //            column: x => x.address_id,
            //            principalTable: "address",
            //            principalColumn: "address_id");
            //        table.ForeignKey(
            //            name: "Customer_Password_FK",
            //            column: x => x.password_id,
            //            principalTable: "password",
            //            principalColumn: "password_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "employee",
            //    columns: table => new
            //    {
            //        employee_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        role_id = table.Column<int>(type: "int(11)", nullable: false),
            //        password_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.employee_id);
            //        table.ForeignKey(
            //            name: "Employee_Password_FK",
            //            column: x => x.password_id,
            //            principalTable: "password",
            //            principalColumn: "password_id");
            //        table.ForeignKey(
            //            name: "Employee_Role_FK",
            //            column: x => x.role_id,
            //            principalTable: "role",
            //            principalColumn: "role_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "product",
            //    columns: table => new
            //    {
            //        product_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        cuvee = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        year = table.Column<int>(type: "int(11)", nullable: false),
            //        producer_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        is_bio = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //        unit_price = table.Column<float>(type: "float", nullable: true),
            //        box_price = table.Column<float>(type: "float", nullable: true),
            //        quantity = table.Column<int>(type: "int(11)", nullable: false),
            //        auto_restock = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //        auto_restock_treshold = table.Column<int>(type: "int(11)", nullable: false),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        family_id = table.Column<int>(type: "int(11)", nullable: false),
            //        supplier_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.product_id);
            //        table.ForeignKey(
            //            name: "Product_Family_FK",
            //            column: x => x.family_id,
            //            principalTable: "family",
            //            principalColumn: "family_id");
            //        table.ForeignKey(
            //            name: "Product_Supplier_FK",
            //            column: x => x.supplier_id,
            //            principalTable: "supplier",
            //            principalColumn: "supplier_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "cart",
            //    columns: table => new
            //    {
            //        cart_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        customer_id = table.Column<int>(type: "int(11)", nullable: false),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.cart_id);
            //        table.ForeignKey(
            //            name: "Cart_Customer_FK",
            //            column: x => x.customer_id,
            //            principalTable: "customer",
            //            principalColumn: "customer_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "order",
            //    columns: table => new
            //    {
            //        order_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        status_id = table.Column<int>(type: "int(11)", nullable: false),
            //        customer_id = table.Column<int>(type: "int(11)", nullable: false),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.order_id);
            //        table.ForeignKey(
            //            name: "Order_Customer_FK",
            //            column: x => x.customer_id,
            //            principalTable: "customer",
            //            principalColumn: "customer_id");
            //        table.ForeignKey(
            //            name: "Order_Status_FK",
            //            column: x => x.status_id,
            //            principalTable: "status",
            //            principalColumn: "status_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "supplier_order",
            //    columns: table => new
            //    {
            //        order_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        employee_id = table.Column<int>(type: "int(11)", nullable: false),
            //        status_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.order_id);
            //        table.ForeignKey(
            //            name: "SupplierOrder_Employee_FK",
            //            column: x => x.employee_id,
            //            principalTable: "employee",
            //            principalColumn: "employee_id");
            //        table.ForeignKey(
            //            name: "SupplierOrder_Status_FK",
            //            column: x => x.status_id,
            //            principalTable: "status",
            //            principalColumn: "status_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "discount",
            //    columns: table => new
            //    {
            //        discount_id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        value = table.Column<int>(type: "int(11)", nullable: false),
            //        start_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()"),
            //        end_date = table.Column<DateTime>(type: "datetime", nullable: false),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        product_id = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.discount_id);
            //        table.ForeignKey(
            //            name: "Discount_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "image",
            //    columns: table => new
            //    {
            //        image_id = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        format_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        product_id = table.Column<int>(type: "int(11)", nullable: false),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.image_id);
            //        table.ForeignKey(
            //            name: "Image_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "review",
            //    columns: table => new
            //    {
            //        user_id = table.Column<int>(type: "int(11)", nullable: false),
            //        product_id = table.Column<int>(type: "int(11)", nullable: false),
            //        rating = table.Column<float>(type: "float", nullable: false),
            //        comment = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_review", x => new { x.product_id, x.user_id });
            //        table.ForeignKey(
            //            name: "Review_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //        table.ForeignKey(
            //            name: "Review_User_FK",
            //            column: x => x.user_id,
            //            principalTable: "customer",
            //            principalColumn: "customer_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "cart_line",
            //    columns: table => new
            //    {
            //        product_id = table.Column<int>(type: "int(11)", nullable: false),
            //        cart_id = table.Column<int>(type: "int(11)", nullable: false),
            //        quantity = table.Column<int>(type: "int(11)", nullable: false),
            //        is_set_aside = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "current_timestamp()")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_cart_line", x => new { x.product_id, x.cart_id });
            //        table.ForeignKey(
            //            name: "CartLine_Cart_FK",
            //            column: x => x.cart_id,
            //            principalTable: "cart",
            //            principalColumn: "cart_id");
            //        table.ForeignKey(
            //            name: "CartLine_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "order_line",
            //    columns: table => new
            //    {
            //        order_id = table.Column<int>(type: "int(11)", nullable: false),
            //        product_id = table.Column<int>(type: "int(11)", nullable: false),
            //        quantity = table.Column<int>(type: "int(11)", nullable: false),
            //        unit_price = table.Column<float>(type: "float", nullable: false),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_order_line", x => new { x.product_id, x.order_id });
            //        table.ForeignKey(
            //            name: "OrderLine_Order_FK",
            //            column: x => x.order_id,
            //            principalTable: "order",
            //            principalColumn: "order_id");
            //        table.ForeignKey(
            //            name: "OrderLine_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "supplier_order_line",
            //    columns: table => new
            //    {
            //        order_id = table.Column<int>(type: "int(11)", nullable: false),
            //        product_id = table.Column<int>(type: "int(11)", nullable: false),
            //        quantity = table.Column<int>(type: "int(11)", nullable: false),
            //        unit_price = table.Column<float>(type: "float", nullable: false),
            //        update_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            //        creation_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()"),
            //        deletion_time = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_supplier_order_line", x => new { x.product_id, x.order_id });
            //        table.ForeignKey(
            //            name: "SupplierOrderLine_Order_FK",
            //            column: x => x.order_id,
            //            principalTable: "supplier_order",
            //            principalColumn: "order_id");
            //        table.ForeignKey(
            //            name: "SupplierOrderLine_Product_FK",
            //            column: x => x.product_id,
            //            principalTable: "product",
            //            principalColumn: "product_id");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .Annotation("Relational:Collation", "utf8mb4_general_ci");

            //migrationBuilder.CreateIndex(
            //    name: "Cart_Customer_FK",
            //    table: "cart",
            //    column: "customer_id");

            //migrationBuilder.CreateIndex(
            //    name: "CartLine_Cart_FK",
            //    table: "cart_line",
            //    column: "cart_id");

            //migrationBuilder.CreateIndex(
            //    name: "CartLine_Product_FK",
            //    table: "cart_line",
            //    column: "product_id");

            //migrationBuilder.CreateIndex(
            //    name: "Customer_Address_FK",
            //    table: "customer",
            //    column: "address_id");

            //migrationBuilder.CreateIndex(
            //    name: "Customer_Password_FK",
            //    table: "customer",
            //    column: "password_id");

            //migrationBuilder.CreateIndex(
            //    name: "Discount_Product_FK",
            //    table: "discount",
            //    column: "product_id");

            //migrationBuilder.CreateIndex(
            //    name: "Employee_Password_FK",
            //    table: "employee",
            //    column: "password_id");

            //migrationBuilder.CreateIndex(
            //    name: "Employee_Role_FK",
            //    table: "employee",
            //    column: "role_id");

            //migrationBuilder.CreateIndex(
            //    name: "Image_Product_FK",
            //    table: "image",
            //    column: "product_id");

            //migrationBuilder.CreateIndex(
            //    name: "Order_Customer_FK",
            //    table: "order",
            //    column: "customer_id");

            //migrationBuilder.CreateIndex(
            //    name: "Order_Status_FK",
            //    table: "order",
            //    column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "OrderLine_Order_FK",
            //    table: "order_line",
            //    column: "order_id");

            //migrationBuilder.CreateIndex(
            //    name: "OrderLine_Product_FK",
            //    table: "order_line",
            //    column: "product_id");

            //migrationBuilder.CreateIndex(
            //    name: "Product_Family_FK",
            //    table: "product",
            //    column: "family_id");

            //migrationBuilder.CreateIndex(
            //    name: "Product_Supplier_FK",
            //    table: "product",
            //    column: "supplier_id");

            //migrationBuilder.CreateIndex(
            //    name: "Review_Product_FK",
            //    table: "review",
            //    column: "product_id");

            //migrationBuilder.CreateIndex(
            //    name: "Review_User_FK",
            //    table: "review",
            //    column: "user_id");

            //migrationBuilder.CreateIndex(
            //    name: "Supplier_Address_FK",
            //    table: "supplier",
            //    column: "address_id");

            //migrationBuilder.CreateIndex(
            //    name: "SupplierOrder_Employee_FK",
            //    table: "supplier_order",
            //    column: "employee_id");

            //migrationBuilder.CreateIndex(
            //    name: "SupplierOrder_Status_FK",
            //    table: "supplier_order",
            //    column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "SupplierOrderLine_Order_FK",
            //    table: "supplier_order_line",
            //    column: "order_id");

            //migrationBuilder.CreateIndex(
            //    name: "SupplierOrderLine_Product_FK",
            //    table: "supplier_order_line",
            //    column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "cart_line");

            //migrationBuilder.DropTable(
            //    name: "discount");

            //migrationBuilder.DropTable(
            //    name: "image");

            //migrationBuilder.DropTable(
            //    name: "order_line");

            //migrationBuilder.DropTable(
            //    name: "review");

            //migrationBuilder.DropTable(
            //    name: "supplier_order_line");

            //migrationBuilder.DropTable(
            //    name: "cart");

            //migrationBuilder.DropTable(
            //    name: "order");

            //migrationBuilder.DropTable(
            //    name: "supplier_order");

            //migrationBuilder.DropTable(
            //    name: "product");

            //migrationBuilder.DropTable(
            //    name: "customer");

            //migrationBuilder.DropTable(
            //    name: "employee");

            //migrationBuilder.DropTable(
            //    name: "status");

            //migrationBuilder.DropTable(
            //    name: "family");

            //migrationBuilder.DropTable(
            //    name: "supplier");

            //migrationBuilder.DropTable(
            //    name: "password");

            //migrationBuilder.DropTable(
            //    name: "role");

            //migrationBuilder.DropTable(
            //    name: "address");
        }
    }
}
