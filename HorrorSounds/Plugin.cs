using Exiled.API.Features;
using MEC;
using Exiled.Events.EventArgs.Server;
using System.Collections.Generic;
using System;

namespace HorrorSounds
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "An4r3w";
        public override string Name { get; } = "Horror Sounds";
        public override string Prefix { get; } = "horrorsounds";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(7, 0, 0);
        public Random Random { get; } = new Random();

        private CoroutineHandle cassieCoroutine;
        private List<string> customSounds;

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;

            customSounds = Config.CustomSounds;

            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;

            Log.Debug("Plugin is enabled");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            Log.Debug("Plugin is disabled");

            base.OnDisabled();
        }

        private void OnRoundStarted()
        {
            float delay = UnityEngine.Random.Range(60f, 120f);

            cassieCoroutine = Timing.CallDelayed(delay, PlayRandomHorrorSound);
            Log.Debug("the corountine has started.");
        }

        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(cassieCoroutine);
            Log.Debug("the corountine has been killed.");
        }

        private void PlayRandomHorrorSound()
        {
            if (customSounds.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, customSounds.Count);
                Cassie.Message(message: customSounds[randomIndex], isNoisy: false, isSubtitles: false);
                Log.Debug("a horror sound has been played");
            }

            float nextDelay = UnityEngine.Random.Range(Config.MinDelaySeconds, Config.MaxDelaySeconds);
            cassieCoroutine = Timing.CallDelayed(nextDelay, PlayRandomHorrorSound);
        }
    }
}