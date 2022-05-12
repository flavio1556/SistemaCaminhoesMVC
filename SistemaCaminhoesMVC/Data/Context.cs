using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCaminhoesMVC.Models.Entities;

namespace SistemaCaminhoesMVC.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
        }
        public DbSet<Vehicle> veiculo { get; set; }
        public DbSet<Model> Model { get; set; }
    }
}
