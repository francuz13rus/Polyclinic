using Microsoft.AspNetCore.Mvc;
using Polyclinic.Data_Transfer_Objects;
using Polyclinic.Interface;

namespace Polyclinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentRequestsController : ControllerBase
    {
        private readonly IAppointmentRequestService _appointmentRequestService;

        public AppointmentRequestsController(IAppointmentRequestService appointmentRequestService)
        {
            _appointmentRequestService = appointmentRequestService;
        }

        /// <summary>
        /// Получить список всех заявок на приём
        /// </summary>
        /// <returns>Список заявок</returns>
        /// <response code="200">Успешно получен список заявок</response>
        [HttpGet]
        public async Task<ActionResult<List<AppointmentRequestDto>>> GetAll()
        {
            var requests = await _appointmentRequestService.GetAllAppointmentRequestsAsync();
            return Ok(requests);
        }

        /// <summary>
        /// Получить заявку по ID
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <returns>Данные заявки</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Заявка не найдена</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentRequestDto>> GetById(int id)
        {
            var request = await _appointmentRequestService.GetAppointmentRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        /// <summary>
        /// Создать новую заявку на приём
        /// </summary>
        /// <param name="requestDto">Данные новой заявки</param>
        /// <returns>Созданная заявка</returns>
        /// <response code="201">Заявка успешно создана</response>
        [HttpPost]
        public async Task<ActionResult<AppointmentRequestDto>> Create(AppointmentRequestDto requestDto)
        {
            var createdRequest = await _appointmentRequestService.CreateAppointmentRequestAsync(requestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRequest.IdRequest }, createdRequest);
        }

        /// <summary>
        /// Обновить существующую заявку
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <param name="requestDto">Обновлённые данные</param>
        /// <returns>Обновлённая заявка</returns>
        /// <response code="200">Заявка успешно обновлена</response>
        /// <response code="404">Заявка не найдена</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentRequestDto>> Update(int id, AppointmentRequestDto requestDto)
        {
            var updatedRequest = await _appointmentRequestService.UpdateAppointmentRequestAsync(id, requestDto);
            if (updatedRequest == null) return NotFound();
            return Ok(updatedRequest);
        }

        /// <summary>
        /// Удалить заявку по ID
        /// </summary>
        /// <param name="id">ID заявки</param>
        /// <response code="204">Заявка успешно удалена</response>
        /// <response code="404">Заявка не найдена</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _appointmentRequestService.DeleteAppointmentRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
