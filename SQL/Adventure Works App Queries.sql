declare @transactionName varchar(20) = 'hello'
begin transaction @transactionName
-----------------------------------------------------------------------------!

select * from person.Password

select BusinessEntityID, FirstName, LastName, * from person.Person as p

select * from HumanResources.Employee as e

select e.BusinessEntityID, p.FirstName, p.MiddleName, p.LastName, 
	   e.JobTitle, e.BirthDate, e.MaritalStatus, e.Gender, e.HireDate, 
	   e.VacationHours, e.SickLeaveHours, d.Name as dep_name, d.GroupName as dep_groupname, 
	   s.Name as shift_name, (52 * (eph.Rate * 40)) as Yearly_Salary
from HumanResources.Employee as e
	join person.person as p on e.BusinessEntityID = p.BusinessEntityID
	join HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID
	join HumanResources.Department as d on edh.DepartmentID = d.DepartmentID
	join HumanResources.Shift as s on edh.ShiftID = s.ShiftID
	join HumanResources.EmployeePayHistory as eph on e.BusinessEntityID = eph.BusinessEntityID
where eph.RateChangeDate = (
		select max(eph2.RateChangeDate) 
		from HumanResources.EmployeePayHistory as eph2
		where eph.BusinessEntityID = eph2.BusinessEntityID
	) AND EndDate is null
order by BusinessEntityID desc

select * from HumanResources.EmployeePayHistory as eph
	where eph.RateChangeDate = (
		select max(eph2.RateChangeDate) 
		from HumanResources.EmployeePayHistory as eph2
		where eph.BusinessEntityID = eph2.BusinessEntityID
	)

select JobTitle, d.Name, d.GroupName from HumanResources.Employee as e
	join HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID
	join HumanResources.Department as d on edh.DepartmentID = d.DepartmentID
	order by e.BusinessEntityID
--declare @BusinessEntityID as int set @BusinessEntityID = 1
--update HumanResources.Employee 
--set SickLeaveHours = 70
--where BusinessEntityID = @BusinessEntityID

select * from HumanResources.Employee as e
	join person.person as p on e.BusinessEntityID = p.BusinessEntityID
	join HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID

select edh.DepartmentID, edh.ShiftID, d.Name as department_name, d.GroupName, s.Name as shift_name, * from HumanResources.EmployeeDepartmentHistory as edh
	join HumanResources.Department as d on edh.DepartmentID = d.DepartmentID
	join HumanResources.Shift as s on edh.ShiftID = s.ShiftID
where d.Name = 'Executive'

execute dbo.uspGetEmployeeData @BusinessEntityID = 1

select e.BusinessEntityID, firstname, LastName from HumanResources.Employee as e
	join person.person as p on e.BusinessEntityID = p.BusinessEntityID
order by FirstName;

SELECT e.businessentityid FROM HumanResources.Employee as e
JOIN Person.Person as p ON e.BusinessEntityID = p.BusinessEntityID
ORDER BY e.BusinessEntityID;

execute dbo.uspGetEmployeeName @BusinessEntityID = 1;
-- password check?
select FirstName, LastName, LoginID, EmailAddress, PasswordHash, PasswordSalt from HumanResources.employee as emp 
	join Person.Person as per on emp.BusinessEntityID = per.BusinessEntityID
	join person.EmailAddress as ea on per.BusinessEntityID = ea.BusinessEntityID
	join person.Password as pass on per.BusinessEntityID = pass.BusinessEntityID

------------------------------------------------------------------------------

-- products:

select * from Production.ProductReview -- some reviews for a product
select pr.productID, p.Name as Product_Name, ReviewerName, format(ReviewDate, 'yyyy-MM-dd') as ReviewDate, Rating, Comments
from Production.ProductReview as pr
	join production.product as p on pr.ProductID = p.ProductID

-- product names and list price
select * from Production.Product as p

