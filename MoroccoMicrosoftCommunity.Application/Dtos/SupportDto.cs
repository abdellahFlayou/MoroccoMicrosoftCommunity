using MoroccoMicrosoftCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Dtos
{
    public class SupportDto
    {
        public int SupportId { get; set; }

        public string? Nom { get; set; }

        public string? Path { get; set; }

        public string? Statut { get; set; }

        public int? DureePartage { get; set; }
        public Support ToEntity()
        {
            return new Support
            {
                // Assignez les propriétés de SupportDto aux propriétés de Support
                SupportId = this.SupportId,
                Nom = this.Nom,
                Path = this.Path,
                Statut = this.Statut,
                DureePartage = this.DureePartage
            };
        }
    }
}
