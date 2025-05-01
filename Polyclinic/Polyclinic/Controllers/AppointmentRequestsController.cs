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

        [HttpGet]
        public async Task<ActionResult<List<AppointmentRequestDto>>> GetAll()
        {
            var requests = await _appointmentRequestService.GetAllAppointmentRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentRequestDto>> GetById(int id)
        {
            var request = await _appointmentRequestService.GetAppointmentRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentRequestDto>> Create(AppointmentRequestDto requestDto)
        {
            var createdRequest = await _appointmentRequestService.CreateAppointmentRequestAsync(requestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRequest.IdRequest }, createdRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentRequestDto>> Update(int id, AppointmentRequestDto requestDto)
        {
            var updatedRequest = await _appointmentRequestService.UpdateAppointmentRequestAsync(id, requestDto);
            if (updatedRequest == null) return NotFound();
            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _appointmentRequestService.DeleteAppointmentRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
