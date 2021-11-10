using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace CDCC.Bussiness.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("is_system_admin")]
        public bool IsSystemAdmin { get; set; }
        public bool? Status { get; set; }
    }
}
