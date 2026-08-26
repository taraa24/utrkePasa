using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class OddsGenerator 
{
    private readonly Random _random = new();
    private const double Hold = 1.10;
    public List<RaceOdds> oddsGenerator(Race race)
    {

        var numOfDogs = race.DogStartingPosition.Count;
        var weights = new List<double>();

        for(int i = 0; i < numOfDogs; i++)
        {
            weights.Add(0.5 + _random.NextDouble()); // tezine izm 0.5 i 1.5
        }

        var wSum = weights.Sum();
        var odds = new List<RaceOdds>();

        for(int i = 0; i < numOfDogs; i++)
        {
            var probability = weights[i]/ wSum;
            var casinoPorbability = probability * Hold;
            var odd = 1.0/ casinoPorbability;

            odds.Add(new RaceOdds
            {
                RaceId = race.RaceId,
                oddType = (i + 1).ToString(),
                Odd = (float)Math.Round(odd, 2)
            });
        }
        
        return odds;
    }

    public string FormatOdds(Race race, List<RaceOdds> odds)
    {
        var result = new List<string>();

        foreach(var dogPosition in race.DogStartingPosition)
        {
            var parts = dogPosition.Split(':');
            var dogName = parts[0];
            var position = int.Parse(parts[1]);

            var dogOdd = odds.First(o => o.oddType == position.ToString());

            result.Add($"{dogName}:{dogOdd.Odd:F2}");
        }

        return string.Join(",", result);
    }
}