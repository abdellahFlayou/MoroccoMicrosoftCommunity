using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class SupportSession
{
    public int? SupportId { get; set; }

    public int? SessionId { get; set; }

    public DateTime? DateSupportSession { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Support? Support { get; set; }
}
