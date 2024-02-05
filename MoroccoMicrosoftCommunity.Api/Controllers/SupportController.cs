using Microsoft.AspNetCore.Mvc;
using MoroccoMicrosoftCommunity.Application.Dtos;
using MoroccoMicrosoftCommunity.Application.Interface;
using System;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportRepo _supportRepo;

        public SupportController(ISupportRepo supportRepo)
        {
            _supportRepo = supportRepo ?? throw new ArgumentNullException(nameof(supportRepo));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supports = await _supportRepo.GetAllAsync();
            return Ok(supports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var support = await _supportRepo.GetById(id);
            if (support == null)
                return NotFound();

            return Ok(support);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] SupportDto supportDto)
        //{
        //    if (supportDto == null)
        //        return BadRequest();

        //    var support = await _supportRepo.Add(supportDto.ToEntity());
        //    return CreatedAtAction(nameof(GetById), new { id = support.SupportId }, support);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] SupportDto supportDto)
        //{
        //    if (supportDto == null || id != supportDto.SupportId)
        //        return BadRequest();

        //    var existingSupport = await _supportRepo.GetById(id);
        //    if (existingSupport == null)
        //        return NotFound();

        //    await _supportRepo.Update(supportDto.ToEntity(existingSupport));
        //    return NoContent();
        //}
        [HttpPost]
        public async Task<IActionResult> CreateSupport([FromBody] SupportDto supportDto)
        {
            if (supportDto == null)
            {
                return BadRequest("Le support ne peut pas être nul");
            }

            var support = supportDto.ToEntity();

            // Utilisez votre ISupportRepo pour ajouter le support à la base de données
            await _supportRepo.Add(support);
            _supportRepo.Save();

            return Ok("Support ajouté avec succès");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupport(int id, [FromBody] SupportDto supportDto)
        {
            if (supportDto == null)
            {
                return BadRequest("Le support ne peut pas être nul");
            }

            var existingSupport = await _supportRepo.GetById(id);

            if (existingSupport == null)
            {
                return NotFound("Support non trouvé");
            }

            // Mettez à jour les propriétés du support existant en fonction des valeurs de SupportDto
            existingSupport.Nom = supportDto.Nom;
            existingSupport.Path = supportDto.Path;
            existingSupport.Statut = supportDto.Statut;
            existingSupport.DureePartage = supportDto.DureePartage;

            // Utilisez votre ISupportRepo pour mettre à jour le support dans la base de données
            await _supportRepo.Update(existingSupport);
            _supportRepo.Save();

            return Ok("Support mis à jour avec succès");
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var support = await _supportRepo.GetById(id);
        //    if (support == null)
        //        return NotFound();

        //    await _supportRepo.DeleteById(id);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var existingSupport = await _supportRepo.GetById(id);

        //    if (existingSupport == null)
        //    {
        //        return NotFound("Support non trouvé");
        //    }

        //    await _supportRepo.DeleteById(id);
        //    _supportRepo.Save();

        //    return Ok("Support supprimé avec succès");
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"Trying to delete Support with ID: {id}");

            var existingSupport = await _supportRepo.GetById(id);

            if (existingSupport == null)
            {
                Console.WriteLine($"Support with ID {id} not found");
                return NotFound("Support non trouvé");
            }

            await _supportRepo.DeleteById(id);
            _supportRepo.Save();

            Console.WriteLine($"Support with ID {id} deleted successfully");

            return Ok("Support supprimé avec succès");
        }

    }
}
