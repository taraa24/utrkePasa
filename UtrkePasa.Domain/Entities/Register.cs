using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class Register
{
    [Key]
    [Column("id")]
    public int Id{get;set;}

    [Column("app_name")]
    public string appName{get;set;} = string.Empty;

    [Column("ip_addr")]
    public string ipAddr{get;set;} = string.Empty;

    [Column("port")]
    public int port{get;set;}

    [Column("app_guid")]
    public Guid appGuid{get;set;}

    [Column("timestamp")]
    public DateTimeOffset? timestamp {get;set;}

    [Column("is_leader")]
    public bool isLeader {get;set;}

/*     [ConcurrencyCheck]
    public int Version { get; set; } */
}