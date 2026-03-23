using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catchem2D.Events
{
    public class BallCaughtEvent : GameEvent
    {
        public BallCaughtEvent(string name) {
            BallName = name;
        }

        public string BallName { get; private set; }
    }
}
