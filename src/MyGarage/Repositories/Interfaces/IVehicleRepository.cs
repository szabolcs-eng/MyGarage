using MyGarage.Models;

namespace MyGarage.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();

        Task<Vehicle?> GetByIdAsync(int id);

        Task<int> SaveAsync(Vehicle vehicle);

        Task<int> DeleteAsync(Vehicle vehicle);
    }
}
