using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Partenaire
{
    public int PartenaireId { get; set; }

    public string? Nom { get; set; }

    public string? Logo { get; set; }

    public int? EvenementId { get; set; }

    public virtual Evenement? Evenement { get; set; }

    public virtual ICollection<PartenaireEvenement> PartenaireEvenements { get; set; } = new List<PartenaireEvenement>();
}
