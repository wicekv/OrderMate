using OrderMate.Core.Aggregates.OrderAggregate.Events;

namespace OrderMate.Core.Aggregates.OrderAggregate.Handlers;

public class NewOrderAddedNotificationHandler : INotificationHandler<NewOrderAddedEvent>
{
  public Task Handle(NewOrderAddedEvent notification, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
