using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using SistemaCaminhoesMVC.Models.Entities;
using System.Collections.Generic;
using static SistemaCaminhoesMVC.Models.Enums.EnumsCommons;
using System.Threading.Tasks;

namespace SistemaCaminhoesMVC.Data
{
    public class SeedingService
    {
        private readonly Context _context;
        public SeedingService(Context salesWebContext)
        {
            _context = salesWebContext;
        }
        public void Seed()
        {
            if (  _context.Model.Any() ||  _context.veiculo.Any() )
            {
                return;
            }
            Model model1 = new Model { Id = 1, Name = "FH" };
            Model model2 = new Model { Id = 2, Name = "FM" };
            Model model3 = new Model { Id = 3, Name = "VM" };
            Model model4 = new Model { Id = 4, Name = "FMX" };
            _context.Model.AddRange(model1, model2, model3, model4);
            _context.SaveChanges();

            Vehicle vehicle1 = new Vehicle {  Chassi = "2ZCZD6lD17L5U8508".ToUpper(), Model = model1, ModelYear = 2020, VehicleType = VehicleType.Caminhao, YearManufacture = 2019 };
            Vehicle vehicle2 = new Vehicle {  Chassi = "8ahA73Pan4bA40383".ToUpper(), Model = model2, ModelYear = 2020, VehicleType = VehicleType.Caminhao, YearManufacture = 2020 };
            Vehicle vehicle3 = new Vehicle {  Chassi = "23Fmz24bRgKme0030".ToUpper(), Model = model1, ModelYear = 2021, VehicleType = VehicleType.Caminhao, YearManufacture = 2021 };
            Vehicle vehicle4 = new Vehicle {  Chassi = "8S1jHFNtvAUrT9155".ToUpper(), Model = model2, ModelYear = 2022, VehicleType = VehicleType.Caminhao, YearManufacture = 2021 };

            
            
            _context.veiculo.AddRange(vehicle1, vehicle2, vehicle3, vehicle4);

             _context.SaveChanges();
        }
    }
}
