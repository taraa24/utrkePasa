namespace UtrkePasa.Domain.Entities;

public class User
{
    public int UserId{get; set;}
    public string Name{get; set;} = string.Empty;
    public string Surname{get; set;} = string.Empty;
    public string Email{get; set;} = string.Empty;
    public string Password{get; set;} = string.Empty;
    public float WalletState{get; set;}

    public ICollection<Ticket> Tickets {get; set;} = new List<Ticket>();
    
}
