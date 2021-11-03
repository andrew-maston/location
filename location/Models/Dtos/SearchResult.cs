using System;

namespace location.Models.Dtos
{
    public class SearchResult
    {
        public SearchResult(User user, UserLocation location, double? distance = null)
        {
            UserId = user.Id;
            Username = user.Username;
            Latitude = location.Location.Y;
            Longitude = location.Location.X;
            Timestamp = location.Timestamp;
            Distance = distance;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public double? Distance { get; set; }
    }
}