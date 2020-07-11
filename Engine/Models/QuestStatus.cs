using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class QuestStatus : BaseNotificationClass
    {
        private bool _isCompleted;

        //No setter as the only time this property should be set is in the constructor
        public Quest PlayerQuest { get; } //holds quest

        public bool IsCompleted 
        { 
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged();
            }
        }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false; //when we give player a quest, it's not completed yet
            //once player completes quest, can set IsCompleted to true.
        }

    }
}
