-- Active: 1732875427459@@127.0.0.1@3306@gestion_stock
INSERT INTO status (name) VALUES ('En cours de traitement'), ('En préparation'), ('Remise en stock'), ('Annulée'), ('Expédiée'), ('En cours de livraison'), ('Non livrée'), ('Livrée');

INSERT INTO role (roleId, name) VALUES (999999, 'Bot');
INSERT INTO password (password_id, password_hash, password_salt, attenmpt_count) VALUES (999999, 'hash', 'salt', 0);
INSERT INTO employee (employee_id, last_name, first_name, email, phone, role_id, password_id) VALUES (999999, 'Bot', 'Bot', 'bot@bot.bot','000', 999999, 999999);