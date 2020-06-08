using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    //internal = only using inside Engine project
    //static = don't need to create an instance of it
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        //Function run first time anyone uses anything inside this class
        //Use to populate _quests. Currently only creating one quest
        static QuestFactory()
        {
            //Declare items needed to complete the quest and reward items
            //Temporary variable so we can add items to complete and pass in as parameter
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>(); 
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            //Create quest
            _quests.Add(new Quest(1,
                "Clear the herb garden",
                "Defeat the snakes in the Herbalist's garden",
                itemsToComplete,
                25, 10,
                rewardItems));
         }

        internal static Quest GetQuestByID(int id)
        {
            //looks in _quests and returns first element where quest.id = id passed as parameter.
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
