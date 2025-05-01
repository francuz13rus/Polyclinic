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

        /// <summary>
        /// Получить все медицинские карты
        /// </summary>
        /// <returns>Список всех медицинских карт</returns>
        /// <response code="200">Успешно найдено</response>
        [HttpGet]
        public async Task<ActionResult<List<MedicalCardDto>>> GetAll()
        {
            var cards = await _medicalCardService.GetAllMedicalCardsAsync();
            return Ok(cards);
        }

        /// <summary>
        /// Получить медицинскую карту по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор медицинской карты</param>
        /// <returns>Медицинская карта по указанному идентификатору</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Медицинская карта не найдена</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalCardDto>> GetById(int id)
        {
            var card = await _medicalCardService.GetMedicalCardByIdAsync(id);
            if (card == null) return NotFound();
            return Ok(card);
        }

        /// <summary>
        /// Создать новую медицинскую карту
        /// </summary>
        /// <param name="cardDto">Данные для создания новой медицинской карты</param>
        /// <returns>Созданная медицинская карта</returns>
        /// <response code="201">Медицинская карта успешно создана</response>
        [HttpPost]
        public async Task<ActionResult<MedicalCardDto>> Create(MedicalCardDto cardDto)
        {
            var createdCard = await _medicalCardService.CreateMedicalCardAsync(cardDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCard.IdCard }, createdCard);
        }

        /// <summary>
        /// Обновить информацию о медицинской карте
        /// </summary>
        /// <param name="id">Идентификатор медицинской карты</param>
        /// <param name="cardDto">Обновлённые данные медицинской карты</param>
        /// <returns>Обновлённая медицинская карта</returns>
        /// <response code="200">Медицинская карта успешно обновлена</response>
        /// <response code="404">Медицинская карта не найдена</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalCardDto>> Update(int id, MedicalCardDto cardDto)
        {
            var updatedCard = await _medicalCardService.UpdateMedicalCardAsync(id, cardDto);
            if (updatedCard == null) return NotFound();
            return Ok(updatedCard);
        }

        /// <summary>
        /// Удалить медицинскую карту
        /// </summary>
        /// <param name="id">Идентификатор медицинской карты</param>
        /// <returns>Статус операции удаления</returns>
        /// <response code="204">Медицинская карта успешно удалена</response>
        /// <response code="404">Медицинская карта не найдена</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _medicalCardService.DeleteMedicalCardAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
