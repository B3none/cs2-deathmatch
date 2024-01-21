using CounterStrikeSharp.API.Core;

namespace DeathmatchPlugin.Modules;

public static class Helpers
{
    public static bool IsValidPlayer(CCSPlayerController? player)
    {
        return player != null && player.IsValid;
    }
}
