using Candidate.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Candidate.DataAccess.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        protected readonly ApplicationContext _context;

        public CandidateRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.Candidate candidate, CancellationToken cancellationToken)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Domain.Entities.Candidate candidate, CancellationToken cancellationToken)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Domain.Entities.Candidate> GetCandidateByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .AsNoTracking()  
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken); 
        }

    }
}
