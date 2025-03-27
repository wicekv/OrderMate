namespace OrderMate.Core.Aggregates.EventAggregate;

public class StoredEvent
{
  public Guid Id { get; set; }
  public string EventType { get; set; } = string.Empty;
  public string Data { get; set; } = string.Empty;
  public DateTime OccurredAtUtc { get; set; }
  public string CausedBy { get; set; } = string.Empty;
}
