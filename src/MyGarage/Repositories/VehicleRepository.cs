using MyGarage.Models;
using MyGarage.Repositories.Interfaces;
using MyGarage.Services;
using SQLite;

namespace MyGarage.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public VehicleRepository(DatabaseService databaseService)
        {
            _db = databaseService.Database;
        }

        public Task<List<Vehicle>> GetAllAsync()
        {
            return _db.Table<Vehicle>().ToListAsync();
        }

        public Task<Vehicle?> GetByIdAsync(int id)
        {
            return _db.Table<Vehicle?>()
                .Where(vehicle => vehicle.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Vehicle vehicle)
        {
            if (vehicle.Id == 0)
            {
                return _db.InsertAsync(vehicle);
            }

            return _db.UpdateAsync(vehicle);
        }

        public Task<int> DeleteAsync(Vehicle vehicle)
        {
            return _db.DeleteAsync(vehicle);
        }
    }
}
