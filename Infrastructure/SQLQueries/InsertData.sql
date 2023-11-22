-- Insert values into Gender table
INSERT INTO Gender (gender_id, gender_name)
VALUES (1, 'Ladies'),
       (2, 'Gents');

-- Insert values into ClothingType table
INSERT INTO ClothingType (type_id, type_name)
VALUES (1, '2 piece lawn'),
       (2, '3 piece lawn'),
       (3, 'Cotton'),
       (4, 'Silk'),
       (5, 'Organza'),
       (6, 'Linen'),
       (7, 'Khaddar'),
       (8, 'Washingwear'),
       (9, 'Karandee');

-- Insert values into Fabric table
INSERT INTO Fabric (fabric_id, fabric_name)
VALUES (1, 'Cotton'),
       (2, 'Khaddar'),
       (3, 'Karandee'),
       (4, 'Silk'),
       (5, 'Organza'),
       (6, 'Linen'),
       (7, 'Washingwear'),
       (8, 'Lawn');

-- Insert values into Clothing table
INSERT INTO Clothing (clothing_id, clothing_name, price, gender_id, type_id, fabric_id)
VALUES (1, '2 piece lawn', 50.00, 1, 1, 8),
       (2, '3 piece lawn', 75.00, 1, 2, 8),
       (3, 'Cotton', 30.00, 1, 3, 1),
       (4, 'Silk', 100.00, 1, 4, 4),
       (5, 'Organza', 80.00, 1, 5, 5),
       (6, 'Linen', 40.00, 1, 6, 6),
       (7, 'Khaddar', 35.00, 1, 7, 2),
       (8, 'Cotton', 45.00, 2, 3, 1),
       (9, 'Washingwear', 55.00, 2, 8, 7),
       (10, 'Khaddar', 40.00, 2, 7, 2),
       (11, 'Karandee', 65.00, 2, 9, 3);
