using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Location
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageName { get; } //holds file path of image
        public List<Quest> QuestsAvailableHere { get; } = new List<Quest>();

        public List<MonsterEncounter> MonstersHere { get; } =
            new List<MonsterEncounter>();

        //Needs a setter as this property is set after the Location object is instantiated
        public Trader TraderHere { get; set; }

        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            //If MonstersHere already has a MonsterEncounter object with that monster ID, then
            //get first monsterencounter object where the ID matches (should only be 1) and sets the chance
            //of encountering it to the newly passed in chanceOfEncountering.
            //Basically, just overwrites it, doesn't add a new MonsterEncounter object. Ensures we don't add
            //the same monster multiple times
            if (MonstersHere.Exists(m => m.MonsterID == monsterID))
            {
                MonstersHere.First(m => m.MonsterID == monsterID).ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                //Otherwise, add a new MonsterEncounter object.
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }

            
        }
        public Monster GetMonster()
        {
            //If there are no MonsterEncounters, return null (no monster at location)
            if (!MonstersHere.Any())
            {
                return null;
            }

            //Sum the percentages of all monsters at this location
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

            //Select a random number between 1 and the total
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            //Loop through the monster list, adding the monster's percentage chance of appearing 
            //to runningTotal. When randomNumber < runningTotal, the monster is returned.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            //If there was a problem, return the last monster in the list. This should never happen
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }

    }
}
