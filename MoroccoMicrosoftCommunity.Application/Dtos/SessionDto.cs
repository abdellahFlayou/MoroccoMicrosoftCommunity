using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Dtos
{
    public class SessionDto

    {
        public int SessionId { get; set; }

        public string? TitreSession { get; set; }

        public DateTime? DateSession { get; set; }

        public string? Description { get; set; }

        public string? Adresse { get; set; }

        public byte[]? Image { get; set; }

        public int? EvenementId { get; set; }

        public int? UtilisateurId { get; set; }

        public int? SpeakerId { get; set; }
    }
}
