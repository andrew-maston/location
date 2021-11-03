using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace location.Models.Dtos
{
    public class LocationUpdateRequest
    {
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public double? Latitude { get; set; }

        [Required]
        public double? Longitude { get; set; }
    }
}