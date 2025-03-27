namespace OrderMate.Core.Common;

public interface IEventStore
{
  Task AppendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
      where TEvent : AuditableDomainEventBase;
}
