CREATE TABLE "Subscriptions" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INTEGER NOT NULL,
    "PlanId" INTEGER NOT NULL,
    "Cycle" INTEGER NOT NULL,
    "Status" INTEGER NOT NULL,
    "TrialEndDate" TIMESTAMP NULL,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP NULL,
    
    CONSTRAINT "FK_Subscriptions_Users" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id"),
    CONSTRAINT "FK_Subscriptions_Plans" FOREIGN KEY ("PlanId") REFERENCES "Plans" ("Id")
);