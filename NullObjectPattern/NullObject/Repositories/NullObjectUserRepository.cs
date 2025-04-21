using NullObjectPattern.NullObject.Entities;

namespace NullObjectPattern.NullObject.Repositories;

public sealed class NullObjectUserRepository
{
    private readonly IList<IUser> _users = new List<IUser>();

    public NullObjectUserRepository()
    {
        _users.Add(new User(Guid.Parse("8363c613-f614-4f7c-971f-396cba910f32"), "John", true));
        _users.Add(new User(Guid.Parse("7c62b792-4903-4fe7-b264-870cdef66ea8"), "Jane", true));
        _users.Add(new User(Guid.Parse("2e32e097-d729-4d12-838d-bcc4e6cd821a"), "Bob", true));
    }

    public IUser GetNullObjectUserById(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return user ?? NullUser.Instance;
    }
}