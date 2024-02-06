using MoroccoMicrosoftCommunity.Domain.Models;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Application.Interface
{
    public interface ISpeakerRepo : IGenericRepo<Speaker>
    {
       // Task<Speaker> AddOrUpdateSpeaker(Speaker speaker);
        Task<Speaker> GetById(int? speakerId);
        bool Save();
    }
}
