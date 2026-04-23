namespace myapi.DTOs.Responses
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
