using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class User
{
    [Key]
    [Column("user_Id")]
    public int UserId{get; set;}

    [Column("name")]
    public string Name{get; set;} = string.Empty;

    [Column("surname")]
    public string Surname{get; set;} = string.Empty;

    [Column("email")]
    public string Email{get; set;} = string.Empty;

    [Column("password")]
    public string Password{get; set;} = string.Empty;

    [Column("wallet_State")]
    public float WalletState{get; set;}

    public ICollection<Ticket> tickets {get; set;} = new List<Ticket>();
    
}
