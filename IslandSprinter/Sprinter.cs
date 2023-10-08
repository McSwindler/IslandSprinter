using System;
using Dalamud.Hooking;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace IslandSprinter;

public unsafe class Sprinter : IDisposable
{
    private static uint SPRINT = 4;
    private static uint ISLAND_SPRINT = 31314;
    private static ushort ISLAND_TERRITORY_ID = 1055;
    public Sprinter()
    {
        useActionHook = Services.GameInteropProvider.HookFromAddress<UseActionDelegate>((IntPtr)ActionManager.MemberFunctionPointers.UseAction, UseActionDetour);
    }

    private delegate bool UseActionDelegate(ActionManager* actionManager, ActionType actionType, uint actionID, ulong targetObjectID, uint param, uint useType, uint pvp, void* arg8);
    private bool UseActionDetour(ActionManager* actionManager, ActionType actionType, uint actionID, ulong targetObjectID, uint param, uint useType, uint pvp, void* arg8)
    {
        //Services.PluginLog.Debug("Used Action {0} {1}", actionType.ToString(), actionID);
        if (Services.ClientState.TerritoryType.Equals(ISLAND_TERRITORY_ID) && actionType.Equals(ActionType.GeneralAction) && actionID.Equals(SPRINT))
        {
            return useActionHook.Original(actionManager, ActionType.Action, ISLAND_SPRINT, targetObjectID, param, useType, pvp, arg8);
        }
        return useActionHook.Original(actionManager, actionType, actionID, targetObjectID, param, useType, pvp, arg8);
    }

    private readonly Hook<UseActionDelegate> useActionHook;

    public void Initialize()
    {
        
        useActionHook.Enable();
    }

    public void Dispose() { 
        useActionHook.Dispose();
    }
}
