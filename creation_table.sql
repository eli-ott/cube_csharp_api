-- Active: 1732875427459@@127.0.0.1@3306@gestion_stock
#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------
CREATE DATABASE IF NOT EXISTS gestion_stock;
USE gestion_stock;
#------------------------------------------------------------
# Table: Statut
#------------------------------------------------------------
DROP TABLE IF EXISTS Statut;
CREATE TABLE IF NOT EXISTS Statut(
    id_statut INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Statut_PK PRIMARY KEY (id_statut)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Famille
#------------------------------------------------------------
DROP TABLE IF EXISTS Famille;
CREATE TABLE IF NOT EXISTS Famille(
    id_famille INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Famille_PK PRIMARY KEY (id_famille)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Fonction
#------------------------------------------------------------
DROP TABLE IF EXISTS Fonction;
CREATE TABLE IF NOT EXISTS Fonction(
    id_fonction INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Fonction_PK PRIMARY KEY (id_fonction)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Mot_de_passe
#------------------------------------------------------------
DROP TABLE IF EXISTS Mot_de_passe;
CREATE TABLE IF NOT EXISTS Mot_de_passe(
    id_mot_de_passe INT AUTO_INCREMENT NOT NULL,
    mdp_hash TEXT NOT NULL,
    nombre_essais INT NOT NULL DEFAULT 0,
    date_reinitialisation DATETIME DEFAULT NULL,
    date_suppression DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Mot_de_passe_PK PRIMARY KEY (id_mot_de_passe)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Adresse
#------------------------------------------------------------
DROP TABLE IF EXISTS Adresse;
CREATE TABLE IF NOT EXISTS Adresse(
    id_adresse INT AUTO_INCREMENT NOT NULL,
    numero_rue INT NOT NULL,
    nom_rue VARCHAR (255) NOT NULL,
    code_postal INT NOT NULL,
    pays VARCHAR (255) NOT NULL,
    complément VARCHAR(255) DEFAULT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Id_adresse_PK PRIMARY KEY (id_adresse)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Client
#------------------------------------------------------------
DROP TABLE IF EXISTS Client;
CREATE TABLE IF NOT EXISTS Client(
    id_client INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone INT NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    actif BOOLEAN NOT NULL DEFAULT 0,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    id_mot_de_passe INT NOT NULL,
    id_adresse INT NOT NULL,
    CONSTRAINT Client_PK PRIMARY KEY (id_client),
    CONSTRAINT id_mot_de_passe_client_fk FOREIGN KEY (id_mot_de_passe) REFERENCES Mot_de_passe(id_mot_de_passe),
    CONSTRAINT id_adresse_client_fk FOREIGN KEY (id_adresse) REFERENCES Adresse(id_adresse)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Commande
#------------------------------------------------------------
DROP TABLE IF EXISTS Commande;
CREATE TABLE IF NOT EXISTS Commande(
    id_commande INT AUTO_INCREMENT NOT NULL,
    date_livraison DATETIME DEFAULT NULL,
    id_statut INT NOT NULL,
    id_client INT NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Commande_PK PRIMARY KEY (id_commande),
    CONSTRAINT id_statut_commande_fk FOREIGN KEY (id_statut) REFERENCES Statut(id_statut),
    CONSTRAINT id_client_commande_fk FOREIGN KEY (id_client) REFERENCES Client(id_client)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Fournisseur
#------------------------------------------------------------
DROP TABLE IF EXISTS Fournisseur;
CREATE TABLE IF NOT EXISTS Fournisseur(
    id_fournisseur INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    contact VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone INT NOT NULL,
    siret VARCHAR (255) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_mot_de_passe INT NOT NULL,
    id_adresse INT NOT NULL,
    CONSTRAINT Fournisseur_PK PRIMARY KEY (id_fournisseur),
    CONSTRAINT id_adresse_fournisseur_fk FOREIGN KEY (id_adresse) REFERENCES Adresse(id_adresse),
    CONSTRAINT id_mot_de_passe_fournisseur_fk FOREIGN KEY (id_mot_de_passe) REFERENCES Mot_de_passe(id_mot_de_passe)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Produit
#------------------------------------------------------------
DROP TABLE IF EXISTS Produit;
CREATE TABLE IF NOT EXISTS Produit(
    id_produit INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    cuvee VARCHAR (255) NOT NULL,
    annee INT NOT NULL,
    nom_producteur VARCHAR (255) NOT NULL,
    bio BOOLEAN NOT NULL DEFAULT 0,
    prix_unitaire FLOAT,
    prix_carton FLOAT,
    quantite INT NOT NULL,
    reaprovisionnement_auto BOOLEAN NOT NULL DEFAULT 0,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_famille INT NOT NULL,
    id_fournisseur INT NOT NULL,
    CONSTRAINT Produit_PK PRIMARY KEY (id_produit),
    CONSTRAINT id_famille_produit_fk FOREIGN KEY (id_famille) REFERENCES Famille(id_famille),
    CONSTRAINT id_fournisseur_produit_fk FOREIGN KEY (id_fournisseur) REFERENCES Fournisseur(id_fournisseur)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Employe
#------------------------------------------------------------
DROP TABLE IF EXISTS Employe;
CREATE TABLE IF NOT EXISTS Employe(
    id_employe INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone INT NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    id_fonction INT NOT NULL,
    id_mot_de_passe INT NOT NULL,
    id_adresse INT NOT NULL,
    CONSTRAINT Employe_PK PRIMARY KEY (id_employe),
    CONSTRAINT id_fonction_employe_fk FOREIGN KEY (id_fonction) REFERENCES Fonction(id_fonction),
    CONSTRAINT id_mot_de_passe_employe_fk FOREIGN KEY (id_mot_de_passe) REFERENCES Mot_de_passe(id_mot_de_passe),
    CONSTRAINT id_adresse_employe_fk FOREIGN KEY (id_adresse) REFERENCES Adresse(id_adresse)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Commande_Fournisseur
#------------------------------------------------------------
DROP TABLE IF EXISTS Commande_Fournisseur;
CREATE TABLE IF NOT EXISTS Commande_Fournisseur(
    id_commande INT AUTO_INCREMENT NOT NULL,
    date_livraison DATETIME DEFAULT NULL,
    date_suppression DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    id_employe INT NOT NULL,
    id_statut INT NOT NULL,
    CONSTRAINT Commande_Fournisseur_PK PRIMARY KEY (id_commande),
    CONSTRAINT id_employe_commande_fournisseur_fk FOREIGN KEY (id_employe) REFERENCES Employe(id_employe),
    CONSTRAINT id_statut_commande_fournisseur_fk FOREIGN KEY (id_statut) REFERENCES Statut(id_statut)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Concerner
#------------------------------------------------------------
DROP TABLE IF EXISTS Concerner;
CREATE TABLE IF NOT EXISTS Concerner(
    id_commande INT NOT NULL,
    id_produit INT NOT NULL,
    quantite INT NOT NULL,
    prix_unitaire FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_commande_concerner_fk FOREIGN KEY (id_commande) REFERENCES Commande(id_commande),
    CONSTRAINT id_produit_concerner_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
) ENGINE = InnoDB;
#------------------------------------------------------------
# Table: Concerner_Fournisseur
#------------------------------------------------------------
DROP TABLE IF EXISTS Concerner_Fournisseur;
CREATE TABLE IF NOT EXISTS Concerner_Fournisseur(
    id_commande INT NOT NULL,
    id_produit INT NOT NULL,
    quantite INT NOT NULL,
    prix_unitaire FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_commande_concerner_fournisseur_fk FOREIGN KEY (id_commande) REFERENCES Commande(id_commande),
    CONSTRAINT id_produit_concerner_fournisseur_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
) ENGINE = InnoDB;