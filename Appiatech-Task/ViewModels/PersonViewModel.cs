using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appiatech_Task.ViewModels
{
    public class PersonViewModel
    {
        [Required(ErrorMessage = "You must insert user name")]
        [StringLength(maximumLength: 30, ErrorMessage = "The user name length should be 30 chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must insert the Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must insert user phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The phone number should have 10 numbers")]
        public string Phone { get; set; }
    }
}
