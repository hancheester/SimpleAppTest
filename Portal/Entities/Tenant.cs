namespace Portal.Entities;

public class Tenant
{
    public int Id { get; set; }
    public Guid Identifier { get; set; }
    public string? Name { get; set; }
}
