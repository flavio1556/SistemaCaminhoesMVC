using SistemaCaminhoesMVC.Data;
using SistemaCaminhoesMVC.Models.Entities;
using SistemaCaminhoesMVC.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using SistemaCaminhoesMVC.Services.Exceptions;

namespace SistemaCaminhoesMVC.Services
{   
    public class VehicleService : IVehicleService
    {
        private readonly Context _context;
        private readonly IModelService _modelService;
        public VehicleService(Context context, IModelService modelService)
        {
            _context = context;
            _modelService = modelService;
        }
        public async Task DeleteAsync(int id)
        {
            Vehicle vehicle = await this.GetByIdAsync(id);
            if (vehicle == null)
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.veiculo.Remove(vehicle);
                await Save();
            }
            catch (Exception)
            {
                throw new IntegrityException("Can't delete Vehicle ");
            }
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            var result = await _context.veiculo.Include(obj => obj.Model).ToListAsync();
            return result;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.veiculo.Include(obj => obj.Model).Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Vehicle vehicle)
        {
            bool hasAny = await _context.veiculo.AnyAsync(x => x.Chassi == vehicle.Chassi);
            if (hasAny)
            {
                throw new NotFoundException("There is already a vehicle with this chassi");
            }
            bool isModelValid = await ModelIsValid(vehicle);
            if (!isModelValid)
            {
                throw new ApplicationException("Model is not available for new registrations");
            }
            _context.Add(vehicle);
            await Save();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            bool hasAny = await this.AnyByIdAsync(vehicle.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }
            bool isModelValid = await ModelIsValid(vehicle);
            if (!isModelValid)
            {
                throw new ApplicationException("Model is not available for new registrations");
            }
            try
            {
                _context.Update(vehicle);
                await Save();
            }
            catch (ApplicationException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> AnyByIdAsync(int id)
        {
          return  await _context.veiculo.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> ModelIsValid(Vehicle vehicle)
        {
            var listModel = await _modelService.GetAllAsync();
            listModel = listModel.Where(x => x.Name == "FM" || x.Name== "FH").ToList();
            return listModel.Any(x => x.Id == vehicle.ModelId);
        }
        public List<int> GetYearsAvailable()
        {
           List<int> years = new List<int> {DateTime.Now.Year, DateTime.Now.Year + 1};
            return years;
        }

    }
}
