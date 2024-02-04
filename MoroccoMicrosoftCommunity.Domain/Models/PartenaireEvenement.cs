using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class PartenaireEvenement
{
    public int PartenaireEvenementId { get; set; }

    public int? PartenaireId { get; set; }

    public int? EvenementId { get; set; }

    public virtual Evenement? Evenement { get; set; }

    public virtual Partenaire? Partenaire { get; set; }
}
