using Candidate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Domain.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Domain.Entities.Candidate> GetCandidateByEmail(string email, CancellationToken cancellationToken);
        Task Add(Domain.Entities.Candidate candidate, CancellationToken cancellationToken);
        Task Update(Domain.Entities.Candidate candidate, CancellationToken cancellationToken);
    }
}
