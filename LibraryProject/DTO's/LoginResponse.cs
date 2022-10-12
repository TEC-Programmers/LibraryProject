using LibraryProject.API.Helpers;

namespace LibraryProject.API.DTO_s
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
