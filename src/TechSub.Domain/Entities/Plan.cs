namespace TechSub.Domain.Entities;

public class Plan : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    public Plan(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }

    public void UpdateInfo(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
        UpdateTimestamp();
    }
}