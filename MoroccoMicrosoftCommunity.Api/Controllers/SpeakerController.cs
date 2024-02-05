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
            catch(Exception ex ) {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.ToString()}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeaker([FromBody]SpeakerDto speakerDto)
        {
            if (speakerDto == null)
            {
                return BadRequest("Invalid Data");
            }

            var speaker = _mapper.Map<Speaker>(speakerDto);
            var result = await _speakerRepo.Add(speaker);
            return Ok(result);
        }


    }
}
