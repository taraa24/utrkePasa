using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class Dog
{
    [Key]
    [Column("dog_Id")]
    public int DogId{get;set;}
    
    [Column("dog_Name")]
    public string DogName{get; set;} = string.Empty;

}