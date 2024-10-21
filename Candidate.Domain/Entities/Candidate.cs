﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }  

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }   

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }    

        [Phone]
        public string PhoneNumber { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        public string CallTimeInterval { get; set; }

        [Url]
        public string LinkedInUrl { get; set; }

        [Url]
        public string GitHubUrl { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
