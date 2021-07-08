Create Database QuanLyKhachSan;
use QuanLyKhachSan;

Create Table RoomStatus(
	room_status_id int identity(1,1) primary key,
	room_status_name nvarchar(max),
)

Create Table RoomType(
	room_type_id int identity(1,1) primary key,
	room_type_name nvarchar(max),
	price int,
)

Create Table Room(
	room_id int primary key,
	room_type_id int,
	room_status_id int,
	foreign key (room_status_id) references RoomStatus(room_status_id),
	foreign key (room_type_id) references RoomType(room_type_id),
)

ALTER TABLE room
ADD room_name nvarchar(max);

Create Table Guest(
	guest_id int identity(1,1) primary key,
	full_name nvarchar(max),
	address nvarchar(max),
	email nvarchar(max),
	phone nvarchar(20),
	country nvarchar(20),
	note nvarchar(max),
)

ALTER TABLE Guest
ADD CMND nvarchar(max);

Create Table Reservation(
	Reservation_id int identity(1,1) primary key,
	guest_id int,
	room_id int,
	reservation_date date,
	check_in_date date,
	check_out_date date,
	adult int,
	children int,
	amount float,
	foreign key (guest_id) references Guest(guest_id),
	foreign key (room_id) references Room(room_id), 
)

Create Table Payment(
	payment_id int identity(1,1),
	guest_id int, 
	reservation_id int,
	fullname nvarchar(max),
	Primary Key(payment_id, guest_id),
	foreign key (reservation_id) references Reservation(reservation_id),
)

Create Table PaymentService(
	PaymentService_ID int identity(1,1) primary key,
)

ALTER Table PaymentService
ADD Useage int;

ALTER Table PaymentService
ADD Reservation_id int;

ALTER Table PaymentService
ADD foreign Key (Reservation_id) REFERENCES Reservation(Reservation_id);

ALTER Table PaymentService
ADD Service_ID int;

ALTER Table PaymentService
ADD foreign Key (Service_ID) REFERENCES Service(Service_ID);

Create Table RoomService(
	Room_Service UNIQUEIDENTIFIER primary key default NEWID(),
	Room_Name nvarchar(max),
	Unit nvarchar(max),	
	Unit_Price int,
	Useage int,
)

ALTER Table Reservation
ADD Room_Service UNIQUEIDENTIFIER;

ALTER Table Reservation
ADD paid int;

ALTER Table Reservation
Add foreign key (Room_Service) REFERENCES RoomService(Room_Service);


Create Table Account(
	UserName nvarchar(50) primary key,
	Password nvarchar(max),
)

ALTER TABLE Account
ADD FullName nvarchar(max);

Delete From Account
Where Account.UserName = N'Thach';

Insert into Account
Values (N'Thach',N'e99d28b464722f0282f1c1d099b236f6');

Create Table Service(
	Service_ID int identity(1,1) primary key,
	Service_Name nvarchar(max),
)

ALTER TABLE Service
ADD unitPrice int;

ALTER TABLE Service
ADD unit nvarchar(max);

Create Table Department(
	Department_ID nvarchar(10) primary key,
	Department_Name nvarchar(max),
	Department_Head_id int,
)


Create Table Staff(
	Staff_ID int identity(1,1) primary key,
	Staff_Name nvarchar(max),
	Staff_Sex nvarchar(10),
	Staff_Phone nvarchar(20),
	Staff_Position_ID nvarchar(10),
	Staff_Payrange_ID nvarchar(10),
	Staff_Coefficients_ID nvarchar(10),
	Staff_Department_ID nvarchar(10),
	Staff_Contract_Date DateTime,
	Expiry_Date DateTime,
)

Create Table Contract(
	Contract_ID int identity(1,1) primary key,
	Staff_ID int,
	Context nvarchar(max),
	foreign key(Staff_ID) references Staff(Staff_ID),
)


Create Table Position(
	Position_ID nvarchar(10) primary key,
	Position_Name nvarchar(max),
)

Create Table Coefficients(
	Coefficients_ID nvarchar(10) primary key,
	Coefficients_Num float,
)

Create Table Payrange(
	Payrange_ID nvarchar(10) primary key,
	Payrange_Num float,
)


ALTER TABLE Staff
ADD Birthday datetime;
ALTER TABLE Staff
ADD Note nvarchar(max);
ALTER TABLE Staff
ADD FOREIGN KEY (Staff_Department_ID) REFERENCES Department(Department_ID);
ALTER TABLE Staff
ADD FOREIGN KEY (Staff_Coefficients_ID) references Coefficients(Coefficients_ID);
ALTER TABLE Staff
ADD FOREIGN KEY (Staff_Payrange_ID) references Payrange(Payrange_ID);
ALTER TABLE Staff
ADD FOREIGN KEY (Staff_Position_ID) references Position(Position_ID);
ALTER TABLE Contract
ADD FOREIGN  key(Staff_ID) references Staff(Staff_ID);
ALTER TABLE Department
ADD FOREIGN KEY (Department_Head_id) REFERENCES Staff(Staff_ID);
ALTER TABLE Department
ADD Deputy nvarchar(max);
ALTER TABLE Reservation 
ADD FOREIGN KEY (Staff_ID) REFERENCES Staff(Staff_ID);
ALTER TABLE Account
ADD Staff_ID int;
ALTER TABLE Account 
ADD FOREIGN KEY (Staff_ID) REFERENCES Staff(Staff_ID);	