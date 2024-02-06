using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
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
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepo _speakerRepo;
        private readonly IMapper _mapper;
        private object _logger;
        public SpeakerController(ISpeakerRepo speakerRepo, IMapper mapper)
        {
            _mapper = mapper;
            _speakerRepo = speakerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpeakers()
        {
            var speakers = _mapper.Map<List<SpeakerDto>>(await _speakerRepo.GetAllAsync());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(speakers);
        }

        [HttpGet]
        [Route("{speakerId}")]
        public async Task<IActionResult> GetSpeakerById(int speakerId)
        {
            try
            {
                var speaker = await _speakerRepo.GetById(speakerId);
                if (speaker == null)
                {
                    return NotFound();
                }
                var speakerDto = _mapper.Map<SpeakerDto>(speaker);
                return Ok(speakerDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.ToString()}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateSpeaker([FromBody] SpeakerDto speakerDto)
        //{
        //    try
        //    {
        //        if (speakerDto == null)
        //        {
        //            return BadRequest("Invalid Data");
        //        }

        //        var speaker = _mapper.Map<Speaker>(speakerDto);
        //        var result = await _speakerRepo.Add(speaker);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Erreur lors de la récupération des  : {ex.Message}");
        //        return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> PostSpeaker(SpeakerDto speakerDto)
        {
            try
            {
                // Map SpeakerDto to Speaker entity
                var speaker = _mapper.Map<Speaker>(speakerDto);

                // Call repository to add speaker
                var addedSpeaker = await _speakerRepo.Add(speaker);

                // Map Speaker entity back to SpeakerDto
                var addedSpeakerDto = _mapper.Map<SpeakerDto>(addedSpeaker);

                // Return created SpeakerDto
                return CreatedAtAction(nameof(GetSpeakerById), new { id = addedSpeakerDto.SpeakerId }, addedSpeakerDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating Partenaire: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{speakerId}")]
        public async Task<IActionResult> UpdateSpeaker(int speakerId, [FromBody] SpeakerDto speakerDto)
        {
            try
            {

                if (speakerDto == null || speakerId != speakerDto.SpeakerId)
                {
                    return NotFound();
                }

                var existingSpeaker = await _speakerRepo.GetById(speakerId);

                if (existingSpeaker == null)
                {
                    return NotFound();
                }

                _mapper.Map(speakerDto, existingSpeaker);
                await _speakerRepo.Update(existingSpeaker);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.Message}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            }
        }

        [HttpDelete("{speakerId}")]
        public async Task<IActionResult> DeleteSpeaker(int speakerId)
        {
            try
            {
                var speakerToDelete = await _speakerRepo.GetById(speakerId);
                if (speakerToDelete == null)
                {
                    return NotFound();
                }
                await _speakerRepo.DeleteById(speakerId);
                return Ok($"Speaker with id {speakerId} deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}