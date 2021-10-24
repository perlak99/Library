using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTO.Authentication
{
    public class UserRegisterDTO
    {
        [Required]
        public string RegisterUsername { get; set; }
        [Required]
        public string RegisterPassword { get; set; }
    }
}
