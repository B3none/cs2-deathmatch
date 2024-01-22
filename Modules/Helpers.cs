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
    
    public static CCSGameRules GetGameRules()
    {
        var gameRulesEntities = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules");
        var gameRules = gameRulesEntities.First().GameRules;
        
        if (gameRules == null)
        {
            throw new Exception($"{DeathmatchPlugin.LogPrefix}Game rules not found!");
        }
        
        return gameRules;
    }
    
    public static void ExecuteDeathmatchConfiguration()
    {
        Server.ExecuteCommand("execifexists cs2-deathmatch/deathmatch.cfg");

        GetGameRules().IsDroppingItems = false;
    }

    public static void RemoveWeaponsOnGround()
    {
        // TODO: Make this work.
        
        // Console.WriteLine("Removing weapons on the ground.");
        var pEntity = new CEntityIdentity(EntitySystem.FirstActiveEntity);
        for (; pEntity != null && pEntity.Handle != IntPtr.Zero; pEntity = pEntity.Next)
        {
            var designerName = pEntity.DesignerName;
                
            if (
                string.IsNullOrEmpty(designerName)
                || !designerName.StartsWith("weapon_")
                || !designerName.StartsWith("item_")
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

    public static int GetClipCapacity(string weaponName)
    {
        return weaponName switch
        {
            "weapon_ak47" => 30,
            "weapon_m4a1" => 30,
            "weapon_m4a1_silencer" => 20,
            "weapon_awp" => 5,
            "weapon_sg552" => 30,
            "weapon_aug" => 30,
            "weapon_p90" => 50,
            "weapon_galilar" => 35,
            "weapon_famas" => 25,
            "weapon_ssg08" => 10,
            "weapon_g3sg1" => 20,
            "weapon_scar20" => 20,
            "weapon_m249" => 100,
            "weapon_negev" => 150,
            "weapon_nova" => 8,
            "weapon_xm1014" => 7,
            "weapon_sawedoff" => 7,
            "weapon_mag7" => 5,
            "weapon_mac10" => 30,
            "weapon_mp9" => 30,
            "weapon_mp7" => 30,
            "weapon_mp5sd" => 30,
            "weapon_ump45" => 25,
            "weapon_bizon" => 64,
            "weapon_glock" => 20,
            "weapon_fiveseven" => 20,
            "weapon_deagle" => 7,
            "weapon_revolver" => 8,
            "weapon_hkp2000" => 13,
            "weapon_usp_silencer" => 12,
            "weapon_p250" => 13,
            "weapon_elite" => 30,
            "weapon_tec9" => 24,
            "weapon_cz75a" => 12,
            _ => 30
        };
    }
}
