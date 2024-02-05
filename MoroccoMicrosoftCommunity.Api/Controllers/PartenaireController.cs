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
    public class PartenaireController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPartnerRepository _partnerRepository;
        private object _logger;

        public PartenaireController(IPartnerRepository partenaireRepo ,IMapper mapper )
        {
            _mapper = mapper;
            _partnerRepository = partenaireRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            var partners = await _partnerRepository.GetAllAsync();
            var partnersDTO = _mapper.Map<IEnumerable<PartenaireDto>>(partners);
            return Ok(partnersDTO);
        }

        [HttpGet("{partnerId}")]
        public async Task<IActionResult> GetPartnerById(int partnerId)
        {
            var partner = await _partnerRepository.GetById(partnerId);
            var partnerDTO = _mapper.Map<PartenaireDto>(partner);
            return Ok(partnerDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartner(PartenaireDto partnerDTO)
        {
            var partnerEntity = _mapper.Map<Partenaire>(partnerDTO);
            var result = await _partnerRepository.Add(partnerEntity);
            return Ok(result);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdatePartner(PartenaireDto partnerDTO)
        //{
        //    var partnerEntity = _mapper.Map<Partenaire>(partnerDTO);
        //    var result = await _partnerRepository.Update(partnerEntity);
        //    return Ok(result);
        //}

        //[HttpDelete("{partnerId}")]
        //public async Task<IActionResult> DeletePartner(int partnerId)
        //{
        //    var result = await _partnerRepository.DeleteById(partnerId);
        //    return Ok(result);
        //}
        [HttpPut("{partenaireId}")]
        public async Task<IActionResult> UpdatePartenaire(int partenaireId, [FromBody] PartenaireDto partenaireDTO)
        {
            try
            {
                // Vérifier si l'ID dans le chemin correspond à l'ID dans le DTO
                if (partenaireId != partenaireDTO.PartenaireId)
                {
                    return BadRequest("PartenaireId in the path does not match PartenaireId in the request body");
                }

                // Mapper le DTO vers l'entité
                var partenaireEntity = _mapper.Map<Partenaire>(partenaireDTO);

                // Mettre à jour l'entité dans le référentiel
                await _partnerRepository.Update(partenaireEntity);

                return Ok("Partenaire updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Partenaire: {ex.Message}");
                return StatusCode(500, "Une erreur interne est survenue lors de la suppression.");


            }
        }
            [HttpDelete("{partenaireId}")]
            public async Task<IActionResult> DeletePartenaire(int partenaireId)
            {
                try
                {
                    // Vérifier si le partenaire avec l'ID spécifié existe
                    var existingPartenaire = await _partnerRepository.GetById(partenaireId);

                    if (existingPartenaire == null)
                    {
                        return NotFound($"Partenaire with id {partenaireId} not found");
                    }

                    // Supprimer le partenaire du référentiel
                    await _partnerRepository.DeleteById(partenaireId);

                    return Ok($"Partenaire with id {partenaireId} deleted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating Partenaire: {ex.Message}");
                    return StatusCode(500, "Une erreur interne est survenue lors de la suppression.");
                }
            }

        }

    }

