
DROP TABLE IF EXISTS dbo.EventStore;
DROP SCHEMA IF EXISTS dbo; --cannot drop dbo when there is table connecting to it.

CREATE OR REPLACE FUNCTION THE_TIME() returns TIMESTAMPTZ AS
$$ --start function
	SELECT NOW() AS RESULT;
$$ LANGUAGE SQL;

CREATE extension if not exists "uuid-ossp";

CREATE SCHEMA dbo;

CREATE TABLE dbo.EventStore(
	--id serial primary key : serial mean identity(1,1)
	Id text  PRIMARY KEY  default uuid_generate_v4()::text , 
	AggId varchar NULL, --varchar mean nvachar(max), unicode is default
	CreatedDate timestamptz default THE_TIME(),
	IsDeleted boolean NOT NULL,
	JsonData varchar NULL,
	ObjType varchar NULL,
	Timestamp timestamp NULL,
	Type int NOT NULL,
	UpdatedDate timestamptz default THE_TIME(),
	search_field tsvector null
);
--alter code
alter table dbo.EventStore
	alter Id type text;

--query
select random()::text; --::text means cast


--trigger
DROP TRIGGER  IF EXISTS event_store_update ON dbo.EventStore 

create trigger event_store_update
before insert or update on dbo.EventStore
for each row execute procedure
tsvector_update_trigger(search_field,'pg_catalog.english',JsonData,ObjType);

--select with vector
--select * from dbo.EventStore
--where search_field @@ to_tsquery('xyz') --some pattern : 'rob & ! conery' / 'rob & con:*' / 
--add special character
--select $$hel0"'''$$

