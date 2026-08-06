using System.ComponentModel.DataAnnotations;

namespace UtrkePasa.Domain.Entities;

public class User
{
    [Key]
    public int user_Id{get; set;}

    
    public string name{get; set;} = string.Empty;
    public string surname{get; set;} = string.Empty;
    public string email{get; set;} = string.Empty;
    public string password{get; set;} = string.Empty;
    public float wallet_State{get; set;}

    public ICollection<Ticket> tickets {get; set;} = new List<Ticket>();
    
}
