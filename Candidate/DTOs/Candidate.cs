using System.ComponentModel.DataAnnotations;

namespace Candidate.WebAPI.DTOs
{
    public class CandidateDto
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string CallTimeInterval { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn URL format.")]
        public string LinkedInUrl { get; set; }

        [Url(ErrorMessage = "Invalid GitHub URL format.")]
        public string GitHubUrl { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }
    }
}
