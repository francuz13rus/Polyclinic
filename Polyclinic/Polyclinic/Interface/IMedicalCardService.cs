using Polyclinic.Data_Transfer_Objects;

namespace Polyclinic.Interface
{
    public interface IMedicalCardService
    {
        Task<MedicalCardDto> GetMedicalCardByIdAsync(int id);
        Task<List<MedicalCardDto>> GetAllMedicalCardsAsync();
        Task<MedicalCardDto> CreateMedicalCardAsync(MedicalCardDto cardDto);
        Task<MedicalCardDto> UpdateMedicalCardAsync(int id, MedicalCardDto cardDto);
        Task<bool> DeleteMedicalCardAsync(int id);
    }
}
