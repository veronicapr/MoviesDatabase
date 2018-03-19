
--Create index on release table, columns: title, date, country
CREATE INDEX releaseDate ON movies.release (date);

CREATE INDEX releaseTitle ON movies.release (title);

CREATE INDEX releaseCountry ON movies.release (country);
 
-- Nºao criamos mais indices pois não sabemos quais as pesquisas mais executadas quando em produção,
-- uma vez que os indices introduzem um overhead na base de dados decidimos apenas introduzir estes por agora
-- que foram os mais pesquisados por nos.