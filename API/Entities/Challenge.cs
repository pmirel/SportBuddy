using System;

namespace API.Entities
{
  public class Challenge
  {
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string SenderUsername { get; set; }
    public AppUser Sender { get; set; }
    public int RecipientId { get; set; }
    public string RecipientUsername { get; set; }
    public AppUser Recipient { get; set; }
    public string Location { get; set; }
    public string Sport { get; set; }
    public DateTime EventDate { get; set; }
    public bool? Answer { get; set; }
    public DateTime ChallengeSent { get; set; } = DateTime.Now;

  }
}