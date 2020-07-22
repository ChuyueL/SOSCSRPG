using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        //readonly = variable can only be set in declaration or in a constructor
        //Protects us from accidentally setting the value somewhere else.
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        //The first time anyone uses anything in ItemFactory class, run this function.
        //Loads up list of game items.
        static ItemFactory()
        {
            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);

            BuildMiscellaneousItem(9001, "Snake fang", 1);
            BuildMiscellaneousItem(9002, "Snakeskin", 2);
            BuildMiscellaneousItem(9003, "Rat tail", 1);
            BuildMiscellaneousItem(9004, "Rat fur", 2);
            BuildMiscellaneousItem(9005, "Spider fang", 1);
            BuildMiscellaneousItem(9006, "Spider silk", 2);
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            //use LINQ to find first item that has ItemTypeID property matching the itemTypeID passed to the function
            //If cannot find item then use default value (null)
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price,
                                        int minimumDamage, int maximumDamage)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Weapon, id, name, price,
                                    true, minimumDamage, maximumDamage));
        }
    }
}
