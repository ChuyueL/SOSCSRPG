using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
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

        //protected = can only be seen the child classes
        //limits the ability to instantiate LivingEntity objects
        protected LivingEntity()
        {
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            OnPropertyChanged(nameof(Weapons));
        }
    }
}