select pc.ProductCategoryID, pc.Name as Product_Catagory, ps.ProductSubcategoryID, 
	   ps.Name as Product_Subcategory, ProductID, ProductNumber, p.Name as Product_Name,
	   ListPrice, StandardCost, (ListPrice - StandardCost) as Margin_Profit, Size, Color, Weight,
	   c.Name as Culture_Name, pd.Description
from Production.Product as p
	join Production.ProductSubcategory as ps on p.ProductSubcategoryID = ps.ProductSubcategoryID
	join Production.ProductCategory as pc on ps.ProductCategoryID = pc.ProductCategoryID
	join Production.ProductModelProductDescriptionCulture as pmpdc on p.ProductModelID = pmpdc.ProductModelID
	join Production.ProductDescription as pd on pmpdc.ProductDescriptionID = pd.ProductDescriptionID
	join Production.Culture as c on pmpdc.CultureID = c.CultureID
where listprice <> 0.00 and ps.Name = 'Bottles and Cages'
order by ProductCategoryID, ProductSubcategoryID, p.Name, Culture_Name

execute dbo.ProductSearchLang @CultureID = en;

select pmpdc.ProductModelID, pmpdc.CultureID, c.name as Culture_Name, pd.Description, *
from Production.Product as p
	join Production.ProductModelProductDescriptionCulture as pmpdc on p.ProductModelID = pmpdc.ProductModelID
	join Production.ProductDescription as pd on pmpdc.ProductDescriptionID = pd.ProductDescriptionID
	join Production.Culture as c on pmpdc.CultureID = c.CultureID

select CultureID, Name 
from Production.Culture
where CultureID <> '';

declare @givenName as nvarchar(50)
set @givenName = 'English'
execute dbo.uspGetCultureID @CultureName = @givenName;

declare @Category as nvarchar(50)
set @Category = 'Bikes'
select ps.Name
from Production.ProductSubcategory as ps
	join Production.ProductCategory as pc on ps.ProductCategoryID = pc.ProductCategoryID
where pc.Name = @Category

declare @SubCategory as nvarchar(50)
set @SubCategory = 'Mountain Bikes'
select p.Name
from Production.Product as p
	join Production.ProductSubcategory as ps on p.ProductSubcategoryID = ps.ProductSubcategoryID
	join Production.ProductCategory as pc on ps.ProductCategoryID = pc.ProductCategoryID
where ps.Name = @SubCategory

--execute dbo.uspGetProductName @SubCategory = 'Mountain Bikes';

------------------------------------------------------------------------------

select ProductID, name from Production.Product as p
where p.ListPrice <> 0.00 and ProductID = 713

-- inserting into ProductReview
select * from Production.ProductReview;
select * from Production.ProductPhoto

--insert into Production.ProductReview (ProductID, ReviewerName, ReviewDate, EmailAddress, Rating, Comments, ModifiedDate)
--values
--(680, 'Tyler', GETDATE(), 'tgc@interesting.com', 5, 'This product is pretty great, love it.', GETDATE())

select GETDATE();

select * from Production.Product as p
	left join Production.ProductModel as pm on p.ProductModelID = pm.ProductModelID
where p.ProductID = 680

-- to check what product should be reviewable
select ProductID, p.Name, *
from Production.Product as p
		join Production.ProductSubcategory as ps on p.ProductSubcategoryID = ps.ProductSubcategoryID
		join Production.ProductCategory as pc on ps.ProductCategoryID = pc.ProductCategoryID
where ListPrice <> 0.00 AND ProductID = 718

-- this is how you delete a row.
--delete from Production.ProductReview where ProductReviewID = 10


-----------------------------------------------------------------------------

-- login/signup stuff 

--create table Person.Login
--(
--	LoginID int IDENTITY(1,1) PRIMARY KEY,
--	FirstName varchar(255) not null,
--	LastName varchar(255) not null,
--	Username varchar(255) not null,
--	Password varchar(255) not null,
--	DisplayName varchar(255) not null,
--	Email varchar(255) not null
--)

--insert into Person.Login (FirstName, LastName, Username, Password, DisplayName, Email)
--values
--('FirstName','LastName','admin', 'password', 'admin', 'admin@admin.com');

