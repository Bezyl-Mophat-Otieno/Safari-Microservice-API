﻿namespace SafariAuthService.Data.Dto
{
    public class UserDTO
    {
        public Guid Guid { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;


        public string PhoneNumber {  get; set; } = string.Empty;
    }
}