using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using System;

namespace HorrorSounds
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "An4r3w";
        public override string Name { get; } = "Horror Sounds";
        public override string Prefix { get; } = "horrorsounds";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 5, 0);

        private EventHandler events;
        public static Plugin Instance { get; set; }

        public override void OnEnabled()
        {
            Instance = this;
            events = new EventHandler();

            Server.RoundStarted += events.OnRoundStarted;
            Server.RoundEnded += events.OnRoundEnded;

            Log.Debug("Plugin is enabled");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= events.OnRoundStarted;
            Server.RoundEnded -= events.OnRoundEnded;
            Log.Debug("Plugin is disabled");

            Instance = null;
            events = null;
            base.OnDisabled();
        }
    }
}
