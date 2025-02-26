﻿using Microsoft.AspNetCore.Identity;

namespace Auth.API.Utility
{
    public class PasswordHasher: IPasswordHasher
    {
        
            public string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }
            public bool VerifyPassword(string password, string hash)
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
        
    }
}
