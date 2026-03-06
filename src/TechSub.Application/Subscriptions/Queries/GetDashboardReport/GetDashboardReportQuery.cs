using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Queries.GetDashboardReport;

public record GetDashboardReportQuery() : IRequest<Result<DashboardReportDto>>;