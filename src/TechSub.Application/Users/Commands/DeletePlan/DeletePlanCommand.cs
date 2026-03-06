using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.DeletePlan;

public record DeletePlanCommand(int Id) : IRequest<Result<bool>>;