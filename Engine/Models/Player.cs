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
    public class Player : BaseNotificationClass //any change to property alerts other classes that care about this property
    {
        private string _name;
        private string _characterClass;
        private int _hitPoints;
        private int _experiencePoints; //private backing variable
        private int _level;
        private int _gold;
        public string Name 
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        } //we can get and set this property 
        public string CharacterClass 
        { 
            get { return _characterClass; }
            set 
            { 
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set 
            { 
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
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
        public int Gold 
        {   get { return _gold; }
            set 
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        //ObservableCollection handles all the notifications
        public ObservableCollection<GameItem> Inventory { get; set; }

        //Where is basically Filter
        //deferred execution - waits to execute the LINQ query until it is really needed,
        //ToList forces it to be needed
        public List<GameItem> Weapons =>
            Inventory.Where(i => i is Weapon).ToList();
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();

            //When player moves to a new location, check whether location has any quests.
            //Then check if player doesn't have quests from that location yet.
            //Any quest player doesn't have yet will be added to Quests.
            //This is done inside GameSession.
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            //In case we've removed a weapon from the player's inventory, need to notify UI
            OnPropertyChanged(nameof(Weapons));
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
