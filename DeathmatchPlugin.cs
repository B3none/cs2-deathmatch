using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using Helpers = DeathmatchPlugin.Modules.Helpers;

namespace DeathmatchPlugin;

[MinimumApiVersion(150)]
public class DeathmatchPlugin : BasePlugin
{
    private const string Version = "0.0.1";
    
    #region Plugin info
    public override string ModuleName => "Deathmatch Plugin";
    public override string ModuleVersion => Version;
    public override string ModuleAuthor => "B3none";
    public override string ModuleDescription => "Community deathmatch for CS2.";
    #endregion

    #region Constants
    public static readonly string LogPrefix = $"[Deathmatch {Version}] ";
    public static readonly string MessagePrefix = $"[{ChatColors.Green}Deathmatch{ChatColors.White}] ";
    #endregion
    
    public override void Load(bool hotReload)
    {
        Console.WriteLine($"{LogPrefix}loaded!");

        RegisterListener<Listeners.OnMapStart>(OnMapStart);

        if (hotReload)
        {
            OnMapStart(Server.MapName);
        }
    }
    
    private readonly HashSet<CCSPlayerController> _headshotOnlyPlayers = new();
    
    [ConsoleCommand("css_headshot", "Toggles headshot only mode.")]
    public void OnCommandHeadshot(CCSPlayerController? player, CommandInfo commandInfo)
    {
        if (player == null || !player.IsValid)
        {
            return;
        }
        
        if (!_headshotOnlyPlayers.Add(player))
        {
            _headshotOnlyPlayers.Remove(player);
            player.PrintToChat($"{MessagePrefix}Headshot only mode {ChatColors.DarkRed}disabled{ChatColors.White}.");
        }
        else
        {
            player.PrintToChat($"{MessagePrefix}Headshot only mode {ChatColors.Green}enabled{ChatColors.White}.");
        }
    }

    private void OnMapStart(string mapName)
    {
        var gameModeCvar = ConVar.Find("game_mode");
        if (gameModeCvar != null && gameModeCvar.GetPrimitiveValue<int>() != 2)
        {
            throw new Exception($"{LogPrefix}This plugin requires game_mode to be set to 2.");
        }

        var gameTypeCvar = ConVar.Find("game_type");
        if (gameTypeCvar != null && gameTypeCvar.GetPrimitiveValue<int>() != 1)
        {
            throw new Exception($"{LogPrefix}This plugin requires game_type to be set to 1.");
        }
        
        AddTimer(0.5f, () =>
        {
            // Update spawn point statuses.
            // Console.WriteLine("Updating spawn point statuses.");
        }, TimerFlags.REPEAT);

        AddTimer(0.5f, Helpers.RemoveWeaponsOnGround, TimerFlags.REPEAT);
        
        Helpers.ExecuteDeathmatchConfiguration();
    }
    
    [GameEventHandler(HookMode.Pre)]
    public HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
    {
        Console.WriteLine("OnPlayerTeam event fired!");
        
        // Silence everything except the disconnect message.
        if (!@event.Disconnect)
        {
            @event.Silent = true;
        }

        return HookResult.Continue;
    }
    
    [GameEventHandler(HookMode.Pre)]
    public HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        var attacker = @event.Attacker;
        
        if (!_headshotOnlyPlayers.Contains(attacker))
        {
            return HookResult.Continue;
        }

        // If headshot, continue.
        if (@event.Hitgroup == 1)
        {
            return HookResult.Continue;
        }
        
        var victim = @event.Userid;
        
        if (victim == null || victim.PlayerPawn.Value == null)
        {
            return HookResult.Continue;
        }

        // Boost the victim health the amount they will be damaged.
        victim.PlayerPawn.Value.Health = (victim.PlayerPawn.Value.Health + @event.DmgHealth);
        Utilities.SetStateChanged(victim.PlayerPawn.Value, "CBaseEntity", "m_iHealth");
        
        victim.PlayerPawn.Value.ArmorValue = (victim.PlayerPawn.Value.ArmorValue + @event.DmgArmor);
        Utilities.SetStateChanged(victim.PlayerPawn.Value, "CCSPlayerPawnBase", "m_ArmorValue");
        
        return HookResult.Changed;
    }

    [GameEventHandler]
    public HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info)
    {
        Console.WriteLine("OnRoundPreStart event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        Console.WriteLine("OnRoundStart event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        Console.WriteLine("OnRoundEnd event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnWeaponFireOnEmpty(EventWeaponFireOnEmpty @event, GameEventInfo info)
    {
        Console.WriteLine("OnWeaponFireOnEmpty event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnHeGrenadeDetonate(EventHegrenadeDetonate @event, GameEventInfo info)
    {
        Console.WriteLine("OnHeGrenadeDetonate event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnSmokeDetonate(EventSmokegrenadeDetonate @event, GameEventInfo info)
    {
        Console.WriteLine("OnSmokeDetonate event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnFlashbangDetonate(EventFlashbangDetonate @event, GameEventInfo info)
    {
        Console.WriteLine("OnFlashbangDetonate event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnMolotovDetonate(EventMolotovDetonate @event, GameEventInfo info)
    {
        Console.WriteLine("OnMolotovDetonate event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnInfernoStartBurn(EventInfernoStartburn @event, GameEventInfo info)
    {
        Console.WriteLine("OnInfernoStartBurn event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnDecoyStarted(EventDecoyStarted @event, GameEventInfo info)
    {
        Console.WriteLine("OnDecoyStarted event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        Console.WriteLine("OnPlayerSpawn event fired!");
        
        // TODO: Implement custom spawn logic.
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        Console.WriteLine("OnPlayerDeath event fired!");

        var attacker = @event.Attacker;
        
        Server.NextFrame(() =>
        {
            // Refill clip on kill.
            if (
                attacker == null 
                || !Helpers.IsValidPlayer(attacker)
                || attacker.PlayerPawn.Value == null
                || !attacker.PlayerPawn.Value.IsValid
                || attacker.PlayerPawn.Value.WeaponServices == null
            )
            {
                return;
            }
        
            var activeWeapon = attacker.PlayerPawn.Value.WeaponServices.ActiveWeapon.Value;

            if (activeWeapon == null || !activeWeapon.IsValid)
            {
                return;
            }
        
            activeWeapon.Clip1 = Helpers.GetClipCapacity(activeWeapon.DesignerName);
        });
        
        return HookResult.Continue;
    }
}
