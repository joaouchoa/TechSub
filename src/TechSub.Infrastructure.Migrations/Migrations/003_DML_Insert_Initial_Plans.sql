-- Inserindo os 3 planos iniciais do sistema
INSERT INTO "Plans" 
    ("Name", "MonthlyPrice", "AnnualPrice", "IsTrialEligible", "Category")
VALUES 
    -- 1. Plano Free: R$ 0, sem Trial (Regra de Domínio), Categoria 1
    ('Plano Free', 0.00, 0.00, false, 1),
    
    -- 2. Plano Basic: R$ 29,90/mês, com 7/14 dias de Trial, Categoria 2
    ('Plano Basic', 29.90, 299.00, true, 2),
    
    -- 3. Plano Pro: R$ 99,90/mês, com Trial, Categoria 3
    ('Plano Pro', 99.90, 999.00, true, 3);