select * from Person.Login
where DisplayName = 'ADMIN';

declare @username as varchar(255) set @username = 'admin' 
declare @password as varchar(255) set @password = 'password' 
select username, password from Person.Login where username = @username and password = @password

-----------------------------------------------------------------------------

select sp.BusinessEntityID, sp.TerritoryID, SalesQuota, Bonus, CommissionPct, sp.SalesYTD, 
	   sp.SalesLastYear, FirstName, LastName, st.name as Region_Name, st.CountryRegionCode, [st].[group] as Continent, st.salesytd as Total_SalesYTD, st.SalesLastYear as Total_SalesYTD_LastYear
from sales.salesperson as sp
	join person.person as p on sp.BusinessEntityID = p.BusinessEntityID
	join sales.SalesTerritory as st on sp.TerritoryID = st.TerritoryID
where sp.BusinessEntityID = 275
order by st.SalesYTD

select p.ProductID, StartDate from production.Product as p
join [Production].[ProductListPriceHistory] as plph on plph.ProductID = p.ProductID

-- example of a scalar function being used
print [dbo].[ufnGetProductListPrice](765, '2012-05-30')

declare @name as varchar(255) set @name = 'Syed Abbas'
select sp.BusinessEntityId, (FirstName + ' ' + LastName) as full_name, * from Sales.SalesPerson as sp
	join person.Person as p on sp.BusinessEntityID = p.BusinessEntityID
	join sales.SalesTerritory as st on sp.TerritoryID = st.TerritoryID
where (FirstName + ' ' + LastName) = @name;

select * from sales.salesperson as sp;

select JobTitle, sp.* from sales.SalesPerson as sp
	join HumanResources.Employee as e on sp.BusinessEntityID = e.BusinessEntityID

declare @ID as int set @ID = 285
select sp.BusinessEntityID, sp.TerritoryID, SalesQuota, Bonus, CommissionPct, sp.SalesYTD, 
	   sp.SalesLastYear, FirstName, LastName, st.name as Region_Name, st.CountryRegionCode,
	   [st].[group] as Continent, st.salesytd as Total_SalesYTD, st.SalesLastYear as Total_SalesYTD_LastYear
from Sales.SalesPerson as sp
	join person.person as p on sp.BusinessEntityID = p.BusinessEntityID
    left join sales.SalesTerritory as st on sp.TerritoryID = st.TerritoryID
where sp.BusinessEntityID = @ID;

declare @name as varchar(255) set @name = 'Northwest'
select distinct Name from sales.SalesTerritory where Name = @name;

execute dbo.uspGetSalesPersonData @ID = 275;

-----------------------------------------------------------------------------

select * from sales.ShoppingCartItem

