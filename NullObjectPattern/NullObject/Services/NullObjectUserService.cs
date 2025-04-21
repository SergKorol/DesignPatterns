using NullObjectPattern.NullObject.Entities;
using NullObjectPattern.NullObject.Repositories;

namespace NullObjectPattern.NullObject.Services;

public sealed class NullObjectUserService
{
    private readonly NullObjectUserRepository _userRepository = new();

    public IUser GetCurrentNullObjectUser(Guid userId)
    {
        return _userRepository.GetNullObjectUserById(userId);
    }
}