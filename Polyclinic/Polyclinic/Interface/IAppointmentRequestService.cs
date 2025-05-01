using Polyclinic.Data_Transfer_Objects;

namespace Polyclinic.Interface
{
    public interface IAppointmentRequestService
    {
        Task<AppointmentRequestDto> GetAppointmentRequestByIdAsync(int id);
        Task<List<AppointmentRequestDto>> GetAllAppointmentRequestsAsync();
        Task<AppointmentRequestDto> CreateAppointmentRequestAsync(AppointmentRequestDto requestDto);
        Task<AppointmentRequestDto> UpdateAppointmentRequestAsync(int id, AppointmentRequestDto requestDto);
        Task<bool> DeleteAppointmentRequestAsync(int id);
    }
}
