using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTO.Authentication
{
    public class UserLoginDTO
    {
        [Required]
        public string LoginUsername { get; set; }
        [Required]
        public string LoginPassword { get; set; }
    }
}
