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

        [HttpGet]
        public async Task<ActionResult<List<DoctorScheduleDto>>> GetAll()
        {
            var schedules = await _doctorScheduleService.GetAllDoctorSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorScheduleDto>> GetById(int id)
        {
            var schedule = await _doctorScheduleService.GetDoctorScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorScheduleDto>> Create(DoctorScheduleDto scheduleDto)
        {
            var createdSchedule = await _doctorScheduleService.CreateDoctorScheduleAsync(scheduleDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.IdSchedule }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorScheduleDto>> Update(int id, DoctorScheduleDto scheduleDto)
        {
            var updatedSchedule = await _doctorScheduleService.UpdateDoctorScheduleAsync(id, scheduleDto);
            if (updatedSchedule == null) return NotFound();
            return Ok(updatedSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _doctorScheduleService.DeleteDoctorScheduleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
