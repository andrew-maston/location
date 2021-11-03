using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

#nullable disable

namespace location.Models
{
    public class UserLocation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }
        public Point Location { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
