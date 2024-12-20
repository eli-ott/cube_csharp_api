-- Active: 1732875427459@@127.0.0.1@3306@gestion_stock
#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------
DROP DATABASE IF EXISTS gestion_stock;
CREATE DATABASE IF NOT EXISTS gestion_stock;
USE gestion_stock;
#------------------------------------------------------------
# Table: Statut
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Statut(
    id_statut INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Statut_PK PRIMARY KEY (id_statut)
);
#------------------------------------------------------------
# Table: Famille
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Famille(
    id_famille INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Famille_PK PRIMARY KEY (id_famille)
);
#------------------------------------------------------------
# Table: Fonction
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Fonction(
    id_fonction INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (50) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Fonction_PK PRIMARY KEY (id_fonction)
);
#------------------------------------------------------------
# Table: Mot_de_passe
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Mot_de_passe(
    id_mot_de_passe INT AUTO_INCREMENT NOT NULL,
    mdp_hash TEXT NOT NULL,
    nombre_essais INT NOT NULL DEFAULT 0,
    date_reinitialisation DATETIME DEFAULT NULL,
    date_suppression DATETIME DEFAULT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Mot_de_passe_PK PRIMARY KEY (id_mot_de_passe)
);
#------------------------------------------------------------
# Table: Adresse
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Adresse(
    id_adresse INT AUTO_INCREMENT NOT NULL,
    adresse VARCHAR(255) NOT NULL,
    ville VARCHAR (255) NOT NULL,
    code_postal INT NOT NULL,
    pays VARCHAR (255) NOT NULL,
    complement VARCHAR(255) DEFAULT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Id_adresse_PK PRIMARY KEY (id_adresse)
);
#------------------------------------------------------------
# Table: Client
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Client(
    id_client INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone VARCHAR(15) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    actif BOOLEAN NOT NULL DEFAULT 0,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    id_mot_de_passe INT NOT NULL,
    id_adresse INT NOT NULL,
    CONSTRAINT Client_PK PRIMARY KEY (id_client),
    CONSTRAINT id_mot_de_passe_client_fk FOREIGN KEY (id_mot_de_passe) REFERENCES Mot_de_passe(id_mot_de_passe),
    CONSTRAINT id_adresse_client_fk FOREIGN KEY (id_adresse) REFERENCES Adresse(id_adresse)
);
#------------------------------------------------------------
# Table: Commande
#------------------------------------------------------------
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
);
#------------------------------------------------------------
# Table: Fournisseur
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Fournisseur(
    id_fournisseur INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    contact VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone VARCHAR(15) NOT NULL,
    siret VARCHAR (255) NOT NULL,
    date_suppression DATETIME DEFAULT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_mot_de_passe INT NOT NULL,
    id_adresse INT NOT NULL,
    CONSTRAINT Fournisseur_PK PRIMARY KEY (id_fournisseur),
    CONSTRAINT id_adresse_fournisseur_fk FOREIGN KEY (id_adresse) REFERENCES Adresse(id_adresse),
    CONSTRAINT id_mot_de_passe_fournisseur_fk FOREIGN KEY (id_mot_de_passe) REFERENCES Mot_de_passe(id_mot_de_passe)
);
#------------------------------------------------------------
# Table: Produit
#------------------------------------------------------------
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
);
#------------------------------------------------------------
# Table: Promotion
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Promotion(
    id_promotion INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR(50) NOT NULL,
    valeur INT NOT NULL,
    date_debut DATETIME DEFAULT CURRENT_TIMESTAMP,
    date_fin DATETIME NOT NULL,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    id_produit INT NOT NULL,
    CONSTRAINT id_promotion PRIMARY KEY (id_promotion),
    CONSTRAINT id_produit_promotion_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
);
#------------------------------------------------------------
# Table: Image
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Image(
    id_image TEXT AUTO_INCREMENT NOT NULL,
    type_format VARCHAR(10) NOT NULL,
    id_produit INT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_image_pk PRIMARY KEY (id_image),
    CONSTRAINT id_produit_image_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
);
#------------------------------------------------------------
# Table: Evaluer
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Evaluer(
    id_utilisateur INT NOT NULL,
    id_produit INT NOT NULL,
    note FLOAT NOT NULL,
    commentaire TEXT DEFAULT NULL,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    
    CONSTRAINT id_utilisateur_client_evaluer_fk FOREIGN KEY (id_utilisateur) REFERENCES Client(id_client),
    CONSTRAINT id_produit_evaluer_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
);
#------------------------------------------------------------
# Table: Panier
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Panier(
    id_panier INT AUTO_INCREMENT NOT NULL,
    id_client INT NOT NULL,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_panier_pk PRIMARY KEY (id_panier),
    CONSTRAINT id_client_panier_fk FOREIGN KEY (id_client) REFERENCES Client(id_client)
);
#------------------------------------------------------------
# Table: Ligne_panier
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Ligne_panier(
    id_produit INT NOT NULL,
    id_panier INT NOT NULL,
    quantite INT NOT NULL,
    de_cote BOOL NOT NULL DEFAULT FALSE,
    update_time DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_produit_ligne_panier_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit),
    CONSTRAINT id_panier_ligne_panier_fk FOREIGN KEY (id_panier) REFERENCES Panier(id_panier)
);
#------------------------------------------------------------
# Table: Employe
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Employe(
    id_employe INT AUTO_INCREMENT NOT NULL,
    nom VARCHAR (255) NOT NULL,
    prenom VARCHAR (255) NOT NULL,
    email VARCHAR (255) NOT NULL,
    telephone VARCHAR(15) NOT NULL,
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
);
#------------------------------------------------------------
# Table: Commande_Fournisseur
#------------------------------------------------------------
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
);
#------------------------------------------------------------
# Table: Ligne_commande
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Ligne_commande(
    id_commande INT NOT NULL,
    id_produit INT NOT NULL,
    quantite INT NOT NULL,
    prix_unitaire FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_commande_ligne_commande_fk FOREIGN KEY (id_commande) REFERENCES Commande(id_commande),
    CONSTRAINT id_produit_ligne_commande_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
);
#------------------------------------------------------------
# Table: Ligne_commande_fournisseur
#------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Ligne_commande_fournisseur(
    id_commande INT NOT NULL,
    id_produit INT NOT NULL,
    quantite INT NOT NULL,
    prix_unitaire FLOAT NOT NULL,
    update_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creation_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT id_commande_ligne_commande_fournisseur_fk FOREIGN KEY (id_commande) REFERENCES Commande(id_commande),
    CONSTRAINT id_produit_ligne_commande_fournisseur_fk FOREIGN KEY (id_produit) REFERENCES Produit(id_produit)
);