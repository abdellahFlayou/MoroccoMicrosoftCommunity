using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoroccoMicrosoftCommunity.Application.Dtos;
using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Domain.Models;

namespace MoroccoMicrosoftCommunity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepo _eventRepo;
        private readonly IMapper _mapper;
        public EventController(IEventRepo eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Evenement>))]
        public async Task<IActionResult> GetEvent()
        {
            var evenements = _mapper.Map<List<EventDto>>(await _eventRepo.GetAllAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(evenements);
        }
        [HttpGet("{eventId}")]
        [ProducesResponseType(200, Type = typeof(Evenement))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEventById(int eventId)
        {
            //if (!_eventRepo.IsExists(eventId))
            //  return NotFound();
            var evenement = _mapper.Map<Evenement>(await _eventRepo.GetById(eventId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(evenement);

        }
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (eventDto == null)
                return BadRequest("Invalid data");

            var eventEntity = _mapper.Map<Evenement>(eventDto);
            var resulta = await _eventRepo.Add(eventEntity);
            return Ok(resulta);
        }
        [HttpPut("{eventId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateEvent(int eventId, [FromBody] EventDto eventDto)
        {
            try
            {
                if (eventDto == null || eventId != eventDto.EvenementId)
                {
                    return BadRequest();
                }

                var existingEvent = await _eventRepo.GetById(eventId);

                if (existingEvent == null)
                {
                    return NotFound();
                }

                // Mettez à jour les propriétés de l'événement existant avec celles du DTO
                _mapper.Map(eventDto, existingEvent);

                await _eventRepo.Update(existingEvent);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Loggez l'exception ou renvoyez une réponse d'erreur appropriée
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }


       


    }
}
