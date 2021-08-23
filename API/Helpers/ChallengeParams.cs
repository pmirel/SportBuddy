namespace API.Helpers
{
    public class ChallengeParams: PaginationParams
    {
        public string Username { get; set; }
        public string Container { get; set; } = "New";
    }
}