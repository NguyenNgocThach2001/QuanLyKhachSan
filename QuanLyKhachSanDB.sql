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
	room_id int identity(1,1) primary key,
	room_type_id int,
	room_status_id int,
	foreign key (room_status_id) references RoomStatus(room_status_id),
	foreign key (room_type_id) references RoomType(room_type_id),
)

Create Table Guest(
	guest_id int identity(1,1) primary key,
	full_name nvarchar(max),
	address nvarchar(max),
	email nvarchar(max),
	phone nvarchar(20),
	country nvarchar(20),
	note nvarchar(max),
)

Create Table Reservation(
	Reservation_id int identity(1,1) primary key,
	guest_id int,
	room_id int,
	reservation_date date,
	check_in_date date,
	check_out_date date,
	adult int,
	children int,
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

Create Table Account(
	UserName nvarchar(50) primary key,
	Password nvarchar(max),
)


Delete From Account
Where Account.UserName = N'Thach';

Insert into Account
Values (N'Thach',N'e99d28b464722f0282f1c1d099b236f6');

