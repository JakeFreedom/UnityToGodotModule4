using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Catchem2D
{
    public partial class GameConfig : Node
    {
        private static IEventBus<GameEvent> bus;


        public GameConfig()
        {
            GD.Print("Game Config loaded");
            Instance = this;
            bus = new EventBus<GameEvent>();
        }

        public static GameConfig Instance { get; private set; }


        public IEventBus<GameEvent> GetBus() => bus;

    }
}
