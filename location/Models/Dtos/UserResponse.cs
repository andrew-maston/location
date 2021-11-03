using System;

namespace location.Models.Dtos
{
    public class UserResponse
    {
        public UserResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}