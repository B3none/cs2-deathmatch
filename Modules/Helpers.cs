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
                
            if (string.IsNullOrEmpty(designerName))
            {
                continue;
            }
                
            if (designerName.StartsWith("weapon_"))
            {
                var entity = new PointerTo<CBasePlayerWeapon>(pEntity.Handle).Value;
                
                if (entity.IsValid)
                {
                    entity.AcceptInput("Kill");
                }
            }
            else if (designerName.StartsWith("item_"))
            {
                var entity = new PointerTo<CWeaponBaseItem>(pEntity.Handle).Value;
                
                if (entity.IsValid)
                {
                    entity.AcceptInput("Kill");
                }
            }
        }
    }
}
