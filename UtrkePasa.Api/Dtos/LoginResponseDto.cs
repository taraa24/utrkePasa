namespace UtrkePasa.Api.Dtos;

public class LoginResponseDto
{
    public int user_Id{get; set;}
    public string name{get; set;} = string.Empty;
    public string surname{get; set;} = string.Empty;
}