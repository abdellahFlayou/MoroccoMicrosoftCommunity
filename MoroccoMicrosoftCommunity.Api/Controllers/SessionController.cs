using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoroccoMicrosoftCommunity.Application.Dtos;
using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpeakerRepo _speakerRepo;
        private readonly ISessionRepo _sessionRepo;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionRepo sessionRepo, IMapper mapper, ILogger<SessionController> logger, ISpeakerRepo speakerRepo)
        {
            _sessionRepo = sessionRepo ?? throw new ArgumentNullException(nameof(sessionRepo));
            _speakerRepo = speakerRepo ?? throw new ArgumentNullException(nameof(speakerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SessionDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSessions()
        {
            //  try
            //{
            //var sessions = await _sessionRepo.GetAllAsync();
            //if (sessions == null || !sessions.Any())
            //{
            //    return NotFound("Aucune session trouvée.");
            //}

            //var sessionDtos = _mapper.Map<List<SessionDto>>(sessions);
            //return Ok(sessionDtos);
            //}
            //catch (Exception ex)
            //{
            //  _logger.LogError($"Erreur lors de la récupération des sessions : {ex}");
            // return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            //}
            var session = _mapper.Map<List<SessionDto>>(await _sessionRepo.GetAllAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(session);
        }

        [HttpGet("{sessionId}")]
        [ProducesResponseType(200, Type = typeof(SessionDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            try
            {
                var session = _mapper.Map<SessionDto>(await _sessionRepo.GetById(sessionId));
                if (session == null)
                {
                    return NotFound("Session non trouvée.");
                }
                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la récupération de la session : {ex}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération de la session.");
            }
        }

        [HttpPost]
        //[ProducesResponseType(201, Type = typeof(SessionDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
        {
            //try
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    //var existingSpeaker = await _speakerRepo.GetById(sessionDto.SpeakerId);
            //    //if (existingSpeaker == null)
            //    //{
            //    //    throw new Exception("Le conférencier spécifié n'existe pas.");
            //    //}

            //    // Map SessionDto to Session entity
            //    var sessionEntity = _mapper.Map<Session>(sessionDto);

            //    // Convert Base64-encoded string to byte[]
            //    //sessionEntity.Image = Convert.FromBase64String(sessionDto.Image);

            //    // Assign the existing speaker
            //    //sessionEntity.SpeakerId = existingSpeaker.SpeakerId;

            //    await _sessionRepo.Add(sessionEntity);

            //    if (_sessionRepo.Save())
            //    {
            //        var createdSessionDto = _mapper.Map<SessionDto>(sessionEntity);
            //        return CreatedAtAction(nameof(GetSessionById), new { sessionId = createdSessionDto.SessionId }, createdSessionDto);
            //    }
            //    else
            //    {
            //        return StatusCode(500, "Erreur lors de la création de la session.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Gérer l'exception
            //    _logger.LogError($"Erreur lors de la création de la session : {ex}");

            //    if (ex.InnerException != null)
            //    {
            //        _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
            //    }

            //    return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            //}
            try
            {
                //if(SessionDto == null)
                //{
                //    return BadRequest("Invalid Data");
                //}
                var session = _mapper.Map<Session>(sessionDto);
                var result = await _sessionRepo.Add(session);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des  : {ex.Message}");
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }


        }

        [HttpPut("{sessionId}")]
        [ProducesResponseType(200, Type = typeof(SessionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateSession(int sessionId, [FromBody] SessionDto sessionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (sessionDto == null)
                {
                    return BadRequest("Invalid data");
                }

                var existingSession = await _sessionRepo.GetById(sessionId);

                if (existingSession == null)
                {
                    return NotFound(); // Ressource non trouvée
                }

                _mapper.Map(sessionDto, existingSession);

                await _sessionRepo.Update(existingSession);

                return Ok(_mapper.Map<SessionDto>(existingSession));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{sessionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSession(int sessionId)
        {
            try
            {
                var existingSession = await _sessionRepo.GetById(sessionId);

                if (existingSession == null)
                {
                    return NotFound(); // Ressource non trouvée
                }

                await _sessionRepo.DeleteById(sessionId);

                return NoContent(); // Suppression réussie, retourner le statut 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}









//using AutoMapper;
//using Azure.Core;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using MoroccoMicrosoftCommunity.Application.Dtos;
//using MoroccoMicrosoftCommunity.Application.Interface;
//using MoroccoMicrosoftCommunity.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MoroccoMicrosoftCommunity.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SessionController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        private readonly ISpeakerRepo _speakerRepo;
//        private readonly ISessionRepo _sessionRepo;
//        private readonly ILogger<SessionController> _logger;

//        public SessionController(ISessionRepo sessionRepo, IMapper mapper, ILogger<SessionController> logger, ISpeakerRepo speakerRepo)
//        {
//            _sessionRepo = sessionRepo ?? throw new ArgumentNullException(nameof(sessionRepo));
//            _speakerRepo = speakerRepo ?? throw new ArgumentNullException(nameof(speakerRepo));
//            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        [HttpGet]
//        [ProducesResponseType(200, Type = typeof(IEnumerable<SessionDto>))]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> GetSessions()
//        {
//            try
//            {
//                var sessions = await _sessionRepo.GetAllAsync();
//                if (sessions == null || !sessions.Any())
//                {
//                    return NotFound("Aucune session trouvée.");
//                }

//                var sessionDtos = _mapper.Map<List<SessionDto>>(sessions);
//                return Ok(sessionDtos);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Erreur lors de la récupération des sessions : {ex}");
//                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
//            }
//        }

//        [HttpGet("{sessionId}")]
//        [ProducesResponseType(200, Type = typeof(SessionDto))]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> GetSessionById(int sessionId)
//        {
//            try
//            {
//                var session = _mapper.Map<SessionDto>(await _sessionRepo.GetById(sessionId));
//                if (session == null)
//                {
//                    return NotFound("Session non trouvée.");
//                }
//                return Ok(session);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Erreur lors de la récupération de la session : {ex}");
//                return StatusCode(500, "Une erreur interne est survenue lors de la récupération de la session.");
//            }
//        }

//        [HttpPost]
//        [ProducesResponseType(201, Type = typeof(SessionDto))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(500)]

//        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest(ModelState);
//                }

//                var existingSpeaker = await _speakerRepo.GetById(sessionDto.SpeakerId);
//                if (existingSpeaker == null)
//                {
//                    throw new Exception("Le conférencier spécifié n'existe pas.");
//                }

//                // Map SessionDto to Session entity
//                var sessionEntity = _mapper.Map<Session>(sessionDto);

//                // Assign base64-encoded string directly to the Image property
//                sessionEntity.Image = Convert.FromBase64String(sessionDto.Image.ToString()); 

//                // Assign the existing speaker
//                sessionEntity.SpeakerId = existingSpeaker.SpeakerId;

//                await _sessionRepo.Add(sessionEntity);

//                if (_sessionRepo.Save())
//                {
//                    var createdSessionDto = _mapper.Map<SessionDto>(sessionEntity);
//                    return CreatedAtAction(nameof(GetSessionById), new { sessionId = createdSessionDto.SessionId }, createdSessionDto);
//                }
//                else
//                {
//                    return StatusCode(500, "Erreur lors de la création de la session.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle the exception
//                Console.WriteLine($"Erreur lors de la création de la session : {ex}");

//                if (ex.InnerException != null)
//                {
//                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
//                }

//                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
//            }
//        }



//        [HttpPut("{sessionId}")]
//        [ProducesResponseType(200, Type = typeof(SessionDto))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> UpdateSession(int sessionId, [FromBody] SessionDto sessionDto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest(ModelState);
//                }

//                if (sessionDto == null)
//                {
//                    return BadRequest("Invalid data");
//                }

//                var existingSession = await _sessionRepo.GetById(sessionId);

//                if (existingSession == null)
//                {
//                    return NotFound(); // Ressource non trouvée
//                }

//                _mapper.Map(sessionDto, existingSession);

//                await _sessionRepo.Update(existingSession);

//                return Ok(_mapper.Map<SessionDto>(existingSession));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Internal Server Error: {ex}");
//                return StatusCode(500, $"Internal Server Error: {ex.Message}");
//            }
//        }

//        [HttpDelete("{sessionId}")]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> DeleteSession(int sessionId)
//        {
//            try
//            {
//                var existingSession = await _sessionRepo.GetById(sessionId);

//                if (existingSession == null)
//                {
//                    return NotFound(); // Ressource non trouvée
//                }

//                await _sessionRepo.DeleteById(sessionId);

//                return NoContent(); // Suppression réussie, retourner le statut 204 No Content
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Internal Server Error: {ex}");
//                return StatusCode(500, $"Internal Server Error: {ex.Message}");
//            }
//        }
//    }
//}

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
