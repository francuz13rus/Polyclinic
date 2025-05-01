using Polyclinic.Data_Transfer_Objects;

namespace Polyclinic.Interface
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);
        Task<PatientDto> UpdatePatientAsync(int id, PatientDto patientDto);
        Task<bool> DeletePatientAsync(int id);
    }
}
