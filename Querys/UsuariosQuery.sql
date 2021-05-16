USE [master] 
GO

CREATE DATABASE [Farmacia]
GO

USE [Farmacia] 
GO

CREATE SCHEMA [Usuario]
GO

create table [Usuario].[TIPOSUSUARIOS](
[tipoUsuarioID] smallint not null primary key identity(1,1),
[tipoUsuario] varchar(50) not null
);
GO

create table [Usuario].[USUARIOS](
[usuarioID] int not null primary key identity(1,1),
[primerNombre] varchar(50) not null,
[primerApellido] varchar(50) not null,
[nombreUsuario] varchar(50) unique not null ,
[contraseña] varchar(50) not null,
[tipoUsuario] smallint not null,
[email] varchar(50) not null
CONSTRAINT [fk_TipoUsuario] FOREIGN KEY ([tipoUsuario]) REFERENCES [Usuario].[TIPOSUSUARIOS]([tipoUsuarioID])
);
GO

insert into [Usuario].[TIPOSUSUARIOS] values 
('Administrator'),('Manager'),('User');
GO

insert into [Usuario].[USUARIOS] values 
('Enrique','Rayo','Enriqu32001','12345',1,'enriqueantoniorayoequeira@yahoo.com'),
('Juan','Aguilar','JuanitoGamer501','54321',2,'juanaguilar771@gmail.com'),
('Pedro','Gonzales','TheGon23','987654321',3,'gonzalespedro200@outlook.com');
GO

CREATE PROC ObtenerTablaUsuariovalido (@user AS varchar(50), @pass AS varchar(50))
AS
BEGIN
	SELECT USUARIOS.usuarioID, USUARIOS.primerNombre, USUARIOS.primerApellido,TIPOSUSUARIOS.tipoUsuario, USUARIOS.email 
	FROM Usuario.USUARIOS JOIN Usuario.TIPOSUSUARIOS ON USUARIOS.tipoUsuario = TIPOSUSUARIOS.tipoUsuarioID
	WHERE @user = USUARIOS.nombreUsuario and @pass = USUARIOS.contraseña
END 
GO

---------------------------------------------
------------EJECUTAR HASTA AQUI--------------
---------------------------------------------

declare @user varchar(50) = 'Enriqu32001'
declare @pass varchar(50) = '12345'
EXEC ObtenerTablaUsuariovalido @user, @pass
GO



ALTER SCHEMA [Usuario] TRANSFER [User].TipoUsuarios
GO

ALTER SCHEMA [Usuario] TRANSFER [User].[Usuario].[Usuario]
GO

select * from [Usuario].[USUARIOS]
GO

select * from [Usuario].[TIPOSUSUARIOS]
GO


drop table [Usuario].[TIPOSUSUARIOS]
GO

drop table [Usuario].[USUARIOS]
GO

delete [Usuario].[USUARIOS]
GO

truncate table [Usuario].Usuario
GO

declare @user varchar(50) = 'Enriqu32001'
declare @pass varchar(50) = '12345'
select * from [Usuario].[USUARIOS] where [nombreUsuario] = @user and [contraseña] = @pass
GO

