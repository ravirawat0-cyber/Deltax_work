CREATE DATABASE Ecommerce;
Use Ecommerce;

CREATE TABLE Customers (
  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  Name VARCHAR(255),
  Email VARCHAR(MAX),
  Password VARCHAR(MAX),
  Phone VARCHAR(255)
);

CREATE TABLE Products (
  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  Name VARCHAR(255),
  Description VARCHAR(MAX),
  Price DECIMAL(10, 2),
  ImageUrl VARCHAR(MAX),
  CategoryID INT NOT NULL
);

CREATE TABLE Categories (
  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  Name VARCHAR(255)
);

CREATE TABLE Orders (
  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  CustomerID INT NOT NULL,
  OrderDate DATE,
  TotalAmount DECIMAL(10, 2),
);

CREATE TABLE Order_Items (
  Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  OrderID INT NOT NULL,
  ProductID INT NOT NULL,
  Quantity INT,
);

ALTER TABLE Products
    ADD CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(Id);

ALTER TABLE Orders
    ADD CONSTRAINT FK_Orders_Customer FOREIGN KEY (CustomerID) REFERENCES Customers(Id);

ALTER TABLE Order_Items
	ADD 
		CONSTRAINT FK_Order_Items_Order FOREIGN KEY (OrderID) REFERENCES Orders(Id),
		CONSTRAINT FK_Order_Items_Product FOREIGN KEY (ProductID) REFERENCES Products(Id);


INSERT INTO Customers (Name, Email, Password, Phone)
VALUES
  ('Ravi Rawat', 'email', 'password123', '999999999'),
  ('Rohan', 'email', 'password456', '999999999'),
  ('Mike ', 'email', 'password789', '99999999');

INSERT INTO Categories (Name)
VALUES
  ('Electronics'),
  ('Clothing'),
  ('Home & Kitchen');

INSERT INTO Products (Name, Description, Price, ImageUrl, CategoryID)
VALUES
  ('iPhone 12', 'Apple iPhone 12 64GB Blue', 999.99, 'URL', 1),
  ('Samsung Galaxy S21', 'Samsung Galaxy S21 128GB Phantom Gray', 899.99, 'URL' ,1),
  ('Nike Air Max', 'Men Nike Air Max Shoes Black/White Size 10', 129.99, 'URL',2),
  ('KitchenAid Stand Mixer', 'Kitchen Mixer Silver', 299.99, 'URL',3);


INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
VALUES
  ( 1, '2023-05-01', 999.99),
  ( 1, '2023-05-15', 129.99),
  ( 2, '2023-05-10', 299.99);


INSERT INTO Order_Items (OrderID, ProductID, Quantity)
VALUES
  ( 1, 1, 1),
  ( 2, 3, 2),
  ( 3, 4, 1);
