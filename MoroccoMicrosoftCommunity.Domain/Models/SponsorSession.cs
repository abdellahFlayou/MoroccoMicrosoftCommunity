using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class SponsorSession
{
    public int SponsorSessionId { get; set; }

    public int? SponsorId { get; set; }

    public int? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Sponsor? Sponsor { get; set; }
}
