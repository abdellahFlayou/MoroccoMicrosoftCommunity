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
        public EventController(IEventRepo eventRepo , IMapper mapper)
        {
            _eventRepo = eventRepo; 
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Evenement>))]
        public async Task <IActionResult> GetEvent()
        {
            var evenements = _mapper.Map<List<EventDto>>( await _eventRepo.GetAllAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(evenements);
        }

        [HttpGet("{eventId}")]
        [ProducesResponseType(200, Type = typeof(Evenement))]
        [ProducesResponseType(400)]
        public async Task <IActionResult> GetEventById(int eventId)
        {
            //if (!_eventRepo.IsExists(eventId))
              //  return NotFound();
            var evenement = _mapper.Map<Evenement>(await _eventRepo.GetById(eventId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(evenement);
        }
    }
}
