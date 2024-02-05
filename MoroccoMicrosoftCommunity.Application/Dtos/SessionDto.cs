using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MoroccoMicrosoftCommunity.Application.Dtos
{
    [DataContract]
    public class SessionDto
    {
        [Key]
        public int SessionId { get; set; }

        public string? TitreSession { get; set; }

        public DateTime? DateSession { get; set; }

        public string? Description { get; set; }

        public string? Adresse { get; set; }

        //public byte[]? Image { get; set; }
        public string? Image { get; set; } // Modifier le type de byte[] à string

        public int? EvenementId { get; set; }

        public int? UtilisateurId { get; set; }

        public int? SpeakerId { get; set; }

        // public SpeakerDto Speaker { get; set; }
    }
}
