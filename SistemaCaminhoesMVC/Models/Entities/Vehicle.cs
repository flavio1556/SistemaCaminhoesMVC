using System.ComponentModel.DataAnnotations;
using System;
using static SistemaCaminhoesMVC.Models.Enums.EnumsCommons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SistemaCaminhoesMVC.Models.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}  required")]
        [Display(Name = "Year of Manufacture")]       
        public int YearManufacture { get; set; }

        [Required(ErrorMessage = "{0}  required")]
        [Display(Name = "model year")]
        [RangeUntilCurrentYearAttribute(ErrorMessage = "The {0} must be between {1} and {2}")]
        public int ModelYear { get; set; }

        [Required(ErrorMessage = "{0}  required")]
        public VehicleType VehicleType { get; set; }

        //[RegularExpression(@"^[A-HJ-NPR-Za-hj-npr-z\d]{8}[\dX][A-HJ-NPR-Za-hj-npr-z\d]{2}\d{6}$", ErrorMessage = "Please provide a valid chassis")]
        [Required(ErrorMessage = "{0}  required")]
        public string Chassi { get; set; }

        public Model Model { get; set; }
        public int ModelId { get; set; }

        
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeUntilCurrentYearAttribute : RangeAttribute, IClientValidatable
    {
        public RangeUntilCurrentYearAttribute() : base(DateTime.Now.Year, DateTime.Now.Year +1)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "futuredate"
            };
        }
    }

}
