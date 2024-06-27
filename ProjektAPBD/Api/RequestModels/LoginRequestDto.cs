using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class LoginRequestDto
{
    public string Login { get; set; }
    public string Password { get; set; }
}