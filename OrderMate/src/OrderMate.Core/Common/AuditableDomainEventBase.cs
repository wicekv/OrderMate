using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMate.Core.Common;

public abstract class AuditableDomainEventBase : DomainEventBase
{
  public string CausedByUser { get; set; } = string.Empty;
  public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
}
