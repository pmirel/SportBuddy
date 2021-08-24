using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
  public interface IChallengeRepository
  {
    void AddChallenge(Challenge challenge);
    void DeleteChallenge(Challenge challenge);
    void UpdateChallenge(Challenge challenge);
    Task<Challenge> GetChallenge(int id);
    Task<PagedList<ChallengeDto>> GetChallengesForUser(ChallengeParams challengeParams);
    Task<IEnumerable<ChallengeDto>> GetChallengeThread(string currentUsername, string recipientUsername);
    Task<bool> SaveAllAsync();
  }
}