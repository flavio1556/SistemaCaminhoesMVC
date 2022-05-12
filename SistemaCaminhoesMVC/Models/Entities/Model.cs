using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCaminhoesMVC.Models.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
