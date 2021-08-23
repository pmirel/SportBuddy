using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class ChallengesController : BaseApiController
  {
    private readonly IUserRepository _userRepository;
    private readonly IChallengeRepository _challengeRepository;
    private readonly IMapper _mapper;
    public ChallengesController(IUserRepository userRepository, IChallengeRepository challengeRepository, IMapper mapper)
    {
      _mapper = mapper;
      _challengeRepository = challengeRepository;
      _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ChallengeDto>> CreateChallenge(CreateChallengeDto createChallengeDto)
    {
      var username = User.GetUsername();
      if (username == createChallengeDto.RecipientUsername.ToLower())
        return BadRequest("you cannot send challenges to yourself");

      var sender = await _userRepository.GetUserByUsernameAsync(username);
      var recipient = await _userRepository.GetUserByUsernameAsync(createChallengeDto.RecipientUsername);
      if (recipient == null) return NotFound();

      var challenge = new Challenge
      {
        Sender = sender,
        Recipient = recipient,
        SenderUsername = sender.UserName,
        RecipientUsername = recipient.UserName,
        Location = createChallengeDto.Location,
        Sport = createChallengeDto.Sport,
        EventDate = createChallengeDto.EventDate,
        Answer = createChallengeDto.Answer
      };

      _challengeRepository.AddChallenge(challenge);

      if (await _challengeRepository.SaveAllAsync()) return Ok(_mapper.Map<ChallengeDto>(challenge));
      return BadRequest("Failed to send challenge");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChallengeDto>>> GetChallengesForUser([FromQuery] ChallengeParams challengeParams)
    {
      challengeParams.Username = User.GetUsername();
      var challenges = await _challengeRepository.GetChallengesForUser(challengeParams);
      Response.AddPaginationHeader(challenges.CurrentPage, challenges.PageSize, challenges.TotalCount, challenges.TotalPages);

      return challenges;
    }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<ChallengeDto>>> GetChallengeThread(string username)
    {
      var currentUsername = User.GetUsername();

      return Ok(await _challengeRepository.GetChallengeThread(currentUsername, username));
    }
    [HttpPut]
    public async Task<ActionResult> UpdateChallenge(ChallengeUpdateDto challengeUpdateDto, int id)
    {
      var username = User.GetUsername();
      var challenge = await _challengeRepository.GetChallenge(id);
      if (challenge.Sender.UserName != username && challenge.Recipient.UserName != username)
        return Unauthorized();
      _mapper.Map(challengeUpdateDto, challenge);
      
       _challengeRepository.UpdateChallenge(challenge);

      if (await _challengeRepository.SaveAllAsync()) return Ok();

      return BadRequest("Problem updating challenge");

    }
  }
}