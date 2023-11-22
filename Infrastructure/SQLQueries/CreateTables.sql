
-- Table: Gender
CREATE TABLE Gender (
  gender_id INT PRIMARY KEY,
  gender_name VARCHAR(50)
);

-- Table: ClothingType
CREATE TABLE ClothingType (
  type_id INT PRIMARY KEY,
  type_name VARCHAR(50)
);

-- Table: Fabric
CREATE TABLE Fabric (
  fabric_id INT PRIMARY KEY,
  fabric_name VARCHAR(50)
);



-- Table: Clothing
CREATE TABLE Clothing (
  clothing_id INT PRIMARY KEY,
  clothing_name VARCHAR(50),
  price DECIMAL(10, 2),
  gender_id INT,
  type_id INT,
  fabric_id INT,
  FOREIGN KEY (gender_id) REFERENCES Gender(gender_id) ON DELETE CASCADE,
  FOREIGN KEY (type_id) REFERENCES ClothingType(type_id) ON DELETE CASCADE,
  FOREIGN KEY (fabric_id) REFERENCES Fabric(fabric_id) ON DELETE CASCADE
);

Create Table Products(
    Id int,
    Name VARCHAR(50),
    Price DECIMAL
)



CREATE TABLE Sales (
  sale_id INT IDENTITY(30000, 1) PRIMARY KEY,
  clothing_id INT,
  gender_id INT,
  type_id INT,
  fabric_id INT,
  description VARCHAR(255),
  actual_price DECIMAL(10, 2),
  sale_price DECIMAL(10, 2),
  date DATE,
  time TIME,
  FOREIGN KEY (clothing_id) REFERENCES Clothing(clothing_id) ON DELETE NO ACTION,
  FOREIGN KEY (gender_id) REFERENCES Gender(gender_id) ON DELETE NO ACTION,
  FOREIGN KEY (type_id) REFERENCES ClothingType(type_id) ON DELETE NO ACTION,
  FOREIGN KEY (fabric_id) REFERENCES Fabric(fabric_id) ON DELETE NO ACTION
);
