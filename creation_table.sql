-- Active: 1732875427459@@127.0.0.1@3306@stock_management
#------------------------------------------------------------
#        MySQL Script.
#------------------------------------------------------------
DROP DATABASE IF EXISTS stock_management;
CREATE DATABASE IF NOT EXISTS stock_management;
USE stock_management;
#------------------------------------------------------------
# Table: Status
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS status(
    status_id INT AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Status_PK PRIMARY KEY (status_id)
);
#------------------------------------------------------------
# Table: Family
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS family(
    family_id INT AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Family_PK PRIMARY KEY (family_id)
);
#------------------------------------------------------------
# Table: Role
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `role`(
    role_id INT AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Role_PK PRIMARY KEY (role_id)
);
#------------------------------------------------------------
# Table: Password
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `password`(
    password_id INT AUTO_INCREMENT NOT NULL,
    password_hash TEXT NOT NULL,
    password_salt VARCHAR(255) NOT NULL,
    attempt_count INT NOT NULL DEFAULT 0,
    reset_date DATETIME DEFAULT NULL,
    deletion_time DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Password_PK PRIMARY KEY (password_id)
);
#------------------------------------------------------------
# Table: Address
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `address`(
    address_id INT AUTO_INCREMENT NOT NULL,
    address_line VARCHAR(255) NOT NULL,
    city VARCHAR(255) NOT NULL,
    zip_code VARCHAR(10) NOT NULL,
    country VARCHAR(255) NOT NULL,
    complement VARCHAR(255) DEFAULT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Address_PK PRIMARY KEY (address_id)
);
#------------------------------------------------------------
# Table: Customer
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS customer(
    customer_id INT AUTO_INCREMENT NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    first_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    active BOOLEAN NOT NULL DEFAULT 0,
    validation_id VARCHAR(255) NOT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    password_id INT NOT NULL,
    address_id INT NOT NULL,
    CONSTRAINT Customer_PK PRIMARY KEY (customer_id),
    CONSTRAINT Customer_Password_FK FOREIGN KEY (password_id) REFERENCES password(password_id),
    CONSTRAINT Customer_Address_FK FOREIGN KEY (address_id) REFERENCES address(address_id)
);
#------------------------------------------------------------
# Table: Order
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `order`(
    order_id INT AUTO_INCREMENT NOT NULL,
    delivery_date DATETIME DEFAULT NULL,
    status_id INT NOT NULL,
    customer_id INT NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Order_PK PRIMARY KEY (order_id),
    CONSTRAINT Order_Status_FK FOREIGN KEY (status_id) REFERENCES status(status_id),
    CONSTRAINT Order_Customer_FK FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);
#------------------------------------------------------------
# Table: Supplier
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS supplier(
    supplier_id INT AUTO_INCREMENT NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    first_name VARCHAR(255) NOT NULL,
    contact VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    siret VARCHAR(255) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    address_id INT NOT NULL,
    CONSTRAINT Supplier_PK PRIMARY KEY (supplier_id),
    CONSTRAINT Supplier_Address_FK FOREIGN KEY (address_id) REFERENCES address(address_id)
);
#------------------------------------------------------------
# Table: Product
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS product(
    product_id INT AUTO_INCREMENT NOT NULL,
    name VARCHAR(255) NOT NULL,
    cuvee VARCHAR(255) NOT NULL,
    year INT NOT NULL,
    producer_name VARCHAR(255) NOT NULL,
    is_bio BOOLEAN NOT NULL DEFAULT 0,
    unit_price FLOAT,
    box_price FLOAT,
    quantity INT NOT NULL,
    auto_restock BOOLEAN NOT NULL DEFAULT 0,
    auto_restock_treshold INT NOT NULL DEFAULT 0,
    deletion_time DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    family_id INT NOT NULL,
    supplier_id INT NOT NULL,
    CONSTRAINT Product_PK PRIMARY KEY (product_id),
    CONSTRAINT Product_Family_FK FOREIGN KEY (family_id) REFERENCES family(family_id),
    CONSTRAINT Product_Supplier_FK FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id)
);
#------------------------------------------------------------
# Table: Discount
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS discount(
    discount_id INT AUTO_INCREMENT NOT NULL,
    name VARCHAR(50) NOT NULL,
    value INT NOT NULL,
    start_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    end_date DATETIME NOT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    product_id INT NOT NULL,
    CONSTRAINT Discount_PK PRIMARY KEY (discount_id),
    CONSTRAINT Discount_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id)
);
#------------------------------------------------------------
# Table: Image
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `image`(
    image_id VARCHAR(150) NOT NULL,
    format_type VARCHAR(10) NOT NULL,
    product_id INT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Image_PK PRIMARY KEY (image_id),
    CONSTRAINT Image_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id)
);
#------------------------------------------------------------
# Table: Review
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS review(
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    rating FLOAT NOT NULL,
    comment TEXT DEFAULT NULL,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    
    CONSTRAINT Review_User_FK FOREIGN KEY (user_id) REFERENCES customer(customer_id),
    CONSTRAINT Review_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id)
);
#------------------------------------------------------------
# Table: Cart
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS cart(
    cart_id INT AUTO_INCREMENT NOT NULL,
    customer_id INT NOT NULL,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Cart_PK PRIMARY KEY (cart_id),
    CONSTRAINT Cart_Customer_FK FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);
#------------------------------------------------------------
# Table: Cart_Line
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS cart_line(
    product_id INT NOT NULL,
    cart_id INT NOT NULL,
    quantity INT NOT NULL,
    is_set_aside BOOL NOT NULL DEFAULT FALSE,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT CartLine_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id),
    CONSTRAINT CartLine_Cart_FK FOREIGN KEY (cart_id) REFERENCES cart(cart_id)
);
#------------------------------------------------------------
# Table: Employee
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS employee(
    employee_id INT AUTO_INCREMENT NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    first_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    deletion_time DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    role_id INT NOT NULL,
    password_id INT NOT NULL,
    CONSTRAINT Employee_PK PRIMARY KEY (employee_id),
    CONSTRAINT Employee_Role_FK FOREIGN KEY (role_id) REFERENCES role(role_id),
    CONSTRAINT Employee_Password_FK FOREIGN KEY (password_id) REFERENCES password(password_id)
);
#------------------------------------------------------------
# Table: Supplier_Order
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS supplier_order(
    order_id INT AUTO_INCREMENT NOT NULL,
    delivery_date DATETIME DEFAULT NULL,
    deletion_time DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    employee_id INT NOT NULL,
    status_id INT NOT NULL,
    CONSTRAINT Supplier_Order_PK PRIMARY KEY (order_id),
    CONSTRAINT SupplierOrder_Employee_FK FOREIGN KEY (employee_id) REFERENCES employee(employee_id),
    CONSTRAINT SupplierOrder_Status_FK FOREIGN KEY (status_id) REFERENCES status(status_id)
);
#------------------------------------------------------------
# Table: Order_Line
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS order_line(
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    unit_price FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT OrderLine_Order_FK FOREIGN KEY (order_id) REFERENCES `order`(order_id),
    CONSTRAINT OrderLine_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id)
);
#------------------------------------------------------------
# Table: Supplier_Order_Line
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS supplier_order_line(
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    unit_price FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT SupplierOrderLine_Order_FK FOREIGN KEY (order_id) REFERENCES supplier_order(order_id),
    CONSTRAINT SupplierOrderLine_Product_FK FOREIGN KEY (product_id) REFERENCES product(product_id)
);
