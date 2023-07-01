using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace HorrorSounds
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ForceHorrorSound : ICommand
    {
        public string Command { get; } = "forcehorrorsound";
        public string[] Aliases { get; } = { "fhs", "forcehorror", "fh", "forcehs" };
        public string Description { get; } = "Triggers a random horror sound.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("horrorsounds.forcesound"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            if (Plugin.Instance.Config.CustomSounds.Count == 0)
            {
                response = "There are no custom horror sounds configured.";
                return false;
            }

            int randomIndex = Plugin.Instance.Random.Next(Plugin.Instance.Config.CustomSounds.Count);
            string randomSound = Plugin.Instance.Config.CustomSounds[randomIndex];

            Cassie.Message(randomSound, false, false);

            Log.Debug("forced a horror sound.");

            response = "A random horror sound has been triggered.";
            return true;
        }
    }
}