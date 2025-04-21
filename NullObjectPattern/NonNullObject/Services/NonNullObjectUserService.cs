using NullObjectPattern.NonNullObject.Entities;
using NullObjectPattern.NonNullObject.Repositories;

namespace NullObjectPattern.NonNullObject.Services;

public sealed class NonNullObjectUserService
{
    private readonly NonNullObjectUserRepository _userRepository = new();

    public User GetCurrentUser(Guid userId)
    {
        return _userRepository.GetUserById(userId);
    }
}