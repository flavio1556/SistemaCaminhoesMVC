using Microsoft.EntityFrameworkCore;
using Moq;
using SistemaCaminhoesMVC.Data;
using SistemaCaminhoesMVC.Models.Entities;
using SistemaCaminhoesMVC.Services;
using SistemaCaminhoesMVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static SistemaCaminhoesMVC.Models.Enums.EnumsCommons;

namespace SistemaCaminhoes.Teste.Services
{
    public class VehicleServiceTests
    {
        private VehicleService vehicleService;
        private Model model1 = new Model { Id = 1, Name = "FH" };
        private Model model2 = new Model { Id = 2, Name = "FM" };
        private Model model3 = new Model { Id = 3, Name = "VM" };
        private Model model4 = new Model { Id = 4, Name = "FMX" };

        public VehicleServiceTests()
        {
            var builder = new DbContextOptionsBuilder<Context>().UseSqlServer("Data Source=DESKTOP-DLP7BK8\\SQLEXPRESS;Initial Catalog=Teste;Integrated Security=True")
            .Options;
            //var options = builder.Options;
            var context = new Mock<Context>(builder).Object;
            context.Database.Migrate();
            vehicleService = new VehicleService(context, new Mock<IModelService>().Object);
        }
        [Fact]
        public async Task Insert_SeendingValidId()
        {
            Vehicle vehicle1 = new Vehicle { Chassi = "2ZCZD6lD17L5U8508".ToUpper(), Model = model1, ModelYear = 2020, VehicleType = VehicleType.Caminhao, YearManufacture = 2019 };
            try
            {
               await vehicleService.InsertAsync(vehicle1);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
