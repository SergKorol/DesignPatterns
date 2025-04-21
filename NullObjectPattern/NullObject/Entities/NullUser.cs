namespace NullObjectPattern.NullObject.Entities;

public sealed class NullUser : IUser
{
    public static readonly NullUser Instance = new ();
    
    private NullUser() { }
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public bool HasAccess { get; set; } = false;
}