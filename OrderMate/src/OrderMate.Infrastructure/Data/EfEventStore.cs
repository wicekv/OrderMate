using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderMate.Core.Aggregates.EventAggregate;
using OrderMate.Core.Common;

namespace OrderMate.Infrastructure.Data;

public class EfEventStore(AppDbContext context) : IEventStore
{
  public async Task AppendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
      where TEvent : AuditableDomainEventBase
  {
    var stored = new StoredEvent
    {
      Id = Guid.NewGuid(),
      EventType = typeof(TEvent).Name,
      Data = JsonSerializer.Serialize(@event, @event.GetType()),
      OccurredAtUtc = @event.DateOccurred,
      CausedBy = @event.CausedByUser
    };

    context.StoredEvents.Add(stored);
    await context.SaveChangesAsync(cancellationToken);
  }
}
