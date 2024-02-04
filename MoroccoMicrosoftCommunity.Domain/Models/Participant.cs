using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Participant
{
    public int ParticipantId { get; set; }

    public int? UtilisateurId { get; set; }

    public virtual Utilisateur? Utilisateur { get; set; }
}
