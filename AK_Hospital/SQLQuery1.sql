--drop 
--create 

create table Patient
(
	PatientId int primary key,
	FirstName varchar(30),
	LastName varchar(30),
	Age int,
	Description varchar(100),
	PhoneNumber varchar(20),
	Status varchar(30)
)

create table Staff
(
	StaffId int primary key,
	FirstName varchar(30),
	LastName varchar(30),
	Role varchar(30),
	Username varchar(30),
	Password varchar(30),
	Logged int
)
select * from Staff
create table Medication
(
	MedicationId int primary key,
	MedicationName varchar(40)
)

create table Prescription
(
	PrescriptionId int primary key,
	PatientId int,
	MedicationId int
)

-- insertS 2,'Adel','Abdul','Surgeon','Adel','1234'

alter table Staff 
add constraint check_Logged
Check (Logged in (0,1))

alter table Patient
add constraint check_status
Check (Status in ('Active','Inactive'))

alter table Staff
add constraint check_role
Check (Role in ('Surgeon','Nurse','Admin'))

alter table Prescription
add constraint fk_PatientId
foreign key (PatientId) references Patient(PatientId)

alter table Prescription
add constraint fk_MedicationId
foreign key (MedicationId) references Medication(MedicationId)
 

select * from Patient
select * from Staff
select * from Medication
select * from Prescription  


go


create or alter proc CheckLogin 
	@Username varchar(30),
	@Password varchar(30)
as
begin
	select StaffId, FirstName+' '+LastName as [Staff Name], Role from Staff
	where Username in (@Username) and Password in (@Password) 
end
go

create or alter proc Login
	@Username varchar(30)
as
begin
	update Staff
	set Logged = 1
	where Username = @Username

	update Staff
	set Logged = 0
	where Username not in (@Username)
end

go

create or alter proc logout
as
begin
	update Staff
	set Logged = 0
end

go









go  ------------------------------- PATIENT ---------------------------------------

create or alter proc insertP
	@PatientId int,
	@FirstName varchar(30),
	@LastName varchar(30),
	@Age int,
	@Description varchar(100),
	@PhoneNumber varchar(20),
	@Status varchar(30)
as
begin
	insert into Patient values
	(@PatientId,@FirstName,@LastName,@Age,@Description,@PhoneNumber,@Status)
end

go

create or alter proc updateP
	@PatientId int,
	@FirstName varchar(30),
	@LastName varchar(30),
	@Age int,
	@Description varchar(100),
	@PhoneNumber varchar(20),
	@Status varchar(30)
as
begin
	update Patient
	set FirstName = @FirstName,  LastName = @LastName, Age = @Age, Description = @Description, PhoneNumber = @PhoneNumber, Status = @Status
	where PatientId = @PatientId
end

go

create or alter proc setInactive
	@PatientId int,
	@Status varchar(30)
as
begin
	update Patient
	set Status = @Status
	where PatientId = @PatientId
end

go

create or alter proc getP
as
begin
	select * from Patient
end

go

create or alter proc getAP
as
begin
	select * from Patient where Status like 'Active'
end

go

create or alter proc getIP
as
begin
	select * from Patient where Status like 'Inactive'
end





go ------------------------------- STAFF ---------------------------------------

create or alter proc insertS
	@StaffId int,
	@FirstName varchar(30),
	@LastName varchar(30),
	@Role varchar(30),
	@Username varchar(30),
	@Password varchar(30)
as
begin
	insert into Staff values
	(@StaffId,@FirstName,@LastName,@Role,@Username,@Password,0)
end

go

create or alter proc updateS
	@StaffId int,
	@FirstName varchar(30),
	@LastName varchar(30),
	@Role varchar(30),
	@Username varchar(30),
	@Password varchar(30)
as
begin
	update Staff
	set FirstName = @FirstName, LastName = @LastName, Role = @Role, Username = @Username, Password = @Password
	where StaffId = @StaffId 
end

go

create or alter proc deleteS
	@StaffId int
as
begin
	delete from Staff
	where StaffId = @StaffId 
end

go

create or alter proc getS
as
begin
	select StaffId, FirstName+' '+LastName as [Staff Name], Role, Username from Staff
end

go

create or alter proc getAdmins
as
begin
	select StaffId, FirstName+' '+LastName as [Staff Name], Role, Username from Staff where Role like 'Admin'
end

go

create or alter proc getSurgeons
as
begin
	select StaffId, FirstName+' '+LastName as [Staff Name], Role, Username from Staff where Role like 'Surgeon'
end

go

create or alter proc getNurses
as
begin
	select StaffId, FirstName+' '+LastName as [Staff Name], Role, Username from Staff where Role like 'Nurse'
end

go








go  ------------------------------- MEDICATION ---------------------------------------

create or alter proc insertM
	@MedicationId int,
	@MedicationName varchar(40),
	@PrescribedFor varchar(100)
as
begin
	insert into Medication values
	(@MedicationId,@MedicationName,@PrescribedFor)
end

go

create or alter proc updateM
	@MedicationId int,
	@MedicationName varchar(40),
	@PrescribedFor varchar(100)
as
begin
	update Medication
	set MedicationName = @MedicationName, PrescribedFor = @PrescribedFor
	where MedicationId = @MedicationId 
end

go

create or alter proc deleteM
	@MedicationId int
as
begin
	delete from Medication
	where MedicationId = @MedicationId 
end

go

create or alter proc getM
as
begin
	select * from Medication
end

go







go  ------------------------------- PRESCRIPTION ---------------------------------------

create or alter proc insertPre
	@PrescriptionId int,
	@PatientId int,
	@MedicationId int
as
begin
	insert into Prescription values
	(@PrescriptionId,@PatientId,@MedicationId)
end

go

create or alter proc updatePre
	@PrescriptionId int,
	@PatientId int,
	@MedicationId int
as
begin
	update Prescription
	set PatientId = @PatientId, MedicationId = @MedicationId 
	where PrescriptionId = @PrescriptionId
end

go

create or alter proc deletePre
	@PrescriptionId int
as
begin
	delete from Prescription
	where PrescriptionId = @PrescriptionId
end

go

create or alter proc getPre
as
begin
	select Pre.PrescriptionId, Pa.FirstName + ' ' + Pa.LastName as [Patient Name], Med.MedicationName
	from Prescription as Pre
	inner join Patient as Pa
		on Pa.PatientId = Pre.PatientId 
	inner join Medication as Med
		on Med.MedicationId = Pre.MedicationId
end

go

create or alter proc getPatientFirstName 
	@PatientId int
as
begin
	select FirstName from Patient where PatientId = @PatientId
end

go

create or alter proc getPatientLastName 
	@PatientId int
as
begin
	select LastName from Patient where PatientId = @PatientId
end
	
go

create or alter proc getMedicationName
	@MedicationId int
as
begin
	select MedicationName from Medication where MedicationId = @MedicationId
end



go  ------------------------------------------------------------------------------------


