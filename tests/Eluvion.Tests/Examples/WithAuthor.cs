using Eluvion.Forge;
using Tonga.Enumerable;

namespace Eluvion.Tests.Examples;

/// <summary>
/// Adds author to origin data.
/// </summary>
public sealed class WithAuthor<TInput>(string userId, IList<User> catalog) : CraftEnvelope<TInput, (TInput input, User author)>(input =>
    (
        input, 
        catalog.FirstOne(user => user.UserId == userId, new ArgumentException($"UserId '{userId}' not found")).Value() 
    )
)
{ }

public record User(string UserId, string UserName);