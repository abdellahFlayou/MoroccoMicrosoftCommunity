using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Speaker
{
    public int SpeakerId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Photo { get; set; }

    public bool? Mct { get; set; }

    public bool? Mvp { get; set; }

    public string? Biography { get; set; }

    public string? LienTwitter { get; set; }

    public string? LienFacebook { get; set; }

    public string? LienLinkedin { get; set; }

    public string? LienInstagram { get; set; }

    public string? SiteWeb { get; set; }

    public int? UtilisateurId { get; set; }

   // public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual Utilisateur? Utilisateur { get; set; }
}
