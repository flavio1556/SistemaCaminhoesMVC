using SistemaCaminhoesMVC.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaCaminhoesMVC.Services.Interfaces
{
    public interface IVehicleService
    {
        public Task InsertAsync(Vehicle vehicle);
        public Task UpdateAsync(Vehicle vehicle);
        public Task DeleteAsync(int id);
        public Task<Vehicle> GetByIdAsync(int id);
        public Task<List<Vehicle>> GetAllAsync();
        public List<int> GetYearsAvailable();
        public Task Save();
    }
}

