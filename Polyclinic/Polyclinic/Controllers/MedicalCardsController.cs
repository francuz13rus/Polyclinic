using Microsoft.AspNetCore.Mvc;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.Interface;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalCardsController : ControllerBase
    {
        private readonly IMedicalCardService _medicalCardService;

        public MedicalCardsController(IMedicalCardService medicalCardService)
        {
            _medicalCardService = medicalCardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalCardDto>>> GetAll()
        {
            var cards = await _medicalCardService.GetAllMedicalCardsAsync();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalCardDto>> GetById(int id)
        {
            var card = await _medicalCardService.GetMedicalCardByIdAsync(id);
            if (card == null) return NotFound();
            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalCardDto>> Create(MedicalCardDto cardDto)
        {
            var createdCard = await _medicalCardService.CreateMedicalCardAsync(cardDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCard.IdCard }, createdCard);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalCardDto>> Update(int id, MedicalCardDto cardDto)
        {
            var updatedCard = await _medicalCardService.UpdateMedicalCardAsync(id, cardDto);
            if (updatedCard == null) return NotFound();
            return Ok(updatedCard);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _medicalCardService.DeleteMedicalCardAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
