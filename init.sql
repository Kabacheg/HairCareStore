CREATE DATABASE ProductDataBase;

USE ProductDataBase;



CREATE TABLE products (
    Id INT PRIMARY KEY Identity,
    [Name] NVARCHAR(max) NOT NULL,
    [Description] TEXT,
    [Price] money NOT NULL
);
