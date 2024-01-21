using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;

namespace DeathmatchPlugin;

[MinimumApiVersion(147)]
public class DeathmatchPlugin : BasePlugin
{
    public override string ModuleName => "Deathmatch Plugin";
    public override string ModuleVersion => "0.0.1";
    public override string ModuleAuthor => "B3none";
    public override string ModuleDescription => "Free for all deathmatch gamemode for CS2.";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Deathmatch loaded!");
    }
}
