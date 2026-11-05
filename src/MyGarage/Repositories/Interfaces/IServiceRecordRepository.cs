using MyGarage.Models;

namespace MyGarage.Repositories.Interfaces
{
    public interface IServiceRecordRepository
    {
        Task<List<ServiceRecord>> GetByVehicleIdAsync(int vehicleId);

        Task<ServiceRecord?> GetByIdAsync(int id);

        Task<int> SaveAsync(ServiceRecord record);

        Task<int> DeleteAsync(ServiceRecord record);
    }
}
