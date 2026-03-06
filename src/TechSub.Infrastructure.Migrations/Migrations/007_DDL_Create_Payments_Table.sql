CREATE TABLE "Payments" (
    "Id" SERIAL PRIMARY KEY,
    "SubscriptionId" INTEGER NOT NULL,
    "Amount" NUMERIC(18,2) NOT NULL,
    "Status" INTEGER NOT NULL,
    "PaymentDate" TIMESTAMP NOT NULL,
    "ExternalTransactionId" VARCHAR(255) NULL,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP NULL,
    
    CONSTRAINT "FK_Payments_Subscriptions" FOREIGN KEY ("SubscriptionId") REFERENCES "Subscriptions" ("Id")
);