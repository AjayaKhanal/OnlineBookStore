namespace OnlineBookStore.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
