using System;
namespace TechSub.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = GetBrazilianTime();
        }

        public void UpdateTimestamp()
        {
            UpdatedAt = GetBrazilianTime();
        }

        protected DateTime GetBrazilianTime()
        {
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone);
        }
    }
}
