Use Adventure Works 2017 DB.

Create a table that has the following:
Table Name: Person.Login
- LoginID (PK, int, not null)
- FirstName (varchar(255), not null)
- LastName (varchar(255), not null)
- Username (varchar(255), not null)
- Password (varchar(255), not null)
- DisplayName (varchar(255), not null)
- Email (varchar(255), not null)

Then add the store procs and scalar functions from the 'SQL' folder in the repo.

There are additional db/tables needed for some of the hidden stuff to work.

(new db) - Clicker:

(new table) - ClickerUserData: Id(PK, int, no null), LoginID(nvarchar(50), not null), ClickCount(decimal(38,0), not null), LastClickDate(datetime, null), CreatedDate(datetime, not null)

(new table) - ClickerUpgrade: Id(PK, int, not null), UserId(FK, int, not null), UpgradeType(nvarchar(50), not null), Level(int, not null), PurchasedDate(datetime, not null)

