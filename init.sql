CREATE DATABASE ProductDataBase;

USE ProductDataBase;


CREATE TABLE Tutorials (
    Id INT PRIMARY KEY Identity,
    [Topic] NVARCHAR(max) NOT NULL,
    [Instruction] TEXT NOT NULL,
);

CREATE TABLE Products (
    Id INT PRIMARY KEY Identity,
    [Name] NVARCHAR(max) NOT NULL,
    [Description] TEXT,
    [Price] money NOT NULL
);


select * from Tutorials