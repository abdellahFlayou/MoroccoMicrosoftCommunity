using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoroccoMicrosoftCommunity.Application.Dtos;
using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Domain.Models;
using MoroccoMicrosoftCommunity.Infrastructure.Data;
using MoroccoMicrosoftCommunity.Infrastructure.Repositories;
using System;

namespace MoroccoMicrosoftCommunity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionRepo _sessionRepo;
        //private readonly ISpeakerRepo _speakerRepo;
        //private readonly AppDBContext _dbContext;
        public SessionController(ISessionRepo sessionRepo, IMapper mapper)
        {
            _sessionRepo = sessionRepo ?? throw new ArgumentNullException(nameof(sessionRepo)); ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;

            //_speakerRepo = speakerRepo;
            //_dbContext = appDBContext;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SessionDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSessions()
        {
            try
            {
                var sessions = await _sessionRepo.GetAllAsync();
                if (sessions == null || !sessions.Any())
                {
                    return NotFound("Aucune session trouvée.");
                }

                var sessionDtos = _mapper.Map<List<SessionDto>>(sessions);
                return Ok(sessionDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.ToString()}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            }
        }
        [HttpGet("{sessionId}")]
        [ProducesResponseType(200, Type = typeof(Session))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {

            var session = _mapper.Map<Session>(await _sessionRepo.GetById(sessionId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(session);

        }


        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (sessionDto == null)
                return BadRequest("Invalid data");

            try
            {
                // Mapping de SessionDto vers Session
                var sessionEntity = _mapper.Map<Session>(sessionDto);

                // Ajout de Session
                var addedSession = await _sessionRepo.Add(sessionEntity);

                // Enregistrement des modifications dans la base de données
                var isSaved = _sessionRepo.Save();

                if (isSaved)
                {
                    return Ok(addedSession);
                }
                else
                {
                    // La sauvegarde a échoué
                    return StatusCode(500, "Internal Server Error");
                }
            }
            catch (Exception ex)
            {
                // Gérer les exceptions
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
//}[HttpPost, DisableRequestSizeLimit]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(400)]
//        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if (sessionDto == null)
//                return BadRequest("Invalid data");

//            // Ajouter le conférencier s'il n'existe pas
//            var speaker = _mapper.Map<Speaker>(sessionDto.Speaker); // Assurez-vous que Speaker est bien défini dans SessionDto
//            var existingSpeaker = await _speakerRepo.GetById(speaker.SpeakerId);

//            if (existingSpeaker == null)
//            {
//                var addedSpeaker = await _speakerRepo.Add(speaker);
//                sessionDto.SpeakerId = addedSpeaker.SpeakerId; // Mettez à jour l'ID du conférencier dans la sessionDto
//            }
//            else
//            {
//                sessionDto.SpeakerId = existingSpeaker.SpeakerId; // Utilisez l'ID du conférencier existant dans la sessionDto
//            }

//            var sessionEntity = _mapper.Map<Session>(sessionDto);
//            var result = await _sessionRepo.Add(sessionEntity);

//            return Ok(result);
//        }

//[HttpPost, DisableRequestSizeLimit]
//[ProducesResponseType(204)]
//[ProducesResponseType(400)]
//public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
//{
//    if (!ModelState.IsValid)
//        return BadRequest(ModelState);

//    if (sessionDto == null)
//        return BadRequest("Invalid data");

//    // Ajouter le conférencier s'il n'existe pas
//    var speaker = _mapper.Map<Speaker>(sessionDto.Speaker); // Assurez-vous que Speaker est bien défini dans SessionDto
//    var existingSpeaker = await _speakerRepo.GetById(speaker.SpeakerId);

//    if (existingSpeaker == null)
//    {
//        var addedSpeaker = await _speakerRepo.Add(speaker);
//        sessionDto.SpeakerId = addedSpeaker.SpeakerId; // Mettez à jour l'ID du conférencier dans la sessionDto
//    }
//    else
//    {
//        sessionDto.SpeakerId = existingSpeaker.SpeakerId; // Utilisez l'ID du conférencier existant dans la sessionDto
//    }

//    var sessionEntity = _mapper.Map<Session>(sessionDto);
//    var result = await _sessionRepo.Add(sessionEntity);

//    return Ok(result);
//}
 //[HttpPost, DisableRequestSizeLimit]
 //       [ProducesResponseType(204)]
 //       [ProducesResponseType(400)]
 //       public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
 //       {
 //           if (!ModelState.IsValid)
 //               return BadRequest(ModelState);

 //           if (sessionDto == null)
 //               return BadRequest("Invalid data");

 //           try
 //           {
 //               // Map SessionDto to Session entity
 //               var sessionEntity = _mapper.Map<Session>(sessionDto);

 //               // Add or update the associated Speaker
 //               var speaker = sessionEntity.Speaker;
 //               if (speaker != null)
 //               {
 //                   var existingSpeaker = await _sessionRepo.GetById(speaker.SpeakerId);

 //                   if (existingSpeaker == null)
 //                   {
 //                       // Add the new speaker
 //                       var addedSpeaker = await _sessionRepo.Add(speaker);
 //                       sessionEntity.SpeakerId = addedSpeaker.SpeakerId;
 //                   }
 //                   else
 //                   {
 //                       // Update the existing speaker
 //                       sessionEntity.SpeakerId = existingSpeaker.SpeakerId;
 //                       _sessionRepo.Update(speaker);
 //                   }
 //               }

 //               // Add the session to the repository
 //               await _sessionRepo.Add(sessionEntity);

 //               // Save changes
 //               _sessionRepo.Save();

 //               return NoContent();
 //           }
 //           catch (Exception ex)
 //           {
 //               // Log the exception or handle it as needed
 //               return StatusCode(500, "Internal Server Error");
 //           }