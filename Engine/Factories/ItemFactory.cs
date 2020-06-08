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
        private static List<GameItem> _standardGameItems;

        //The first time anyone uses anything in ItemFactory class, run this function.
        //Loads up list of game items.
        static ItemFactory()
        {
            _standardGameItems = new List<GameItem>();

            _standardGameItems.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));
            _standardGameItems.Add(new GameItem(9001, "Snake fang", 1));
            _standardGameItems.Add(new GameItem(9002, "Snakeskin", 2));
            _standardGameItems.Add(new GameItem(9003, "Rat tail", 1));
            _standardGameItems.Add(new GameItem(9004, "Rat fur", 2));
            _standardGameItems.Add(new GameItem(9005, "Spider fang", 1));
            _standardGameItems.Add(new GameItem(9006, "Spider silk", 2));
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            //use LINQ to find first item that has ItemTypeID property matching the itemTypeID passed to the function
            //If cannot find item then use default value (null)
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            //create a copy of the object (cloning), to allow unique individual objects
            if (standardItem != null)
            {
                return standardItem.Clone();
            }

            //we should never return null but it's in there just in case.
            return null;
        }
    }
}
