declare @transactionName varchar(20) = 'hello'
begin transaction @transactionName
-----------------------------------------------------------------------------!

-- Clicker Queries

select * from AdventureWorks2017.Person.Login -- using the loginID to see which user info to get

-- clicker user data table created

--CREATE TABLE ClickerUserData (
--    Id INT IDENTITY(1,1) PRIMARY KEY,
--    LoginID NVARCHAR(50) NOT NULL,
--    ClickCount INT NOT NULL DEFAULT 0,
--    LastClickDate DATETIME NULL,
--    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
--);

-- clicker upgrade tabel created

--CREATE TABLE ClickerUpgrade (
--    Id INT IDENTITY(1,1) PRIMARY KEY,
--    UserId INT NOT NULL,
--    UpgradeType NVARCHAR(50) NOT NULL,
--    Level INT NOT NULL DEFAULT 1,
--    PurchasedDate DATETIME NOT NULL DEFAULT GETDATE(),
--    CONSTRAINT FK_ClickerUpgrade_User FOREIGN KEY (UserId)
--        REFERENCES ClickerUserData(Id)
--);

select * from ClickerUserData -- users are added in the signup on adventureworks db then once it is confirm it is added here via loginID.
--
INSERT INTO ClickerUserData (LoginID)
VALUES (2);

-----------------------------------------------------------------------------!
rollback transaction @transactionName