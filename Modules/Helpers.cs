using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;

namespace DeathmatchPlugin.Modules;

public static class Helpers
{
    public static bool IsValidPlayer(CCSPlayerController? player)
    {
        return player != null && player.IsValid;
    }
    
    public static void ExecuteDeathmatchConfiguration()
    {
        Server.ExecuteCommand("execifexists cs2-deathmatch/deathmatch.cfg");
    }

    public static void RemoveWeaponsOnGround()
    {
        // Console.WriteLine("Removing weapons on the ground.");
        var pEntity = new CEntityIdentity(EntitySystem.FirstActiveEntity);
        for (; pEntity != null && pEntity.Handle != IntPtr.Zero; pEntity = pEntity.Next)
        {
            var designerName = pEntity.DesignerName;
                
            if (
                string.IsNullOrEmpty(designerName)
                || designerName.StartsWith("weapon_")
                || designerName.StartsWith("item_")
            )
            {
                continue;
            }
            
            var entity = new PointerTo<CBaseEntity>(pEntity.Handle).Value;
                
            if (entity.IsValid)
            {
                entity.AcceptInput("Kill");
            }
        }
    }
}
