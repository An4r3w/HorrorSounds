using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using System;
using System.Collections.Generic;

namespace HorrorSounds
{
    public class EventHandler
    {
        private CoroutineHandle cassieCoroutine;
        private List<string> customSounds;

        public void OnRoundStarted()
        {
            float delay = UnityEngine.Random.Range(60f, 120f);

            cassieCoroutine = Timing.CallDelayed(delay, PlayRandomHorrorSound);
            Log.Debug("the corountine has started.");
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(cassieCoroutine);
            Log.Debug("the corountine has been killed.");
        }

        public void PlayRandomHorrorSound()
        {
            customSounds = Plugin.Instance.Config.CustomSounds;

            if (customSounds.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, customSounds.Count);
                Cassie.Message(message: customSounds[randomIndex], isNoisy: false, isSubtitles: false);
                Log.Debug("a horror sound has been played");
            }

            float nextDelay = UnityEngine.Random.Range(Plugin.Instance.Config.MinDelaySeconds, Plugin.Instance.Config.MaxDelaySeconds);
            cassieCoroutine = Timing.CallDelayed(nextDelay, PlayRandomHorrorSound);
        }
    }
}
