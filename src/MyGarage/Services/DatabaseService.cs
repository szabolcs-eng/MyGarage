using MyGarage.Models;
using SQLite;

namespace MyGarage.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public SQLiteAsyncConnection Database => _db;

        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyGarage.db3");
            _db = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            await _db.CreateTableAsync<Vehicle>();
            await _db.CreateTableAsync<ServiceRecord>();
        }
    }
}
