using MediatR;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Queries.GetAllActivePlans;

public class GetAllActivePlansQueryHandler : IRequestHandler<GetAllActivePlansQuery, Result<IEnumerable<PlanResponse>>>
{
    private readonly IPlanRepository _planRepository;

    public GetAllActivePlansQueryHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<Result<IEnumerable<PlanResponse>>> Handle(GetAllActivePlansQuery request, CancellationToken cancellationToken)
    {
        
        var plans = await _planRepository.GetAllActiveAsync(cancellationToken);

        // 2. Mapeia a Entidade de Domínio para o DTO de Resposta
        var response = plans.Select(p => new PlanResponse(
            p.Id,
            p.Name,
            p.MonthlyPrice,
            p.AnnualPrice,
            p.IsTrialEligible,
            p.Category.ToString() // O C# converte o Enum automaticamente para o nome dele!
        ));

        // 3. Retorna sucesso com a lista
        return Result<IEnumerable<PlanResponse>>.Sucess(response);
    }
}