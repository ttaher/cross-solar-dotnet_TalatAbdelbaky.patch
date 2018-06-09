﻿using System.ComponentModel.DataAnnotations;

namespace CrossSolar.Models
{
    public class PanelModel
    {
        public int Id { get; set; }

        [Required]
        [Range(-90, 90)]
        [RegularExpression(@"^\d+(\.\d{6})$")]
        public double Latitude { get; set; }


        [Range(-180, 180)]
        [RegularExpression(@"^\d+(\.\d{6})$")]
        public double Longitude { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "This field must be 16 characters")]
        public string Serial { get; set; }

        public string Brand { get; set; }

    }
}
