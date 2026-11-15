using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCommShared.DTOs
{
    //This class represents the information returned by Core Identity when a user logs in
    public class LoginResultDTO
    {
        public string TokenType { get; set; } = "";
        public string AccessToken { get; set; } = "";
        
        public long ExpiresIn { get; set; } // Expiration time in seconds

        public string RefreshToken { get; set; } = "";
    }
}
