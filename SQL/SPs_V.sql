use p5g1;

-- Create SP to insert Writer
GO
CREATE PROCEDURE movies.sp_AddWriter (
									@ssn int, 
									@name varchar(50), 
									@birth_date date,
									@rank int
									)
AS
BEGIN

	INSERT into movies.writer VALUES (@ssn, @name, @birth_date, @rank);
	 
END

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Users

--drop PROCEDURE movies.sp_AddUser
GO
CREATE PROCEDURE movies.sp_AddUser (
									@username varchar(20), 
									@name varchar(50),
									@bdate date, 
									@email varchar(50),
									@country varchar(20)
									)
AS
BEGIN

	INSERT into movies.users VALUES (@username, @name, @bdate, @email, @country);
	 
END

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Awards

-- drop procedure movies.sp_AddAward
GO
CREATE PROCEDURE movies.sp_AddAward (
									@year varchar(20), 
									@type varchar(50), 
									@designation varchar(500),
									@movie_id int
									)
AS
BEGIN

	INSERT into movies.award VALUES (@year, @type, @designation, @movie_id);
	 
END

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Review

--drop PROCEDURE movies.sp_AddReview
GO
CREATE PROCEDURE movies.sp_AddReview (
									@id int, 
									@rating int, 
									@review varchar(500),
									@date date,
									@movie_id int,
									@username varchar(20)
									)
AS
BEGIN

	INSERT into movies.review VALUES (@id, @rating, @review, @date, @movie_id, @username);
	 
END

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to search Actors

--drop PROCEDURE movies.sp_SearchActors
GO
CREATE PROCEDURE movies.sp_SearchActors (
									@name varchar(50) = null,
									@bdate date = null,
									@rank int = null
									)
AS
BEGIN

	declare @out table(ssn int, name varchar(50), bdate date, [rank] int, bio varchar(500));
	declare @tmp table(ssn int, name varchar(50), bdate date, [rank] int, bio varchar(500));

	insert into @out select * from movies.udf_GetActors();

	if not @name is null
		insert into @tmp select * from @out where name like '%'+@name+'%';
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @bdate is null
		insert into @out select * from @tmp where bdate=@bdate;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @rank is null
		insert into @tmp select * from @out where [rank] <= @rank;
	else
		insert into @tmp select * from @out;

	select * from @tmp;
	
END
go

-- exec movies.sp_SearchActors @rank=500
----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to search Users

--drop PROCEDURE movies.sp_SearchUsers
GO
CREATE PROCEDURE movies.sp_SearchUsers (
									@username varchar(20) = null,
									@name varchar(50) = null,
									@bdate date = null,
									@email varchar(50) = null,
									@country varchar(50) = null
									)
AS
BEGIN

	declare @out table(username varchar(20), name varchar(50), bdate date, email varchar(50), country varchar(50));
	declare @tmp table(username varchar(20), name varchar(50), bdate date, email varchar(50), country varchar(50));

	insert into @tmp select * from movies.udf_GetUsers();

	if not @username is null
		insert into @out select * from @tmp where username=@username;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @name is null
		insert into @tmp select * from @out where name like '%'+@name+'%';
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @bdate is null
		insert into @out select * from @tmp where bdate=@bdate;
	else
		insert into @out select * from @tmp;

	delete from @tmp;
	 
	if not @email is null
		insert into @tmp select * from @out where email=@email;
	else
		insert into @tmp select * from @out;

	delete from @out;

	if not @country is null
		insert into @out select * from @tmp where country=@country;
	else
		insert into @out select * from @tmp;

	select * from @out;
END
go

-- exec movies.sp_SearchUsers @email = 'test@ua.pt'

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to search Writers

--drop PROCEDURE movies.sp_SearchWriters
GO
CREATE PROCEDURE movies.sp_SearchWriters (
									@name varchar(50) = null,
									@bdate date = null,
									@rank int = null
									)
AS
BEGIN

	declare @out table(ssn int, name varchar(50), bdate date, [rank] int);
	declare @tmp table(ssn int, name varchar(50), bdate date, [rank] int);

	insert into @out select * from movies.udf_GetWriters();

	if not @name is null
		insert into @tmp select * from @out where name like '%'+@name+'%';
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @bdate is null
		insert into @out select * from @tmp where bdate=@bdate;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @rank is null
		insert into @tmp select * from @out where [rank] <= @rank;
	else
		insert into @tmp select * from @out;

	select * from @tmp;
	
END
go

----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to search Studios

--drop PROCEDURE movies.sp_SearchStudios
GO
CREATE PROCEDURE movies.sp_SearchStudios (
									@name varchar(50) = null,
									@location varchar(50) = null
									)
AS
BEGIN

	declare @out table(id int, name varchar(50), location varchar(50));
	declare @tmp table(id int, name varchar(50), location varchar(50));

	insert into @out select * from movies.udf_GetStudios();

	if not @name is null
		insert into @tmp select * from @out where name like '%'+@name+'%';
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @location is null
		insert into @out select * from @tmp where  [@tmp].location = @location;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	select * from @out;
	
END
go

-- exec movies.sp_SearchStudios @location = 'USA'