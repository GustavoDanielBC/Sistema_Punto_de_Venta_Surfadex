CREATE DATABASE DBVENTA

use DBVENTA

CREATE TABLE Rol(
idRol INT PRIMARY KEY IDENTITY(1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

CREATE TABLE Accesos(
idMenu INT PRIMARY KEY IDENTITY(1,1),
nombre varchar(50),
icono varchar(50),
url varchar(50)
)

CREATE TABLE Permisos(
idMenuRol INT PRIMARY KEY IDENTITY(1,1),
idMenu INT REFERENCES Menu(idMenu),
IdRol int REFERENCES Rol(idRol)
)


CREATE TABLE Usuario(
idUsuario INT PRIMARY KEY IDENTITY(1,1),
nombreCompleto varchar(100),
correo varchar(50),
idRol INT REFERENCES Rol(idRol),
clave varchar(40),
esActivo bit default 1,
fechaRegistro DATETIME DEFAULT getdate()
)

CREATE TABLE Categoria(
idCategoria INT PRIMARY KEY IDENTITY(1,1),
nombre varchar(50),
esActivo BIT DEFAULT 1,
fechaRegistro datetime default getdate()
)

CREATE TABLE Producto(
IdProducto INT PRIMARY KEY IDENTITY (1,1),
nombre varchar(100),
idCategoria INT REFERENCES Categoria(idCategoria),
stock int,
precio DECIMAL(10,2),
esActivo BIT DEFAULT 1,
fechaRegistro datetime default getdate()

)


CREATE TABLE Venta(
idVenta INT PRIMARY KEY IDENTITY (1,1),
folioFiscal varchar(40),
tipoPago varchar(50),
total DECIMAL (10,2),
fechaRegistro datetime default getDate()
)

CREATE TABLE DetalleVenta(
idDetalleVenta INT PRIMARY KEY IDENTITY (1,1),
idVenta INT REFERENCES Venta(idVenta),
idProducto int REFERENCES Producto(idProducto),
cantidad int,
precio DECIMAL(10,2),
total DECIMAL(10,2)

)



