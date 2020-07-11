using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        //If we construct a GameItem object and we only pass in the first 3 parameters, use false for isUnique param.
        //This can only be done for params at end of parameters list
        public GameItem(int itemTypeID, string name, int price, bool isUnique = false)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            //instantiate a new gameitem that has same properties as itself
            return new GameItem(ItemTypeID, Name, Price, IsUnique);
        }
    }
}
