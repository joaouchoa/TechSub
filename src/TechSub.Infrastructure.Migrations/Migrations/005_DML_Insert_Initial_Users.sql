INSERT INTO "Users" 
    ("Name", "Email", "PasswordHash", "Role")
VALUES 
    (
        'joao', 
        'joao@gmail.com', 
        '$2a$11$9pH0cIDU1tF1NCrXPnSHsO3byWJ42ESXdMVZycUePc8EpBMknXmdG', 
        1 -- Role: Customer
    ),
    (
        'maria', 
        'maria@gmail.com', 
        '$2a$11$9pH0cIDU1tF1NCrXPnSHsO3byWJ42ESXdMVZycUePc8EpBMknXmdG', 
        2 -- Role: Admin
    );