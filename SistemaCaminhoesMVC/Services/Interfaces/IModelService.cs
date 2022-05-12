using SistemaCaminhoesMVC.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SistemaCaminhoesMVC.Services.Interfaces
{
    public interface IModelService
    {
        public Task<List<Model>> GetAllAsync();
    }
}
