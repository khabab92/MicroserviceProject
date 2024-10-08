﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dto
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string roleName { get; set; }
    }
}
