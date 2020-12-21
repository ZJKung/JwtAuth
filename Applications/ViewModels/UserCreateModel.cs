using System;
using System.Text.Json.Serialization;
namespace Applications.ViewModels
{
    public class UserCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("secret")]
        public string Secret { get; set; }



    }
}
