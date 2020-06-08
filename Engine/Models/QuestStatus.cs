using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class QuestStatus
    {
        public Quest PlayerQuest { get; set; } //holds quest
        public bool IsCompleted { get; set; }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false; //when we give player a quest, it's not completed yet
            //once player completes quest, can set IsCompleted to true.
        }

    }
}
