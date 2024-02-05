using MoroccoMicrosoftCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Interface
{
    public interface IEventRepo : IGenericRepo<Evenement>
    {
        
        bool Save();

    }
}
