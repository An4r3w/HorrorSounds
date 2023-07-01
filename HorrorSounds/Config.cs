using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace HorrorSounds
{
    public class Config : IConfig
    {
        [Description("Determines whether the Horror Sounds plugin is enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Determines whether debug messages should be printed in the console.")]
        public bool Debug { get; set; } = false;

        [Description("A list of custom horror sounds that will be played randomly during the round.")]
        public List<string> CustomSounds { get; set; } = new List<string>() { "pitch_0.1 .g7", ".g3 .g2" };

        [Description("The minimum delay in seconds between playing the horror sounds.")]
        public float MinDelaySeconds { get; set; } = 300;

        [Description("The maximum delay in seconds between playing the horror sounds.")]
        public float MaxDelaySeconds { get; set; } = 500;
    }
}