﻿namespace Cheesemaking_recipes_API
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireMinutes { get; set; }
        public string JwtIssuer { get; set; }
    }
}
