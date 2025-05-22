namespace IDENTITY.BLL.DTO.Responses
{
    public class JwtResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool RequiresTwoFactor { get; set; }
        public string Token { get; set; }
    }
}
