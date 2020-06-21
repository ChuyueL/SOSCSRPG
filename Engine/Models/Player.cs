using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    //still implements INotifyPropertyChanged, but through the base class
    public class Player : LivingEntity
    {
        private string _characterClass;
        private int _experiencePoints; //private backing variable
        private int _level;

        public string CharacterClass 
        { 
            get { return _characterClass; }
            set 
            { 
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints { //property
            get { return _experiencePoints; }
            set { 
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level 
        { 
            get { return _level; }
            set 
            { 
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }

        public Player()
        {
            //When player moves to a new location, check whether location has any quests.
            //Then check if player doesn't have quests from that location yet.
            //Any quest player doesn't have yet will be added to Quests.
            //This is done inside GameSession.
            Quests = new ObservableCollection<QuestStatus>();
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                //Count how many items player has in their inventory where the ItemID matches.
                //If the count is less than the count of the passed in parameter, return false 
                //as player does not have enough items.
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }

            }

            //If we get through all the items in the list and we haven't returned false for any,
            //player has enough items so return true.
            return true;
        }
    }
}
