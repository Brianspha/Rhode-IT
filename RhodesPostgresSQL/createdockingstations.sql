create table  IF NOT EXISTS DockingStations(
name name not null Primary key,
latitude varchar not null,
longitude varchar not null,
registrationhash char(66) not null

);