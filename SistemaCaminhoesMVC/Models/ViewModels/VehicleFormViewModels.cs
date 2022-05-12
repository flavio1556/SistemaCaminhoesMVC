using SistemaCaminhoesMVC.Models.Entities;
using System.Collections.Generic;

namespace SistemaCaminhoesMVC.Models.ViewModels
{
    public class VehicleFormViewModels
    {
        public Vehicle Vehicle { get; set; }
        public List<Model> Models { get; set; }
    }
}
