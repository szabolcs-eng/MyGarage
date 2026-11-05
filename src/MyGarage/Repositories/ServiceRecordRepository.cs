using MyGarage.Models;
using MyGarage.Repositories.Interfaces;
using MyGarage.Services;
using SQLite;

namespace MyGarage.Repositories
{
    public class ServiceRecordRepository : IServiceRecordRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public ServiceRecordRepository(DatabaseService databaseService)
        {
            _db = databaseService.Database;
        }

        public Task<List<ServiceRecord>> GetByVehicleIdAsync(int vehicleId)
        {
            return _db.Table<ServiceRecord>()
                .Where(record => record.VehicleId == vehicleId)
                .OrderByDescending(record => record.Date)
                .ToListAsync();
        }

        public Task<ServiceRecord?> GetByIdAsync(int id)
        {
            return _db.Table<ServiceRecord?>()
                .Where(record => record.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(ServiceRecord record)
        {
            if (record.Id == 0)
            {
                return _db.InsertAsync(record);
            }

            return _db.UpdateAsync(record);
        }

        public Task<int> DeleteAsync(ServiceRecord record)
        {
            return _db.DeleteAsync(record);
        }
    }
}
