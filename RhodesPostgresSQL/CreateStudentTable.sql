CREATE TABLE  IF NOT EXISTS student_staff (
credits INT NOT NULL,
password VARCHAR(32) NOT NULL,
student_staff_id VARCHAR(8) PRIMARY KEY NOT NULL,
eth_address character(42) NOT NULL,
loginhash character(66) ,
lastlogin timestamp without time zone DEFAULT now()
);


INSERT INTO student_staff (credits,password,student_staff_id,eth_address,loginhash) VALUES
(0,'Spha','g14m1190','0x68362a5502f758ca2bf75c22f2e93737e78eeaee',''),
(0,'Password','g25n4895','0xa47ae79a036c5461591dea825cb8b8761cbbba2d',''),
(0,'Password','g25d9880','0x5879393414b0df83dfaf16c63c0e03f14ea20e77',''),
(0,'Password','g29d6356','0xcba87494a176f5c6142b52197d5af2c69bf8cbda',''),
(0,'Password','g47y7051','0x39dd2a984fbbf9aaebf83003e4318e177fd68d80',''),
(0,'Password','g13b4512','0x9ff99961f3ddbfb1c52f3a3d780130e33c00094c',''),
(0,'Password','g14o7715','0xb6a980530e653474b64a1f27fc6dea151ca53123',''),
(0,'Password','g11a3007','0x65bf71631f431123ce1c1adf9abbd142aeb894d7',''),
(0,'Password','g26v4819','0xe8bfeb365b0d7cd4e113cb0c39d3e48de31038ce',''),
(0,'Password','g23l7881','0x61811a280b0d65153321ad0e0f2d72409878c431',''),
(0,'Password','g11u8767','0xedaeadb3ddb2f82667855775d9d842fd78ddad8d','') ON CONFLICT(student_staff_id) DO NOTHING;