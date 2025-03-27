using OrderMate.Core.Common;

namespace OrderMate.Core.Aggregates.UserAggregate.Handlers.UserCreated;

public class UserCreatedEvent : AuditableDomainEventBase
{
  public int UserId { get; init; }
  public string Email { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;

  public UserCreatedEvent(int userId, string email, string name)
  {
    UserId = userId;
    Email = email;
    Name = name;
  }
}

public class UserCreatedEventHandler(IEventStore eventStore) : INotificationHandler<UserCreatedEvent>
{
  public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
  {
    await eventStore.AppendAsync(notification, cancellationToken);
  }
}
