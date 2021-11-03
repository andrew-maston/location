using location.Models;
using System;

namespace location.Controllers
{
    public class LocationResponse
    {
        public LocationResponse(User user, UserLocation location)
        {
            UserId = user.Id;
            Username = user.Username;
            Latitude = location.Location.Y;
            Longitude = location.Location.X;
            Timestamp = location.Timestamp;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}