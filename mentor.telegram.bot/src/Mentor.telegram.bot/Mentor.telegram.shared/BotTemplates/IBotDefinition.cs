using Bot.telegram.funnel;

namespace Mentor.telegram.shared.BotTemplates
{
    public interface IBotDefinition
    {
        Funnel GetFunnel();
    }
}