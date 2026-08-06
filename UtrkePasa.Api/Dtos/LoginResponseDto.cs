namespace UtrkePasa.Api.Dtos;

public class LoginResponseDto
{
    public int UserId{get; set;}
    public string Name{get; set;} = string.Empty;
    public string Surname{get; set;} = string.Empty;
}