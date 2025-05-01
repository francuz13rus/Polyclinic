using Polyclinic.Data_Transfer_Objects;

namespace Polyclinic.Interface
{
    public interface IDoctorService
    {
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> CreateDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDto> UpdateDoctorAsync(int id, DoctorDto doctorDto);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
