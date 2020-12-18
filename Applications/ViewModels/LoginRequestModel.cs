using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Applications.ViewModels
{
    public class LoginRequestModel
    {
        [JsonPropertyNameAttribute("userName")]
        [Required]
        public string Username { get; set; }
        [Required]
        [JsonPropertyNameAttribute("password")]
        public string Password { get; set; }




    }
}
