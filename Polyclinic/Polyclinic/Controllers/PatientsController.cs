using Microsoft.AspNetCore.Mvc;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.Interface;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Получить всех пациентов
        /// </summary>
        /// <returns>Список всех пациентов</returns>
        /// <response code="200">Успешно найдено</response>
        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Получить информацию о пациенте по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пациента</param>
        /// <returns>Информация о пациенте по указанному идентификатору</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Пациент не найден</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        /// <summary>
        /// Создать нового пациента
        /// </summary>
        /// <param name="patientDto">Данные для создания нового пациента</param>
        /// <returns>Созданный пациент</returns>
        /// <response code="201">Пациент успешно создан</response>
        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto patientDto)
        {
            var createdPatient = await _patientService.CreatePatientAsync(patientDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.IdPatient }, createdPatient);
        }

        /// <summary>
        /// Обновить информацию о пациенте
        /// </summary>
        /// <param name="id">Идентификатор пациента</param>
        /// <param name="patientDto">Обновлённые данные пациента</param>
        /// <returns>Обновлённая информация о пациенте</returns>
        /// <response code="200">Информация о пациенте успешно обновлена</response>
        /// <response code="404">Пациент не найден</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(int id, PatientDto patientDto)
        {
            var updatedPatient = await _patientService.UpdatePatientAsync(id, patientDto);
            if (updatedPatient == null) return NotFound();
            return Ok(updatedPatient);
        }

        /// <summary>
        /// Удалить пациента
        /// </summary>
        /// <param name="id">Идентификатор пациента</param>
        /// <returns>Статус операции удаления</returns>
        /// <response code="204">Пациент успешно удалён</response>
        /// <response code="404">Пациент не найден</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
