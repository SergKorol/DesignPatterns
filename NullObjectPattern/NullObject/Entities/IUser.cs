namespace NullObjectPattern.NullObject.Entities;

public interface IUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool HasAccess { get; set; }
}