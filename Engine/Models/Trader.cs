using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        //max hit points and current hit points are just placeholders as trader doesn't need hit points.
        //9999 gold as placeholder in case we ever decide to subtract gold from trader 
        public Trader(string name) : base(name, 9999, 9999, 9999)
        {
        }

    }
}
