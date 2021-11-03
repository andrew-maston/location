using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace location.Controllers
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