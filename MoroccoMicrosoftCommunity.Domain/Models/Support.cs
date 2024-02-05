using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Support
{
    public int SupportId { get; set; }

    public string? Nom { get; set; }

    public string? Path { get; set; }

    public string? Statut { get; set; }

    public int? DureePartage { get; set; }
}
