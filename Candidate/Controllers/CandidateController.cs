using Candidate.Domain.Interfaces;
using Candidate.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateController(ICandidateRepository candidateService)
        {
            _candidateRepository = candidateService;
        }
        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertCandidate([FromBody] CandidateDto candidate, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (candidate == null)
            {
                return BadRequest("Candidate cannot be null.");
            }
            
            // check if candidate exists
            var existingCandidate = await _candidateRepository.GetCandidateByEmail(candidate.Email, cancellationToken);
            if (existingCandidate != null) 
            {
                // Update existing candidate
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
                existingCandidate.GitHubUrl = candidate.GitHubUrl;
                existingCandidate.Comment = candidate.Comment;

                await _candidateRepository.Update(existingCandidate, cancellationToken);
            }
            else
            {
                // Add candidate
                var newCandidate = new Domain.Entities.Candidate
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    PhoneNumber = candidate.PhoneNumber,
                    Email = candidate.Email,
                    CallTimeInterval = candidate.CallTimeInterval,
                    LinkedInUrl = candidate.LinkedInUrl,
                    GitHubUrl = candidate.GitHubUrl,
                    Comment = candidate.Comment
                };
                // add candidate
                await _candidateRepository.Add(newCandidate, cancellationToken);
            };

            return Ok("Candidate added / updated successfully");

        }
    }
}
