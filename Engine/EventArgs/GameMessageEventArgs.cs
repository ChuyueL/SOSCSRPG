using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.EventArgs
{
    //System.EventArgs are the standard event arguments which are used when you raise an event.
    public class GameMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public GameMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