select s.*, 
	s.[Demographics].value('declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey"; 
	(/StoreSurvey/AnnualSales)[1]', 'money') AS [AnnualSales], 
	p.*, e.JobTitle 
from sales.Store as s
	join Person.Person as p on s.SalesPersonID = p.BusinessEntityID
	join HumanResources.Employee as e on p.BusinessEntityID = e.BusinessEntityID

select swa.BusinessEntityID, swa.Name, AddressType, AddressLine1, AddressLine2, City, StateProvinceName, PostalCode, CountryRegionName,
	   swc.ContactType, swc.Title, swc.FirstName, swc.MiddleName, swc.LastName, swc.Suffix, swc.PhoneNumber, swc.PhoneNumberType, swc.EmailAddress, swc.EmailPromotion,
	   swd.AnnualSales, swd.AnnualRevenue, swd.BankName, swd.BusinessType, swd.YearOpened, swd.Specialty, swd.SquareFeet, swd.Brands, swd.Internet, swd.NumberEmployees
from sales.vStoreWithAddresses as swa
	join sales.vStoreWithContacts as swc on swa.BusinessEntityID = swc.BusinessEntityID
	join sales.vStoreWithDemographics as swd on swa.BusinessEntityID = swd.BusinessEntityID
order by swa.AddressType asc, swc.ContactType asc

select * from sales.vStoreWithAddresses as swa
order by name;
select * from sales.vStoreWithContacts as swc
order by name, ContactType;
select * from sales.vStoreWithDemographics as swd
order by name;

select * from sales.vStoreWithAddresses as swa
	join sales.vStoreWithDemographics as swd on swa.BusinessEntityID = swd.BusinessEntityID
	order by swa.BusinessEntityID

select * from sales.store

declare @choice as varchar(500) set @choice = 'Stylish'
execute dbo.SearchStoreName @StoreName = @choice;

select distinct swa.BusinessEntityID, swa.Name, AddressType, AddressLine1, AddressLine2, City, StateProvinceName, PostalCode, CountryRegionName,
		swc.ContactType, swc.Title, swc.FirstName, swc.MiddleName, swc.LastName, swc.Suffix, swc.PhoneNumber, swc.PhoneNumberType, swc.EmailAddress, swc.EmailPromotion,
		swd.AnnualSales, swd.AnnualRevenue, swd.BankName, swd.BusinessType, swd.YearOpened, swd.Specialty, swd.SquareFeet, swd.Brands, swd.Internet, swd.NumberEmployees
	from sales.vStoreWithAddresses as swa
		join sales.vStoreWithContacts as swc on swa.BusinessEntityID = swc.BusinessEntityID
		join sales.vStoreWithDemographics as swd on swa.BusinessEntityID = swd.BusinessEntityID
order by swa.BusinessEntityID, swa.name

select distinct * from sales.vStoreWithAddresses as swa
	join sales.vStoreWithDemographics as swd on swa.BusinessEntityID = swd.BusinessEntityID
where AddressType <> 'shipping'
order by swa.Name

select * from sales.vStoreWithAddresses where AddressType <> 'shipping' order by BusinessEntityID
execute dbo.SearchStoreName @StoreName = ''

-- checking for only dupes with the store contacts
SELECT *
FROM (
    SELECT 
        s.[BusinessEntityID], 
        s.[Name], 
        ct.[Name] AS [ContactType], 
        p.[Title], 
        p.[FirstName], 
        p.[MiddleName], 
        p.[LastName], 
        p.[Suffix], 
        pp.[PhoneNumber], 
        pnt.[Name] AS [PhoneNumberType],
        ea.[EmailAddress], 
        p.[EmailPromotion]
    FROM [Sales].[Store] s
        INNER JOIN [Person].[BusinessEntityContact] bec 
            ON bec.[BusinessEntityID] = s.[BusinessEntityID]
        INNER JOIN [Person].[ContactType] ct
            ON ct.[ContactTypeID] = bec.[ContactTypeID]
        INNER JOIN [Person].[Person] p
            ON p.[BusinessEntityID] = bec.[PersonID]
        LEFT OUTER JOIN [Person].[EmailAddress] ea
            ON ea.[BusinessEntityID] = p.[BusinessEntityID]
        LEFT OUTER JOIN [Person].[PersonPhone] pp
            ON pp.[BusinessEntityID] = p.[BusinessEntityID]
        LEFT OUTER JOIN [Person].[PhoneNumberType] pnt
            ON pnt.[PhoneNumberTypeID] = pp.[PhoneNumberTypeID]
) base
WHERE base.BusinessEntityID IN (
    SELECT BusinessEntityID
    FROM (
        SELECT s.[BusinessEntityID]
        FROM [Sales].[Store] s
            INNER JOIN [Person].[BusinessEntityContact] bec 
                ON bec.[BusinessEntityID] = s.[BusinessEntityID]
            INNER JOIN [Person].[ContactType] ct
                ON ct.[ContactTypeID] = bec.[ContactTypeID]
            INNER JOIN [Person].[Person] p
                ON p.[BusinessEntityID] = bec.[PersonID]
            LEFT OUTER JOIN [Person].[EmailAddress] ea
                ON ea.[BusinessEntityID] = p.[BusinessEntityID]
            LEFT OUTER JOIN [Person].[PersonPhone] pp
                ON pp.[BusinessEntityID] = p.[BusinessEntityID]
            LEFT OUTER JOIN [Person].[PhoneNumberType] pnt
                ON pnt.[PhoneNumberTypeID] = pp.[PhoneNumberTypeID]
    ) sub
    GROUP BY BusinessEntityID
    HAVING COUNT(*) > 1
)
ORDER BY base.BusinessEntityID


-----------------------------------------------------------------------------

-- edit store details page 
-- TO REFRESH QUERY PAGE | Edit -> IntelliSense -> Refresh Local Cache

-- address related -- (DE) = dont edit
select * from sales.vStoreWithAddresses where name = 'Finer Sales and Service'

select * from sales.Store -- not updating (BusinessEntityID, Name, SalesPersonID)

select * from person.AddressType as at -- has addressTypeID which tells the name (connect to bea), so go here first to get the name then ID then back to BEA to update the AddressTypeID

select * from person.address as a -- contains the AddressID (Connected via bea), AddressLine1, AddressLine2, City, StateProvinceID (SP), PostalCode

select * from person.StateProvince as sp -- (DE) reverse Name from here to get the StateProvinceID (this is also where to check if it is a valid StateName)

select * from person.CountryRegion as cr -- (DE) grab Name then find it's countryregioncode (then go to SP), 

select * from [Person].[BusinessEntityAddress] bea -- has the BusinessEntityID, AddressID, and AddressTypeID

select * from sales.Store as s
	join person.BusinessEntityAddress as bea
	on s.BusinessEntityID = bea.BusinessEntityID
	join person.Address as a
	on bea.AddressID = a.AddressID
	join person.StateProvince as sp
	on a.StateProvinceID = sp.StateProvinceID
	join person.CountryRegion as cr
	on sp.CountryRegionCode = cr.CountryRegionCode
order by s.BusinessEntityID

select dbo.ufnGetStateProvinceID('Alberta'), dbo.ufnGetCountryCode('United States');

select dbo.ufnGetAddressID('1','2')

select dbo.ufnGetAddressTypeID('Main Office')


-- contacts related -- (DE) = dont edit
select PhoneNumber, PhoneNumberType, EmailAddress, EmailPromotion from sales.vStoreWithContacts
where BusinessEntityID = 644
order by Name, ContactType

select * from sales.Store as s-- Holds the BusinessEntityID, Name, SalesPersonID | (DE)
where name = 'Aerobic Exercise Company'

select * from person.BusinessEntityContact as bec -- Holds BusinessEntityID, PersonID, ContactTypeID | (DE)?

select * from Person.ContactType as ct -- Holds ContactTypeID, Name | (DE) | Reason: Just need the ct.ContactTypeID by using ct.Name

select * from Person.Person as p -- Holds BusinessEntityID, Title, FirstName, MiddleName, LastName, Suffix, EmailPromotion

select * from Person.EmailAddress as ea -- Holds BusinessEntityID, EmailAddressID, EmailAddress | 
										-- For some reason BusinessEntityID is 1 less than it is here than it is in vStoreWithContacts 
										-- so keep that in mind. But use Person.EmailAddress (BusinessEntityID) to connect to sales.Store (BusinessEntityID)

select * from Person.PersonPhone as pp -- Holds BusinessEntityID, PhoneNumber, PhoneNumberTypeID | Same as Person.EmailAddress, it's offset by 1

select * from Person.PhoneNumberType as pnt -- Holds PhonenumberTypeID, Name | Will need to get the PhoneNumberTypeID as will be given the Name. 
											-- Create a func for this. Additionally connection is pp.BusinessEntityID=pnt.BusinessEntityID

select * from person.BusinessEntityContact as bea
	join person.person as p on bea.PersonID = p.BusinessEntityID
	join person.PersonPhone as pp on p.BusinessEntityID = pp.BusinessEntityID
	join person.PhoneNumberType as pnt on pp.PhoneNumberTypeID = pnt.PhoneNumberTypeID 
where bea.BusinessEntityID = 644

-- 
select bea.PersonID from person.BusinessEntityContact as bea
	join person.person as p on bea.PersonID = p.BusinessEntityID

-- getting personID's
execute dbo.uspGetPersonIDs @BusinessEntityID = 644

declare @PersonID as int set @PersonID = 643
select * from person.PersonPhone as pp
	where BusinessEntityID = @PersonID

select dbo.ufnGetPhoneNumberTypeID('Cell') as PhoneNumberTypeID

-- could use this as validation instead of comparing fullnames (as there could technically be dupes)
select ct.ContactTypeID, ct.Name from person.BusinessEntityContact as bec
	join person.ContactType as ct on bec.ContactTypeID = ct.ContactTypeID
where PersonID = @PID

execute uspUpdatePersonPhoneNumberTypeID @ID = @PhoneNumberTypeID, @PersonID = @PID

execute uspUpdatePersonPhoneNumber @PersonID = @PID, @PhoneNumber = @PN
execute uspUpdatePersonEmailAddress @PersonID = @PID, @EmailAddress = @EA
execute uspUpdatePersonEmailPromotion @PersonID = @PID, @EmailPromotion = @EP

-- Cant update Owner as it is a type, would need to update the ID 
--update sales.vStoreWithContacts
--set ContactType = 'Owner'
--where BusinessEntityID = '2051' and ContactType = 'Ownera'

select * from person.person as p
	join person.PersonPhone as pp on p.BusinessEntityID = pp.BusinessEntityID
where (p.FirstName = 'Bryan' and p.LastName = 'Walton') or (p.FirstName = 'Rosmarie' and p.LastName = 'Carroll')

-- what can be edited (PhoneNumber, EmailAddress, EmailPromotion) - Extra edit outside the viewtable (PhoneNumberType) - need ID
-- change PhoneNumberType through: select * from Person.PersonPhone as pp
declare @BusinessEntityID as int set @BusinessEntityID = 644
declare @ContactType as nvarchar(50) set @ContactType = 'Owner'
select * from Sales.vStoreWithContacts where BusinessEntityID = @BusinessEntityID and ContactType = @ContactType


-----------------------------------------------------------------------------

select dbo.ufnGetDisplayName('admin','password')
execute dbo.uspGetUsernamePassword @Username = 'admin', @Password = 'password'

select distinct JobTitle from HumanResources.Employee as e
	join HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID 
	join HumanResources.Department as d on edh.DepartmentID = d.DepartmentID
where d.Name = 'executive'

select Rate from HumanResources.EmployeePayHistory where BusinessEntityID

select * from HumanResources.EmployeePayHistory where BusinessEntityID = 1

select * from HumanResources.Department;

select * from production.ProductReview

select name from Production.Culture where CultureID <> ''

execute dbo.uspGetAllNotSpanishLanguages

select * from Production.Culture

execute dbo.ProductSearchLang @CultureID = 'zh-cht' -- es don't have products

select productID, ReviewerName, format(ReviewDate, 'yyyy-MM-dd') as ReviewDate, Rating, Comments from Production.ProductReview;

select name from Production.Product where ProductID = 718

-- delete from Production.ProductReview where ProductID = 718

select sp.BusinessEntityID, (FirstName + ' ' + LastName) as full_name from Sales.SalesPerson as sp
	join Person.Person as p on sp.BusinessEntityID = p.BusinessEntityID
where (FirstName + ' ' + LastName) = @name

declare @Name as varchar(500) set @Name = 'Syed Abbas'
select dbo.ufnValidateSalesPersonIDWithName(@Name)

select st.Name from sales.SalesTerritory as st

select * from sales.SalesTerritory

select * from Person.Login

--delete from Person.Login where LoginID = 7

select LoginID from Person.Login where username = 'admin'

select dbo.ufnCheckUserName('admin')

-----------------------------------------------------------------------------!
rollback transaction @transactionName