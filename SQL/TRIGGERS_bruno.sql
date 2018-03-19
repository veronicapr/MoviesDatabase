go
create trigger movies.t_deleteActor on movies.actor instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteAward on movies.award instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteDirector on movies.director instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteGenre on movies.genre instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteLocations on movies.locations instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteMove on movies.movie instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deletePerformed_by on movies.performed_by instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteRelease on movies.release instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteReview on movies.review instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteStudio on movies.studio instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteTrailer on movies.trailer instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteUsers on movies.users instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteWriter on movies.writer instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
go
create trigger t_deleteWritten_by on movies.written_by instead of delete
as
PRINT 'Delete Operation not permited!';
go
-----------------------------------------------------------------
