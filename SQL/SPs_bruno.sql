use p5g1;

CREATE TYPE movies.genrelist
AS TABLE
(
	genre varchar(20)
);

CREATE TYPE movies.actorlist
AS TABLE
(
	ssn int
);

CREATE TYPE movies.writerlist
AS TABLE
(
	ssn int
);

CREATE TYPE movies.locationslist
AS TABLE
(
	location varchar(50)
);

CREATE TYPE movies.userlist
AS TABLE
(
	username varchar(50)
);

--drop type movies.genrelist;
--drop type movies.actorlist;
--drop type movies.writerlist;
--drop type movies.locationslist;
--drop type movies.userlist;


----------------------------------------------------------------------------------------------------------------------------------------------
--drop procedure movies.sp_AddMovie;

GO
CREATE PROCEDURE movies.sp_AddMovie (
									@id int,
									@duration time, 
									@description varchar(500), 
									@age_restriction varchar(5), 
									@rating tinyint, 
									@studio_id int, 
									@director_ssn int, 
									@Genre AS movies.genrelist READONLY,
									@Actors AS movies.actorlist READONLY,
									@Writers AS movies.writerlist READONLY
									)
AS
BEGIN
	-- SET NOCOUNT ON; -- reduces msgs sent to users and slightly improves performance on bigger sp's

	INSERT into movies.movie VALUES (@id, @duration, @description, @age_restriction, @rating, @studio_id, @director_ssn);
	
	declare @tmp varchar(20);
	declare cg cursor for select * from @Genre;
	--
	open cg;
	fetch cg into @tmp;

	while @@FETCH_STATUS = 0
	begin
		insert into movies.genre values(@id, @tmp);
		fetch cg into @tmp;
	end
	close cg;
	--
	declare @ssn int;
	declare ca cursor for select * from @Actors;

	open ca;
	fetch ca into @ssn;

	while @@FETCH_STATUS = 0
	begin
		insert into movies.performed_by values(@id, @ssn);
		fetch ca into @ssn;
	end
	close ca;
	---
	declare cw cursor for select * from @Writers;

	open cw;
	fetch cw into @ssn;

	while @@FETCH_STATUS = 0
	begin
		insert into movies.written_by values(@id, @ssn);
		fetch cw into @ssn;
	end
	close cw;
	---
	return; 
END
GO

--declare @g movies.genrelist;
--insert into @g values ('AAA');
--insert into @g values ('BBB');
--insert into @g values ('CCC');

/*exec movies.sp_AddMovie 
									@id=20,
									@duration='1:2:3', 
									@description='fdsvfdbvfdhf', 
									@age_restriction='PG', 
									@rating=100, 
									@studio_id=1, 
									@director_ssn=1, 
									@Genre=@g
									;*/



---------------------------------------------------------------------------------------------------------
-- Create SP to search movies
go
CREATE procedure movies.sp_searchMovies (
										@Title varchar(50) = null, 
										@Age_restriction varchar(5) = null,
										@Country varchar(50) = null, 
										@Studio_id int = null, 
										@Year int = null, 
										@Actors AS movies.actorlist READONLY
											)
AS
BEGIN
	declare @tmp table(id int, title varchar(50), country varchar(50), [date] date, duration time, age_restriction varchar(5), rating int, studio varchar(50), [description] varchar(500), studio_id int);
	declare @out table(id int, title varchar(50), country varchar(50), [date] date, duration time, age_restriction varchar(5), rating int, studio varchar(50), [description] varchar(500), studio_id int);
	
	if exists(select * from @Actors)
		insert into @tmp select id, title, country, [date], duration, age_restriction, rating, studio, [description], studio_id from @Actors join movies.performed_by on ssn=actor_ssn join movies.udf_GetMovies () on movie_id=id;
	else
		insert into @tmp select * from movies.udf_GetMovies();

	if not @Title is null
		insert into @out select * from @tmp where title like '%'+@Title+'%';
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @Age_restriction is null
		insert into @tmp select * from @out where age_restriction=@Age_restriction;
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @Country is null
		insert into @out select * from @tmp where country=@Country;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @Studio_id is null
		insert into @tmp select * from @out where studio_id=@Studio_id;
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @Year is null
		insert into @out select * from @tmp where YEAR([date])=@Year;
	else
		insert into @out select * from @tmp;

	select * from @out;


END
go

--drop procedure movies.sp_searchMovies;

--declare @g movies.actorlist;
--insert into @g values (1);

--exec movies.sp_searchMovies  @Studio_id=1;

-------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Actors
go
create procedure movies.sp_AddActor (
									@ssn int,
									@name varchar(50),
									@bdate date,
									@rank int,
									@bio varchar(600)
									)
as
begin

insert into movies.actor values (@ssn, @name, @bdate, @rank, @bio);

end
go

--exec movies.sp_AddActor @ssn = 100, @name = 'aaaaaa', @bdate = '1/2/3', @rank = 100, @bio = 'dasuhdufashduhsfu';

--------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Studios
go
create procedure movies.sp_AddStudio (
									@id int,
									@name varchar(50),
									@locations AS movies.locationslist READONLY
									)
as
begin

	insert into movies.studio values (@id, @name);

	declare @tmp varchar(50);
	declare cs cursor for select * from @locations;
	--
	open cs;
	fetch cs into @tmp;

	while @@FETCH_STATUS = 0
	begin
		insert into movies.locations values(@id, @tmp);
		fetch cs into @tmp;
	end
	close cs;

