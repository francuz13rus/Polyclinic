using Microsoft.AspNetCore.Mvc;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.Interface;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSchedulesController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorSchedulesController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        /// <summary>
        /// Получить все расписания врачей
        /// </summary>
        /// <returns>Список всех расписаний врачей</returns>
        /// <response code="200">Успешно найдено</response>
        [HttpGet]
        public async Task<ActionResult<List<DoctorScheduleDto>>> GetAll()
        {
            var schedules = await _doctorScheduleService.GetAllDoctorSchedulesAsync();
            return Ok(schedules);
        }

        /// <summary>
        /// Получить расписание врача по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор расписания</param>
        /// <returns>Расписание врача по указанному идентификатору</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Расписание не найдено</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorScheduleDto>> GetById(int id)
        {
            var schedule = await _doctorScheduleService.GetDoctorScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        /// <summary>
        /// Создать новое расписание врача
        /// </summary>
        /// <param name="scheduleDto">Данные для создания нового расписания</param>
        /// <returns>Созданное расписание</returns>
        /// <response code="201">Расписание успешно создано</response>
        [HttpPost]
        public async Task<ActionResult<DoctorScheduleDto>> Create(DoctorScheduleDto scheduleDto)
        {
            var createdSchedule = await _doctorScheduleService.CreateDoctorScheduleAsync(scheduleDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.IdSchedule }, createdSchedule);
        }

        /// <summary>
        /// Обновить расписание врача
        /// </summary>
        /// <param name="id">Идентификатор расписания</param>
        /// <param name="scheduleDto">Обновлённые данные расписания</param>
        /// <returns>Обновлённое расписание</returns>
        /// <response code="200">Расписание успешно обновлено</response>
        /// <response code="404">Расписание не найдено</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorScheduleDto>> Update(int id, DoctorScheduleDto scheduleDto)
        {
            var updatedSchedule = await _doctorScheduleService.UpdateDoctorScheduleAsync(id, scheduleDto);
            if (updatedSchedule == null) return NotFound();
            return Ok(updatedSchedule);
        }

        /// <summary>
        /// Удалить расписание врача
        /// </summary>
        /// <param name="id">Идентификатор расписания</param>
        /// <returns>Статус операции удаления</returns>
        /// <response code="204">Расписание успешно удалено</response>
        /// <response code="404">Расписание не найдено</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _doctorScheduleService.DeleteDoctorScheduleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
