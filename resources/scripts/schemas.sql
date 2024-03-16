create database db_midias;


IF OBJECT_ID(N'dbo.Midia') IS NOT NULL  
   DROP TABLE [dbo].Midia;  
GO

use db_midias
CREATE TABLE Midia (
	Id INT PRIMARY KEY IDENTITY,
	FilePath NVARCHAR(100),
	CreationDate DATETIME
);

GO