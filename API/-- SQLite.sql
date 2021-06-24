-- SQLite
DELETE from Users where id=1;
DELETE from Users where id=2;
DELETE from Users where id=3;

INSERT INTO Users (Id, UserName)
 VALUES (1, "Bob");
INSERT INTO Users (Id, UserName)
VALUES (2, "Tom");
INSERT INTO Users (Id, UserName)
VALUES (3, "Ion");