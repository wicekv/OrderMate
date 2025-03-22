using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMate.Core.Aggregates.AuditAggregate;

public class AuditEntry : EntityBase, IAggregateRoot
{
  public string MedataData { get; private set; } = string.Empty;
  public DateTime StartTimeUtc { get; private set; }
  public DateTime EndTimeUtc { get; private set; }
  public bool Succeeded { get; private set; }
  public string ErrorMessage { get; private set; } = string.Empty;

  private AuditEntry() { }
}
