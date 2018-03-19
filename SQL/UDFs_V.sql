use p5g1

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- drop FUNCTION movies.udf_GetActors
go
CREATE FUNCTION movies.udf_GetActors () RETURNS table
AS
	RETURN 
	(
		SELECT *
		FROM movies.actor 
	);
go
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- drop FUNCTION movies.udf_GetWriters
go
CREATE FUNCTION movies.udf_GetWriters () RETURNS table
AS
	RETURN 
	(
		SELECT *
		FROM movies.writer
	);
go

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- drop FUNCTION movies.udf_GetUsers
go
CREATE FUNCTION movies.udf_GetUsers () RETURNS table
AS
	RETURN 
	(
		SELECT users.username, name, bdate, email, country
		FROM movies.users
	);
go

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- drop FUNCTION movies.udf_GetUsers
go
CREATE FUNCTION movies.udf_GetStudios () RETURNS table
AS
	RETURN 
	(
		SELECT studio.id, name, location
		FROM movies.studio join movies.locations on movies.studio.id=movies.locations.id
	);
go

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- drop FUNCTION movies.udf_location 
go
CREATE FUNCTION movies.udf_location () RETURNS table
AS
	RETURN 
	(
		SELECT location
		FROM movies.locations
	);
go
