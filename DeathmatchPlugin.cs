using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;

namespace DeathmatchPlugin;

[MinimumApiVersion(147)]
public class DeathmatchPlugin : BasePlugin
{
    public override string ModuleName => "Deathmatch Plugin";
    public override string ModuleVersion => "0.0.1";
    public override string ModuleAuthor => "B3none";
    public override string ModuleDescription => "Free for all deathmatch game mode for CS2.";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Deathmatch loaded!");
    }

    [GameEventHandler(HookMode.Pre)]
    public HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
    {
        // Ensure all team join events are silent.
        @event.Silent = true;
        
        return HookResult.Continue;
    }
}
