namespace Lab8.Auth.Requests;

public class UserRegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Group { get; set; }
    public string Phone { get; set; }
    public string Variant { get; set; }
    public string Password { get; set; }
}