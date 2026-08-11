using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class DogRaceState
{
    public Dog? Dog {get;set;}
    public int Position{get;set;} = 0;
    public int Place{get;set;}

}