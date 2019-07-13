Create table if not exists blockdata (
id BIGSERIAL PRIMARY KEY,
blocknumber Integer not null,hash char(66) UNIQUE not null, 
datesubmited varchar(50) not null,miner char(66) not null,
transactioncount Integer not null,gasUsed Integer not null,
gasLimit Integer not null,diffuclty Integer not null,
totaldifficulty Integer not null,blocksize Integer not null, 
parenthash char(66) not null,
transactionroot char(66) not null,
sha3uncles char(66) not null, nonce Integer not null);