end
go

--drop procedure movies.sp_AddStudio;

--declare @g movies.locationslist;
--insert into @g values ('AAA');

--exec movies.sp_AddStudio @id = 100, @name = 'aaaaaa', @locations=@g;
----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Releases
go
create procedure movies.sp_AddRelease (
									@id int,
									@title varchar(50),
									@date date,
									@country varchar(50),
									@cover varchar(100) = null,
									@movieID int
									)
as
begin

	insert into movies.release values (@id, @title, @date, @country, @cover, @movieID);

end
go
----------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to insert Trailers

go
create procedure movies.sp_AddTrailer (
									@id int,
									@title varchar(50),
									@date date,
									@language varchar(50),
									@movieID int,
									@duration time
									)
as
begin

	insert into movies.trailer values (@id, @title, @date, @duration, @language, @movieID);

end
go

--drop procedure movies.sp_AddTrailer;

--exec movies.sp_AddTrailer 10,'AAAA', '1/1/2001', 'English', 5, '00:02:30';
--------------------------------------------------------------------------------------------------------------------------------------------------------------

-- Create SP to search trailers
go
CREATE procedure movies.sp_searchTrailers (
										@Title varchar(50) = null, 
										@MovieID varchar(5) = null,
										@Language varchar(50) = null, 
										@Year int = null
											)
AS
BEGIN
	declare @tmp table(id int, title varchar(50), [date] date, duration time, [language] varchar(50), movie_id int);
	declare @out table(id int, title varchar(50), [date] date, duration time, [language] varchar(50), movie_id int);
	
	insert into @tmp select * from movies.trailer;

	if not @Title is null
		insert into @out select * from @tmp where title like '%'+@Title+'%';
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @MovieID is null
		insert into @tmp select * from @out where movie_id=@MovieID;
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @Language is null
		insert into @out select * from @tmp where [language]=@Language;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @Year is null
		insert into @tmp select * from @out where YEAR([date])=@Year;
	else
		insert into @tmp select * from @out;

	select * from @tmp;

END
go

-- drop procedure movies.sp_searchTrailers

--------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Create SP to search reviews
go
CREATE procedure movies.sp_searchReviews (
											@MovieID int = null, 
											@Rating int = null, 
											@Year int = null, 
											@Users movies.userlist READONLY
										)
AS
BEGIN
	declare @tmp table (title varchar(50), username varchar(50), rating int, review varchar(500), [date] date, id int, movie_id int);
	declare @out table (title varchar(50), username varchar(50), rating int, review varchar(500), [date] date, id int, movie_id int);

	
	if exists(select * from @Users)
		insert into @tmp select title, movies.udf_GetReviews.username, rating, review, [date], id, movie_id from movies.udf_GetReviews() join @Users on movies.udf_GetReviews.username = [@Users].username;
	else
		insert into @tmp select * from movies.udf_GetReviews();

	if not @MovieID is null
		insert into @out select * from @tmp where movie_id=@MovieID;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @Rating is null
		insert into @tmp select * from @out where rating >= @Rating;
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @Year is null
		insert into @out select * from @tmp where YEAR([date])=@Year;
	else
		insert into @out select * from @tmp;

	select * from @out;

END
go

-- drop procedure movies.sp_searchReviews;
-- exec movies.sp_searchReviews

--------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Create SP to search awards
go
CREATE procedure movies.sp_searchAwards (
										@Type varchar(50) = null, 
										@MovieID varchar(50) = null, 
										@Year int = null
											)
AS
BEGIN
	declare @tmp table([year] int, [type] varchar(50), designation varchar(50), movie_id int);
	declare @out table([year] int, [type] varchar(50), designation varchar(50), movie_id int);
	
	insert into @tmp select * from movies.award;

	if not @Type is null
		insert into @out select * from @tmp where [type]=@Type;
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @MovieID is null
		insert into @tmp select * from @out where movie_id=@MovieID;
	else
		insert into @tmp select * from @out;

	delete from @out

	if not @Year is null
		insert into @out select * from @tmp where [year]=@Year;
	else
		insert into @out select * from @tmp;

	select * from @out;

END
go

--------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Create SP to add Directors
GO
CREATE PROCEDURE movies.sp_AddDirector (
									@ssn int, 
									@name varchar(50), 
									@birth_date date,
									@rank int
									)
AS
BEGIN

	INSERT into movies.director VALUES (@ssn, @name, @birth_date, @rank);
	 
END
GO

-- exec movies.sp_AddDirector @ssn=100, @name='AA', @birth_date='02-03-1994', @rank=5000

--------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Create SP to search Directors

go
CREATE procedure movies.sp_searchDirectors (
										@name varchar(50) = null, 
										@movieID varchar(50) = null, 
										@bdate date = null,
										@rank int = null
											)
AS
BEGIN
	declare @tmp table (ssn int, name varchar(50), bdate date, [rank] int);
	declare @out table (ssn int, name varchar(50), bdate date, [rank] int);
	
	insert into @tmp select * from movies.director;

	if not @name is null
		insert into @out select * from @tmp where name like '%' + @name + '%';
	else
		insert into @out select * from @tmp;

	delete from @tmp;

	if not @MovieID is null
		insert into @tmp select ssn, name, bdate, [rank] from @out join movies.movie on ssn=director_ssn where id=@MovieID;
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
-- drop procedure movies.sp_searchDirectors
-- exec movies.sp_searchDirectors @name='Frank'