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
    public class PartenaireController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPartnerRepository _partnerRepository;
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
    }
}
