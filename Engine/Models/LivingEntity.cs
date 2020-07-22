using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    //abstract = cannot be instantiated 
    public abstract class LivingEntity : BaseNotificationClass //any change to property alerts other classes that care about this property
    {
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;

        //private set = can only set the value inside LivingEntity class
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        //protected so player class can change maximumhitpoints when level increases
        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            protected set
            {
                _maximumHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        //protected = can set value inside livingentity class, or from child classes. Player class needs access
        //to this property.
        public int Level
        {
            get { return _level; }
            protected set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        //ObservableCollection handles all the notifications
        //No setters as we will never reset these properties, we will only change values in the existing collections.
        public ObservableCollection<GameItem> Inventory { get; }

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; }

        //Where is basically Filter
        //deferred execution - waits to execute the LINQ query until it is really needed,
        //ToList forces it to be needed
        public List<GameItem> Weapons =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Weapon).ToList();

        public bool IsDead => CurrentHitPoints <= 0;

        //Other objects can subscribe to this event so they will know when the livingentity is killed
        public event EventHandler OnKilled;

        //protected = can only be seen the child classes
        //limits the ability to instantiate LivingEntity objects
        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints, int gold, int level = 1)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
            
            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void TakeDamage(int hitPointsofDamage)
        {
            CurrentHitPoints -= hitPointsofDamage;

            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;

            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amountOfGold)
        {
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold)
        {
            if (amountOfGold > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }

            Gold -= amountOfGold;
        }
        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            //if item is unique, add a new groupedinventory item with quantity 1.
            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                //Check if this is the first one of this item that the player has
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID))
                {
                    //quantity 0 because next line adds 1 to quantity
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                }

                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }

            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            //get the first item from groupinventory where item id matches item id of item we want to remove
            //If item we want to remove is unique, we have to find that exact matching item
            //If not, there's no distinction between the individuzl items.
            //This is a ternary operator - if first part evaluates to true, the first statement is executed.
            GroupedInventoryItem groupedInventoryItemToRemove = item.IsUnique ?
                GroupedInventory.FirstOrDefault(gi => gi.Item == item) :
                GroupedInventory.FirstOrDefault(gi => gi.Item.ItemTypeID == item.ItemTypeID);

            if (groupedInventoryItemToRemove != null) //should never be null but good to check
            {
                //does object have quantity of 1? 
                //if so, completely remove item from groupedinventory.
                if (groupedInventoryItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else
                {
                    //decrease quantity by 1
                    groupedInventoryItemToRemove.Quantity--;
                }
            }

            OnPropertyChanged(nameof(Weapons));
        }
        private void RaiseOnKilledEvent()
        {
            //If there are any subscribers to OnKilled event, raise the event. Any subscribers will know if 
            //a livingentity gets killed 
            OnKilled?.Invoke(this, new System.EventArgs());
        }
    }

}
