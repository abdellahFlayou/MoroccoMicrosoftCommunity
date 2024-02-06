using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoroccoMicrosoftCommunity.Domain.Models;

public partial class Evenement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EvenementId { get; set; }

    public string? Titre { get; set; }

    public string? Photo { get; set; }

    public string? Description { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public virtual ICollection<PartenaireEvenement> PartenaireEvenements { get; set; } = new List<PartenaireEvenement>();

    public virtual ICollection<Partenaire> Partenaires { get; set; } = new List<Partenaire>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
