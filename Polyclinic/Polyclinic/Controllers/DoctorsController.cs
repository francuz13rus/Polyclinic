using Microsoft.AspNetCore.Mvc;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.Interface;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Получить всех врачей
        /// </summary>
        /// <returns>Список всех врачей</returns>
        /// <response code="200">Успешно найдено</response>
        [HttpGet]
        public async Task<ActionResult<List<DoctorDto>>> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Получить информацию о враче по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор врача</param>
        /// <returns>Информация о враче по указанному идентификатору</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Врач не найден</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        /// <summary>
        /// Создать нового врача
        /// </summary>
        /// <param name="doctorDto">Данные для создания нового врача</param>
        /// <returns>Созданный врач</returns>
        /// <response code="201">Врач успешно создан</response>
        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Create(DoctorDto doctorDto)
        {
            var createdDoctor = await _doctorService.CreateDoctorAsync(doctorDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDoctor.IdDoctor }, createdDoctor);
        }

        /// <summary>
        /// Обновить информацию о враче
        /// </summary>
        /// <param name="id">Идентификатор врача</param>
        /// <param name="doctorDto">Обновлённые данные врача</param>
        /// <returns>Обновлённая информация о враче</returns>
        /// <response code="200">Информация о враче успешно обновлена</response>
        /// <response code="404">Врач не найден</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> Update(int id, DoctorDto doctorDto)
        {
            var updatedDoctor = await _doctorService.UpdateDoctorAsync(id, doctorDto);
            if (updatedDoctor == null) return NotFound();
            return Ok(updatedDoctor);
        }

        /// <summary>
        /// Удалить информацию о враче
        /// </summary>
        /// <param name="id">Идентификатор врача</param>
        /// <returns>Статус операции удаления</returns>
        /// <response code="204">Врач успешно удалён</response>
        /// <response code="404">Врач не найден</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
