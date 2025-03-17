CREATE DATABASE HairCareStoreDataBase;

USE HairCareStoreDataBase;


CREATE TABLE HttpLog (
    RequestId VARCHAR(255) PRIMARY KEY,
    Url VARCHAR(1000) NOT NULL,
    RequestBody TEXT NULL,
    RequestHeaders TEXT NULL,
    MethodType VARCHAR(50) NOT NULL,
    ResponseBody TEXT NULL,
    ResponseHeaders TEXT NULL,
    StatusCode INT NOT NULL,
    CreationDateTime DATETIME NOT NULL DEFAULT GETUTCDATE(),
    EndDateTime DATETIME NULL,
    ClientIp VARCHAR(45) NULL
);

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


select * from HttpLog