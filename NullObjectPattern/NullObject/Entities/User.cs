namespace NullObjectPattern.NullObject.Entities;

public sealed class User(Guid id, string name, bool hasAccess) : IUser
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public bool HasAccess { get; set; } = hasAccess;
}