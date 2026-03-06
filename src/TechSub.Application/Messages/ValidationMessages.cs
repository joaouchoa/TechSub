namespace TechSub.Application.Users.Messages;

public static class ValidationMessages
{
    // Mensagens de Negócio
    public const string ERRO001_EmailAlreadyInUse = "ERRO001 - Este e-mail já está em uso.";
    //public const string ERRO002_UserNotFound = "ERRO002 - Usuário não encontrado.";
    public const string ERRO003_InvalidCredentials = "ERRO003 - E-mail ou senha inválidos.";

    // Mensagens de Validação - Nome
    public const string ERRO004_NameRequired = "ERRO004 - O nome é obrigatório.";
    public const string ERRO005_NameMaxLength = "ERRO005 - O nome deve ter no máximo 100 caracteres.";

    // Mensagens de Validação - E-mail
    public const string ERRO006_EmailRequired = "ERRO006 - O e-mail é obrigatório.";
    public const string ERRO007_EmailInvalidFormat = "ERRO007 - Formato de e-mail inválido.";

    // Mensagens de Validação - Senha
    public const string ERRO008_PasswordRequired = "ERRO008 - A senha é obrigatória.";
    public const string ERRO009_PasswordMinLength = "ERRO009 - A senha deve ter no mínimo 6 caracteres.";
    public const string ERRO010_PasswordRequiresUppercase = "ERRO010 - A senha deve conter pelo menos uma letra maiúscula.";
    public const string ERRO011_PasswordRequiresLowercase = "ERRO011 - A senha deve conter pelo menos uma letra minúscula.";
    public const string ERRO012_PasswordRequiresNumber = "ERRO012 - A senha deve conter pelo menos um número.";

    public const string ERRO013_NameRequired = "ERRO013 - O nome do plano é obrigatório.";
    public const string ERRO014_InvalidMonthlyPrice = "ERRO014 - O preço mensal deve ser maior ou igual a zero.";
    public const string ERRO015_InvalidAnnualPrice = "ERRO015 - O preço anual deve ser maior ou igual a zero.";
    public const string ERRO016_InvalidCategory = "ERRO016 - Categoria de plano inválida.";

    public const string ERRO017_FreePlanCannotHaveTrial = "ERRO017 - Um plano gratuito (Free) não pode ser elegível para Trial.";

    public const string ERRO018_PlanNotFound = "ERRO018 - O plano selecionado não existe ou está inativo.";
    public const string ERRO019_UserAlreadySubscribed = "ERRO019 - O usuário já possui uma assinatura ativa ou em período de testes.";
    public const string ERRO020_SubscriptionNotFound = "ERRO020 - Você não possui nenhuma assinatura para cancelar.";
    public const string ERRO021_SubscriptionAlreadyCanceled = "ERRO021 - Sua assinatura já se encontra cancelada.";
    public const string ERRO022_TrialOnlyForNewSubscribers = "ERRO022 - O período de testes está disponível apenas para novos assinantes.";
    public const string ERRO023_PlanNotEligibleForTrial = "ERRO023 - Este plano não é elegível para período de testes gratuito.";
    public const string ERRO024_SubscriptionNotExists = "ERRO024 - Assinatura não encontrada.";
    public const string ERRO025_NotPossibleToChargeCancelledSubscription = "ERRO025 - Não é possível cobrar uma assinatura cancelada.";
    public const string ERRO026_ThePlanIsNoLongerExists = "ERRO026 - O plano selecionado não existe mais.";
}