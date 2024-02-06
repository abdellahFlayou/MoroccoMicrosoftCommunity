using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

            var evenement = _mapper.Map<Evenement>(await _eventRepo.GetById(eventId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(evenement);

        }
        //[HttpPost, DisableRequestSizeLimit]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (eventDto == null)
        //        return BadRequest("Invalid data");

        //    var eventEntity = _mapper.Map<Evenement>(eventDto);
        //    var resulta = await _eventRepo.Add(eventEntity);
        //    return Ok(resulta);
        //}
        //        [HttpPost, DisableRequestSizeLimit]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (eventDto == null)
        //        return BadRequest("Invalid data");

        //    // Assurez-vous que EvenementId est null dans le DTO
        //    eventDto.EvenementId = null;

        //    var eventEntity = _mapper.Map<Evenement>(eventDto);
        //    var result = await _eventRepo.Add(eventEntity);

        //    return Ok(result);
        //}
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (eventDto == null)
                return BadRequest("Invalid data");

            // Mapper le DTO vers l'entité Evenement
            var eventEntity = _mapper.Map<Evenement>(eventDto);

            // Ne pas fournir de valeur pour EvenementId
            // car c'est une colonne d'identité gérée automatiquement par la base de données

            // Ajouter l'événement à la base de données
            await _eventRepo.Add(eventEntity);

            // Retourner une réponse avec le code de statut 201 (Created)
            // et inclure le nouvel événement dans le corps de la réponse
            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.EvenementId }, eventEntity);
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

                _mapper.Map(eventDto, existingEvent);

                await _eventRepo.Update(existingEvent);

                return NoContent();
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        //[HttpDelete("{eventId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> DeleteEvent(int eventId)
        //{
        //    try
        //    {

        //        var eventEntity = await _eventRepo.GetById(eventId);

        //        if (eventEntity == null)
        //        {
        //            return NotFound();
        //        }


        //        await _eventRepo.DeleteById(eventId);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
        //    }
        //}
        // Endpoint pour supprimer un événement
        [HttpDelete("{eventId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            try
            {

                var eventEntity = await _eventRepo.GetById(eventId);

                if (eventEntity == null)
                {
                    return NotFound();
                }


                await _eventRepo.DeleteById(eventId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de l'événement avec l'ID {eventId}: {ex.ToString()}");
                return StatusCode(500, "Une erreur interne est survenue lors de la suppression.");
            }
        }

    }
}
