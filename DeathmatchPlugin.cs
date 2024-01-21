using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
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
        Console.WriteLine("Deathmatch loaded!");

        RegisterListener<Listeners.OnMapStart>(OnMapStart);

        if (hotReload)
        {
            OnMapStart(Server.MapName);
        }
    }

    private void OnMapStart(string mapName)
    {
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
        Console.WriteLine("OnPlayerHurt event fired!");
        
        return HookResult.Continue;
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
        
        var player = @event.Userid;
        
        if (!Helpers.IsValidPlayer(player))
        {
            return HookResult.Continue;
        }
        
        // Remove a players weapons and allocate them a new set.
        // player.RemoveWeapons();
        // AllocationManager.Allocate(player);
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        Console.WriteLine("OnPlayerDeath event fired!");
        
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnBombPickup(EventBombPickup @event, GameEventInfo info)
    {
        Console.WriteLine("OnBombPickup event fired!");
        
        return HookResult.Continue;
    }
}
