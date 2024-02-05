using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Dtos
{
    public class SponsorDto
    {
        public int SponsorId { get; set; }

        public string? Nom { get; set; }

        public string? Logo { get; set; }

        public int? SessionId { get; set; }
    }
}
