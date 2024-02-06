using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Dtos
{
    public class EventDto
    {
        public int EvenementId { get; set; }

        public string? Titre { get; set; }

        public string? Photo { get; set; }

        public string? Description { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }
    }
}
