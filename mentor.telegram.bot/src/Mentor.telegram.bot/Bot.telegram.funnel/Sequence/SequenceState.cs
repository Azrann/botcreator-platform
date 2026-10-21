using System.Collections.Generic;
using Bot.telegram.funnel.Actions;

namespace Bot.telegram.funnel
{
    public class SequenceState
    {
        public static string MainMenu = "Main Menu 🔙";
        public static string Back = "Back ↩";

        public List<FunnelAction> MainMenuActions { get; set; }
        public List<IStepStates> Steps { get; set; }
        public string FirstStepCallback { get; set; }

        public string Id { get; set; }

        public SequenceState()
        {
            Steps = new List<IStepStates>();
        }
    }
}