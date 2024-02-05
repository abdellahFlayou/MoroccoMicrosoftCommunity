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
            try
            {
                var speakers = await _speakerRepo.GetAllAsync();

                if(speakers == null || !speakers.Any()) {
                    return NotFound();
                }
                var speakerDto = _mapper.Map<List<SpeakerDto>>(speakers);
                return Ok(speakerDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des sessions : {ex.ToString()}");
                return StatusCode(500, "Une erreur interne est survenue lors de la récupération des sessions.");
            }
        }

        [HttpGet]
        [Route("{id}")]
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
    }
}
