using System;
using System.Collections.Generic;

#nullable disable

namespace location.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public ICollection<UserLocation> Locations { get; set; } = new List<UserLocation>();
    }
}
