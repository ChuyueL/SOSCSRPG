using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>(); //can only be accessed in World

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            _locations.Add(new Location(xCoordinate, yCoordinate, name, description,
                                        $"/Engine;component/Images/Locations/{imageName}"));
        }

        //Returns the Location objects at the x-coordinate and y-coordinate provided.
        public Location LocationAt(int xCoordinate, int yCoordinate) //need to call it from UI project so public
        {
            foreach (Location loc in _locations) 
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }

            }

            return null;
        }
    }
}
