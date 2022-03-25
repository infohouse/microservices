using System;

namespace Contracts
{
    public record CatlogItemCreated(Guid itemId, string Name, string Description);
    public record CatlogItemUpdated(Guid itemId, string Name, string Description);
    public record CatlogItemDeleted(Guid itemId);
}