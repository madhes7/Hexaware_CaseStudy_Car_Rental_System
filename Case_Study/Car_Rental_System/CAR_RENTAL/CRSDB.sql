
--CASE STUDY--

create database CRSDB;

use CRSDB;

-- Vehicle table--
create table Vehicle(
 vehicleID varchar(20) Primary Key ,
 make varchar(20) ,
 model varchar(15),
 [year]  INT CHECK ([year] BETWEEN 1900 AND YEAR(GETDATE())),
 dailyRate Money Not Null,
 [status] varchar(15) CHECK ([status] in('available', 'notAvailable')) Not Null,
 passengerCapacity Int Not Null,
 engineCapacity Int );

 --Customer Table--

 create table Customer(
 customerID int Primary Key Identity(1,1),
firstName varchar(20) Not Null,
lastName varchar(20) ,
email varchar(60) Unique Not Null,
phoneNumber varchar(15) Unique Not Null);

-- Lease Table--

create table Lease(
  leaseID Int Primary Key Identity(1,1),
  vehicleID varchar(20) Not Null,
  customerID Int Not Null,
  startDate date Not Null,
  endDate date  Not Null,
  [type] varchar(15) check([type] In( 'DailyLease','MonthlyLease'))  Constraint FK_Lease_Vechical Foreign Key (vehicleId) References Vehicle(vehicleId) On Delete CasCade On Update CasCade,  Constraint FK_Lease_Customer Foreign Key (CustomerId) References Customer(CustomerId) On Delete CasCade On Update CasCade,  constraint Day_Check check(enddate>startdate)   );  --Payment Table--  create table Payment(  paymentID Int Primary Key Identity(1,1),
  leaseID Int not Null,
  paymentDate Date Not Null,
  amount Money check(amount>0),  Constraint FK_Payment_lease Foreign Key (leaseID) References lease(leaseID) On Delete CasCade On Update CasCade);  -- Insert values into Vehicle table
INSERT INTO Vehicle (vehicleID, make, model, [year], dailyRate, [status], passengerCapacity, engineCapacity)
VALUES 
('V001', 'Toyota', 'Corolla', 2020, 50.00, 'available', 5, 1600),
('V002', 'Honda', 'Civic', 2021, 60.00, 'available', 5, 1800),
('V003', 'Ford', 'Focus', 2019, 45.00, 'notAvailable', 5, 1700);

-- Insert values into Customer table
INSERT INTO Customer (firstName, lastName, email, phoneNumber)
VALUES 
('John', 'Doe', 'john.doe@example.com', '1234567890'),
('Jane', 'Smith', 'jane.smith@example.com', '0987654321'),
('Alice', 'Johnson', 'alice.johnson@example.com', '1122334455');

-- Insert values into Lease table
INSERT INTO Lease (vehicleID, customerID, startDate, endDate, [type])
VALUES 
('V001', 1, '2024-01-01', '2024-01-10', 'DailyLease'),
('V002', 2, '2024-02-01', '2024-02-28', 'MonthlyLease'),
('V003', 3, '2024-03-01', '2024-03-10', 'DailyLease');

-- Insert values into Payment table
INSERT INTO Payment (leaseID, paymentDate, amount)
VALUES 
(1, '2024-01-01', 500.00),
(2, '2024-02-01', 1200.00),
(3, '2024-03-01', 450.00);
select * from Vehicle;select * from Customer;select * from Lease;select * from Payment;