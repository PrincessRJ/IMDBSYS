CREATE DATABASE UCLM

CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    Age INT,
    Year INT
);

DROP TABLE Students;
SELECT * FROM Students