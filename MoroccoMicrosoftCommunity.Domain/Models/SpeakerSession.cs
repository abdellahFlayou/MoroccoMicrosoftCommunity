using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class SpeakerSession
{
    public int? SpeakerId { get; set; }

    public int? SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Speaker? Speaker { get; set; }
}
