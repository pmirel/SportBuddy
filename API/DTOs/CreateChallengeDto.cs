using System;

namespace API.DTOs
{
    public class CreateChallengeDto
    {
         public string RecipientUsername { get; set; }
        public string Location { get; set; }
    public string Sport { get; set; }
    public DateTime EventDate { get; set; }
    public bool? Answer { get; set; }
    }
}