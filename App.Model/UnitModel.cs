using System.ComponentModel.DataAnnotations;

namespace App.Model
{
    public class UnitModel
    {
        [Required]
        public double Unit{ get; set; }

        [Required]
        public bool ToMetric { get; set; }

        [Required]
        public int UserId { get; set; }

        public double Result { get; set; }
    }
}