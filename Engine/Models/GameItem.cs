using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon
        }

        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }

        //If we construct a GameItem object and we only pass in the first 3 parameters, use false for isUnique param.
        //This can only be done for params at end of parameters list
        //Damage parameters are optional so don't need to pass in for non-weapon gameitems
        public GameItem(ItemCategory category, int itemTypeID, string name, int price, 
                        bool isUnique = false, int minimumDamage = 0, int maximumDamage = 0)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
        }

        public GameItem Clone()
        {
            //instantiate a new gameitem that has same properties as itself
            return new GameItem(Category, ItemTypeID, Name, Price, 
                                IsUnique, MinimumDamage, MaximumDamage);
        }
    }
}
