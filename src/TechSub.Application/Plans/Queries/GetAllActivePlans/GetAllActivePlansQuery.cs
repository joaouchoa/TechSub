using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Queries.GetAllActivePlans;

public record GetAllActivePlansQuery() : IRequest<Result<IEnumerable<PlanResponse>>>;

//public record GetAllActivePlansQuery() : IRequest<Result<IEnumerable<PlanResponse>>>;


