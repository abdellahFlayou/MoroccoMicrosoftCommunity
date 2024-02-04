using System;
using System.Collections.Generic;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Utilisateur
{
    public int UtilisateurId { get; set; }

    public string? AdresseMail { get; set; }

    public string? MotDePasse { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Sexe { get; set; }

    public string? Gsm { get; set; }

    public string? Ville { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Speaker> Speakers { get; set; } = new List<Speaker>();
}
