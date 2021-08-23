using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class ChallengeRepository : IChallengeRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ChallengeRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public void AddChallenge(Challenge challenge)
    {
      _context.Challenges.Add(challenge);
    }


    public async Task<Challenge> GetChallenge(int id)
    {
      return await _context.Challenges.FindAsync(id);
    }

    public async Task<PagedList<ChallengeDto>> GetChallengesForUser(ChallengeParams challengeParams)
    {
      var query = _context.Challenges
      .OrderByDescending(c => c.ChallengeSent)
      .AsQueryable();

      query = challengeParams.Container switch
      {
        "Received" => query.Where(u => u.Recipient.UserName == challengeParams.Username && u.Answer != null),
        "Sent" => query.Where(u => u.Sender.UserName == challengeParams.Username && u.Answer != null),
        _ => query.Where(u => u.Recipient.UserName == challengeParams.Username && u.Answer == null)
      };

      var challenges = query.ProjectTo<ChallengeDto>(_mapper.ConfigurationProvider);

      return await PagedList<ChallengeDto>.CreateAsync(challenges, challengeParams.PageNumber, challengeParams.PageSize);

    }

    public async Task<IEnumerable<ChallengeDto>> GetChallengeThread(string currentUsername, string recipientUsername)
    {
      var challenges = await _context.Challenges
      .Include(u => u.Sender).ThenInclude(p => p.Photos)
      .Include(u => u.Recipient).ThenInclude(p => p.Photos)
      .Where(c => c.Recipient.UserName == currentUsername
          && c.Sender.UserName == recipientUsername
          || c.Recipient.UserName == recipientUsername
           && c.Sender.UserName == currentUsername
      )
      .OrderBy(c => c.ChallengeSent)
      .ToListAsync();

      var newChallenges = challenges.Where(c => c.Answer == null && c.Recipient.UserName == currentUsername).ToList();

      return _mapper.Map<IEnumerable<ChallengeDto>>(challenges);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void UpdateChallenge(Challenge challenge)
    {
      _context.Entry(challenge).State = EntityState.Modified;
    }
  }
}