using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Utils;

namespace DeathmatchPlugin.Modules.Managers;

public static class AllocationManager
{
    public static void Allocate(CCSPlayerController player)
    {
        AllocateEquipment(player);
        AllocateWeapons(player);
    }

    private static void AllocateEquipment(CCSPlayerController player)
    {
        player.GiveNamedItem(CsItem.KevlarHelmet);
    }

    private static void AllocateWeapons(CCSPlayerController player)
    {
        if (player.Team == CsTeam.Terrorist)
        {
            player.GiveNamedItem(CsItem.AK47);
            // player.GiveNamedItem(CsItem.Glock);
            player.GiveNamedItem(CsItem.Deagle);
        }
        
        if (player.Team == CsTeam.CounterTerrorist)
        {
            // @klippy
            if (player.PlayerName.Trim() == "klip")
            {
                player.GiveNamedItem(CsItem.M4A4);
            }
            else
            {
                player.GiveNamedItem(CsItem.M4A1S);
            }

            // player.GiveNamedItem(CsItem.USPS);
            player.GiveNamedItem(CsItem.Deagle);
        }

        player.GiveNamedItem(CsItem.Knife);
    }
}