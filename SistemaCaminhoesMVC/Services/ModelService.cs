using Microsoft.EntityFrameworkCore;
using SistemaCaminhoesMVC.Data;
using SistemaCaminhoesMVC.Models.Entities;
using SistemaCaminhoesMVC.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCaminhoesMVC.Services
{
    public class ModelService : IModelService
    {
        private readonly Context _context;
        public ModelService(Context context)
        {
            _context = context;
        }
        public Task<List<Model>> GetAllAsync()
        {
            return _context.Model.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
